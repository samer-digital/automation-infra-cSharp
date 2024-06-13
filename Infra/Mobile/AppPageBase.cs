using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

public abstract class AppPageBase
{
    private AppiumDriver? _driver;

    private WebDriverWait? _wait;
    private bool _isInitialized = false;

    protected AppPageBase() { }

    public void Init(AppiumDriver driver)
    {
        if (_isInitialized)
        {
            throw new InvalidOperationException("Init() should be called only once");
        }
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // Set in the config
        _isInitialized = true;
    }

    protected AppiumDriver Driver
    {
        get
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Init() should be called before using driver");
            }
            return _driver!;
        }
    }

    public async Task WaitUntilWebViewContextLoad(int timeout)
    {
        await Task.Run(() =>
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(d =>
            {
                var contexts = ((AppiumDriver)d).Contexts;
                return contexts.Contains("WEBVIEW");
            });
        });
    }

    public async Task SwitchToWebViewContext()
    {
        var contexts = await GetContexts();
        foreach (var context in contexts)
        {
            if (context.Contains("WEBVIEW"))
            {
                await SwitchContext(context);
                break;
            }
        }
    }

    public async Task<string> GetCurrentContext()
    {
        return await Task.FromResult(_driver!.Context);
    }

    public async Task<IList<string>> GetContexts()
    {
        return await Task.FromResult(_driver!.Contexts);
    }

    public async Task SwitchContext(string context)
    {
        await Task.Run(() => _driver!.Context = context);
    }

    public async Task<string> GetUrl()
    {
        return await Task.FromResult(_driver!.Url);
    }

    public async Task CloseWindow()
    {
        await Task.Run(() => _driver!.Close());
    }

    public async Task<string> GetTitle()
    {
        return await Task.FromResult(_driver!.Title);
    }

    public async Task DeleteSession()
    {
        await Task.Run(() => _driver!.Quit());
    }

    public async Task LaunchApp(String appId)
    {
        await Task.Run(() => _driver!.ActivateApp(appId));
    }

    public async Task CloseApp(String appId)
    {
        await Task.Run(() => _driver!.TerminateApp(appId));
    }

    public async Task RemoveApp(string appName)
    {
        await Task.Run(() => _driver!.RemoveApp(appName));
    }

    public string? AppPath
    {
        get
        {
            return platform switch
            {
                PlatformName.ANDROID => ConfigProvider.ANDROID_APP_PATH,
                PlatformName.IOS => ConfigProvider.IOS_APP_PATH,
                _ => throw new NotSupportedException("Unsupported platform")
            };
        }
    }



    public async Task<bool> WaitUntilElementDisplayed(By locator, int timeout)
    {
        try
        {
            await Task.Run(() => _wait!.Until(d => d.FindElement(locator).Displayed));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public async Task<bool> WaitForElementNotDisplayed(By locator, int timeout)
    {
        try
        {
            await Task.Run(() => _wait!.Until(d => !d.FindElement(locator).Displayed));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public string? AppId => ConfigProvider.ANDROID_APP_PACKAGE;
    public string? BundleId => ConfigProvider.IOS_BUNDLE_ID;
    public abstract PlatformName platform { get; }
}