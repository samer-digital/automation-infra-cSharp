using Serilog;
using Microsoft.Playwright;

/// <summary>
/// Manages test contexts, pages, tear-down actions, and capturing screenshots.
/// </summary>
public class TestContext
{
    private readonly ILogger _logger;
    private readonly Dictionary<string, ContextHolder> _contextHolders;
    private readonly List<Func<TestContext, Task>> _tearDownActions;
    private readonly WorkerContext _workerContext;
    
    // public static Dictionary<string, ContextOptions> LoadContextOptions()
    // {
    //     var configContent = File.ReadAllText(filePath);
    //     return JsonConvert.DeserializeObject<Dictionary<string, ContextOptions>>(configContent);
    // }

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
    /// Gets the context holder for the specified context key.
    /// </summary>
    /// <param name="contextKey">The context key.</param>
    /// <returns>The context holder for the specified context key.</returns>
    private ContextHolder GetContext(string contextKey)
    {
        if (!_contextHolders.ContainsKey(contextKey))
        {
            var contextHolder = new ContextHolder(
                async () =>
                {
                    var browser = await _workerContext.GetBrowserAsync();
                    return await browser.NewContextAsync(new BrowserNewContextOptions()
                    { 
                        ViewportSize = ViewportSize.NoViewport
                    });
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