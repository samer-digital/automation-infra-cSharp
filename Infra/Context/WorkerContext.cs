using Microsoft.Playwright;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// Manages browser instances, loads browser options, handles tear-down actions, and creates API request contexts.
/// </summary>
public class WorkerContext
{
    private IBrowser? _browser;
    private readonly List<Func<WorkerContext, Task>> _tearDownActions;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkerContext"/> class.
    /// </summary>
    public WorkerContext()
    {
        _tearDownActions = new List<Func<WorkerContext, Task>>();
    }

    /// <summary>
    /// Loads the browser options from a JSON configuration file.
    /// </summary>
    /// <returns>The browser launch options.</returns>
    public static BrowserTypeLaunchOptions LoadBrowserOptions()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"../../../../browserOptions.json");
        string configFilePath = Path.GetFullPath(filePath);
        var configContent = File.ReadAllText(configFilePath);
        var options = JsonConvert.DeserializeObject<BrowserOptions>(configContent)!;
        return new BrowserTypeLaunchOptions
        {
            Headless = options.Headless,
            SlowMo = options.SlowMo,
            Args = options.Args,
            Timeout = options.Timeout
        };
    }

    /// <summary>
    /// Gets the browser instance asynchronously.
    /// </summary>
    /// <returns>The browser instance.</returns>
    public async Task<IBrowser> GetBrowserAsync()
    {
        if (_browser == null)
        {
            BrowserTypeLaunchOptions browserTypeLaunchOptions = LoadBrowserOptions();
            var playwright = await Playwright.CreateAsync();
            switch (ConfigProvider.BROWSER)
            {
                case "chromium":
                    _browser = await playwright.Chromium.LaunchAsync(browserTypeLaunchOptions);
                    break;
                case "firefox":
                    _browser = await playwright.Firefox.LaunchAsync(browserTypeLaunchOptions);
                    break;
                case "webkit":
                    _browser = await playwright.Webkit.LaunchAsync(browserTypeLaunchOptions);
                    break;
                default:
                    throw new ArgumentException("Unsupported browser type");
            }
        }
        return _browser;
    }

    /// <summary>
    /// Adds a tear-down action to be executed after the test.
    /// </summary>
    /// <param name="tearDownAction">The tear-down action.</param>
    public void AddTearDownAction(Func<WorkerContext, Task> tearDownAction)
    {
        _tearDownActions.Add(tearDownAction);
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
    /// Disposes the worker context asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DisposeAsync()
    {
        if (_browser != null)
        {
            await _browser.CloseAsync();
            _browser = null;
        }
    }

    /// <summary>
    /// Gets the API request context asynchronously.
    /// </summary>
    /// <returns>The API request context.</returns>
    public async Task<IAPIRequestContext> GetApiRequestContextAsync()
    {
        var playwright = await Playwright.CreateAsync();
        return await playwright.APIRequest.NewContextAsync();
    }
}