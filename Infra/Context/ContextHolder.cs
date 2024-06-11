using Microsoft.Playwright;
using Serilog;

/// <summary>
/// Manages browser contexts, API contexts, and captures screenshots.
/// </summary>
public class ContextHolder : IDisposable
{
    private IBrowserContext? _browserContext;
    private IAPIRequestContext? _apiContext;

    private readonly Func<Task<IBrowserContext>> _browserContextCreator;
    private readonly Func<Task<IAPIRequestContext>> _apiContextCreator;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextHolder"/> class.
    /// </summary>
    /// <param name="browserContextCreator">A function to create a new browser context.</param>
    /// <param name="apiContextCreator">A function to create a new API context.</param>
    /// <param name="logger">The logger instance for logging information.</param>
    public ContextHolder(Func<Task<IBrowserContext>> browserContextCreator, Func<Task<IAPIRequestContext>> apiContextCreator, ILogger logger)
    {
        _browserContextCreator = browserContextCreator;
        _apiContextCreator = apiContextCreator;
        _logger = logger;
    }

    /// <summary>
    /// Gets the browser context asynchronously.
    /// </summary>
    /// <returns>The browser context.</returns>
    private async Task<IBrowserContext> GetBrowserContextAsync()
    {
        if (_browserContext == null)
        {
            _browserContext = await _browserContextCreator();
        }
        return _browserContext;
    }

    /// <summary>
    /// Gets the API context asynchronously.
    /// </summary>
    /// <returns>The API context.</returns>
    private async Task<IAPIRequestContext> GetApiContextAsync()
    {
        if (_apiContext == null)
        {
            _apiContext = await _apiContextCreator();
        }
        return _apiContext;
    }

    /// <summary>
    /// Gets a page asynchronously. If no pages are open, opens a new page.
    /// </summary>
    /// <returns>The page.</returns>
    public async Task<IPage> GetPageAsync()
    {
        var browserContext = await GetBrowserContextAsync();
        if (browserContext.Pages.Count == 0)
        {
            await browserContext.NewPageAsync();
        }
        return browserContext.Pages[0];
    }

    /// <summary>
    /// Gets the API context asynchronously.
    /// </summary>
    /// <returns>The API context.</returns>
    public async Task<IAPIRequestContext> GetApiAsync()
    {
        return await GetApiContextAsync();
    }

    /// <summary>
    /// Captures a screenshot and saves it to a file.
    /// </summary>
    /// <param name="prefix">The prefix for the screenshot file name.</param>
    /// <param name="testName">The name of the test.</param>
    /// <returns>The screenshot as a byte array.</returns>
    public async Task<byte[]?> CaptureScreenshotAsync(string prefix, string testName)
    {
        var page = await GetPageAsync();
        if (page != null)
        {
            var screenshotPath = Path.Combine(AppContext.BaseDirectory, @"../../../resources/screenshots", $"{prefix}_{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
            _logger.Information($"Screenshot saved to {screenshotPath}");
            return await File.ReadAllBytesAsync(screenshotPath);
        }
        return null;
    }

    /// <summary>
    /// Disposes the browser and API contexts asynchronously.
    /// </summary>
    public async Task DisposeAsync()
    {
        if (_browserContext != null)
        {
            await _browserContext.CloseAsync();
        }
        _browserContext = null;

        if (_apiContext != null)
        {
            await _apiContext.DisposeAsync();
        }
        _apiContext = null;
    }

    /// <summary>
    /// Disposes the browser and API contexts.
    /// </summary>
    public void Dispose()
    {
        DisposeAsync().GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }
}