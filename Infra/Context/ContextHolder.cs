using Microsoft.Playwright;
using OpenQA.Selenium.Appium;
using Serilog;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

/// <summary>
/// Manages browser contexts, API contexts, and captures screenshots.
/// </summary>
public class ContextHolder : IDisposable
{
    private AppiumDriver? _driver;

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
            await _browserContext.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
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

    public async Task<IPage> NewTabAsync()
    {
        var browserContext = await GetBrowserContextAsync();
        return await browserContext.NewPageAsync();
    }

    public async Task<IPage> SwitchToTabAsync(int tabIndex)
    {
        var browserContext = await GetBrowserContextAsync();
        if (tabIndex < 0 || tabIndex >= browserContext.Pages.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(tabIndex), "Invalid tab index");
        }
        return browserContext.Pages[tabIndex];
    }

    public AppiumDriver GetDriverAsync(AppiumOptions options, PlatformName platformName)
    {
        if (_driver == null)
        {
            var url = new Uri($"{ConfigProvider.APPIUM_HOST}:{ConfigProvider.APPIUM_PORT}/{ConfigProvider.APPIUM_BASE_URL}");
            if (platformName.Equals(PlatformName.ANDROID)) {
                _driver = new AndroidDriver(url, options);
            } else {
                _driver = new IOSDriver(url, options);
            }
        }
        return _driver;
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
            var screenshotPath = Path.Combine(AppContext.BaseDirectory, @"../../../Resources/screenshots", $"{prefix}_{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
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
        var failed = NUnit.Framework.TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
    || NUnit.Framework.TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;
        if (_browserContext != null)
        {
            await _browserContext.Tracing.StopAsync(new()
            {
                Path = failed || NUnit.Framework.TestContext.CurrentContext.Test.Name == "CopyAddressLinkAndNavigateTest" ? Path.Combine(
                AppContext.BaseDirectory, @"../../../Resources/playwright-traces",
                $"{NUnit.Framework.TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.zip"
            ) : null,
            });
            await _browserContext.CloseAsync();
        }
        _browserContext = null;

        if (_apiContext != null)
        {
            await _apiContext.DisposeAsync();
        }
        _apiContext = null;

        _driver?.Dispose(); // Should add plugin for end session and update status in sauce labs, screenshots.
        _driver = null;
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