using Serilog;
using Microsoft.Playwright;
using Newtonsoft.Json;

/// <summary>
/// Manages test contexts, pages, tear-down actions, and capturing screenshots.
/// </summary>
public class TestContext
{
    private readonly ILogger _logger;
    private readonly Dictionary<string, ContextHolder> _contextHolders;
    private readonly List<Func<TestContext, Task>> _tearDownActions;
    private readonly WorkerContext _workerContext;
    private static Dictionary<string, ContextOptions>? _contextOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestContext"/> class.
    /// </summary>
    /// <param name="workerContext">The worker context.</param>
    /// <param name="testName">The name of the test.</param>
    /// <param name="logger">The logger instance for logging information.</param>
    public TestContext(WorkerContext workerContext, string testName, ILogger logger)
    {
        _contextHolders = new Dictionary<string, ContextHolder>();
        _tearDownActions = new List<Func<TestContext, Task>>();
        _workerContext = workerContext;
        _logger = logger;
        _logger.Information($"*** Initializing context for {testName} ***");
    }

    /// <summary>
    /// Loads the context options from a JSON file.
    /// </summary>
    /// <returns>A dictionary of context options.</returns>
    private static Dictionary<string, ContextOptions>? LoadContextOptions()
    {
        if (_contextOptions == null)
        {
            string contextOptionsFile = Path.Combine(Directory.GetCurrentDirectory(), @"../../../../contextOptions.json");
            string contextOptionsPath = Path.GetFullPath(contextOptionsFile);
            var contextOptions = File.ReadAllText(contextOptionsPath);
            _contextOptions = JsonConvert.DeserializeObject<Dictionary<string, ContextOptions>>(contextOptions);
        }
        return _contextOptions;
    }

    /// <summary>
    /// Creates browser context options from the context options.
    /// </summary>
    /// <param name="contextOptions">The context options.</param>
    /// <returns>Browser new context options.</returns>
    private BrowserNewContextOptions CreateBrowserContextOptions(ContextOptions contextOptions)
    {
        var storageStatePath = Path.Combine(Directory.GetCurrentDirectory(), @"../../../Resources/storageState.json");
        return new BrowserNewContextOptions
        {
            ViewportSize = contextOptions.Viewport != null ? new ViewportSize { Width = contextOptions.Viewport.Width, Height = contextOptions.Viewport.Height } : ViewportSize.NoViewport,
            UserAgent = contextOptions.UserAgent,
            Locale = contextOptions.Locale,
            TimezoneId = contextOptions.TimezoneId,
            Geolocation = contextOptions.Geolocation != null ? new Geolocation { Latitude = contextOptions.Geolocation.Latitude, Longitude = contextOptions.Geolocation.Longitude } : null,
            IgnoreHTTPSErrors = contextOptions.IgnoreHTTPSErrors,
            JavaScriptEnabled = contextOptions.JavaScriptEnabled,
            DeviceScaleFactor = contextOptions.DeviceScaleFactor > 0 ? contextOptions.DeviceScaleFactor : null,
            IsMobile = contextOptions.IsMobile,
            HasTouch = contextOptions.HasTouch,
            StorageStatePath = File.Exists(storageStatePath) ? storageStatePath : null
        };
    }

    /// <summary>
    /// Gets the context holder for the specified context key.
    /// </summary>
    /// <param name="contextKey">The context key.</param>
    /// <returns>The context holder for the specified context key.</returns>
    private ContextHolder GetContext(string contextKey)
    {
        if (!_contextHolders.ContainsKey(contextKey))
        {
            _contextOptions = LoadContextOptions();
            if (_contextOptions == null || !_contextOptions.ContainsKey(contextKey))
            {
                throw new KeyNotFoundException($"Context key '{contextKey}' not found in context options.");
            }
            var contextOptions = _contextOptions[contextKey];
            var contextOptionsDictionary = CreateBrowserContextOptions(contextOptions);

            var contextHolder = new ContextHolder(
                async () =>
                {
                    var browser = await _workerContext.GetBrowserAsync();
                    var browserContext = await browser.NewContextAsync(contextOptionsDictionary);

                    // Clear cookies and local storage
                    await browserContext.ClearCookiesAsync();

                    // Grant permissions
                    if (contextOptions.Permissions != null)
                    {
                        foreach (var permission in contextOptions.Permissions)
                        {
                            await browserContext.GrantPermissionsAsync([permission]);
                        }
                    }

                    return browserContext;
                },
                async () => await _workerContext.GetApiRequestContextAsync(),
                _logger
            );
            _contextHolders[contextKey] = contextHolder;
        }
        return _contextHolders[contextKey];
    }

    /// <summary>
    /// Gets the page asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the page.</typeparam>
    /// <param name="pageClassFactory">The factory method to create the page instance.</param>
    /// <param name="options">The options for getting the page.</param>
    /// <returns>The page instance.</returns>
    public async Task<T> GetPageAsync<T>(Func<IPage, T> pageClassFactory, GetPageOptions? options = null) where T : PageBase
    {
        options ??= new GetPageOptions { ContextKey = "default", ShouldNavigate = false };
        options.ContextKey ??= "default";

        var contextHolder = GetContext(options.ContextKey);
        var page = await contextHolder.GetPageAsync();
        var typedPage = pageClassFactory(page);

        if (options.ShouldNavigate)
        {
            await typedPage.NavigateAsync();
        }

        return typedPage;
    }

    /// <summary>
    /// Adds a tear-down action to be executed after the test.
    /// </summary>
    /// <param name="tearDownAction">The tear-down action.</param>
    public void AddTearDownAction(Func<Task> tearDownAction)
    {
        _tearDownActions.Add(async _ => await tearDownAction());
    }

    /// <summary>
    /// Executes all tear-down actions asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task TearDownAsync()
    {
        foreach (var tearDownAction in _tearDownActions)
        {
            await tearDownAction(this);
        }
    }

    /// <summary>
    /// Takes screenshots for all contexts asynchronously.
    /// </summary>
    /// <param name="title">The title for the screenshots.</param>
    /// <returns>A list of byte arrays representing the screenshots.</returns>
    public async Task<List<byte[]?>> TakeScreenshotsAsync(string title)
    {
        var tasks = _contextHolders.Select(async kvp =>
        {
            var (holderKey, holder) = kvp;
            return await holder.CaptureScreenshotAsync(holderKey, title);
        });

        var results = await Task.WhenAll(tasks);
        return results.Where(r => r != null).ToList();
    }

    /// <summary>
    /// Disposes the test context asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DisposeAsync()
    {
        foreach (var contextHolder in _contextHolders.Values)
        {
            await contextHolder.DisposeAsync();
        }
        _contextHolders.Clear();
        _tearDownActions.Clear();
    }
}
