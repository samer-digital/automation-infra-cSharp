using OpenQA.Selenium.Appium;

public static class CapabilitiesProvider
{
    public static AppiumOptions GetCapabilitiesForPage(AppPageBase appPage, AppPageOptions appPageOptions)
    {
        AppiumOptions options;

        switch (appPage.platform)
        {
            case PlatformName.ANDROID:
                options = GetAndroidCapabilities(appPage, appPageOptions);
                break;
                
            case PlatformName.IOS:
                options = GetIosCapabilities(appPage, appPageOptions);
                break;
                
            default:
                throw new ArgumentException("Unsupported platform type.");
        }

        if (ConfigProvider.USE_SAUCE_LABS)
        {
            AddSauceLabsOptions(options, appPageOptions);
        }

        return options;
    }

    private static AppiumOptions GetAndroidCapabilities(AppPageBase appPage, AppPageOptions appPageOptions)
    {
        var options = new AppiumOptions
        {
            AutomationName = ConfigProvider.ANDROID_AUTOMATION_NAME,
            PlatformName = "Android",
            DeviceName = appPageOptions.DeviceName ?? ConfigProvider.ANDROID_DEVICE_NAME,
            PlatformVersion = appPageOptions.PlatformVersion ?? ConfigProvider.ANDROID_PLATFORM_VERSION,
            App = appPage.AppPath
        };

        options.AddAdditionalAppiumOption("autoGrantPermissions", ConfigProvider.AUTO_GRANT_PERMISSION);
        options.AddAdditionalAppiumOption("deviceOrientation", appPageOptions.DeviceOrientation);
        options.AddAdditionalAppiumOption("noReset", ConfigProvider.NO_RESET);

        if (appPage is AndroidAppPageBase androidPage)
        {
            options.AddAdditionalAppiumOption("appium:appPackage", androidPage.AndroidAppPackage);
            options.AddAdditionalAppiumOption("appium:appActivity", androidPage.AndroidAppActivity);
        }

        return options;
    }

    private static AppiumOptions GetIosCapabilities(AppPageBase appPage, AppPageOptions appPageOptions)
    {
        var options = new AppiumOptions
        {
            AutomationName = ConfigProvider.IOS_AUTOMATION_NAME,
            PlatformName = "iOS",
            DeviceName = appPageOptions.DeviceName ?? ConfigProvider.IOS_DEVICE_NAME,
            PlatformVersion = appPageOptions.PlatformVersion ?? ConfigProvider.IOS_PLATFORM_VERSION,
            App = appPage.AppPath
        };

        options.AddAdditionalAppiumOption("autoAcceptAlerts", ConfigProvider.AUTO_ACCEPT_ALERTS);
        options.AddAdditionalAppiumOption("deviceOrientation", appPageOptions.DeviceOrientation);
        options.AddAdditionalAppiumOption("noReset", ConfigProvider.NO_RESET);

        if (appPage is IosAppPageBase iosPage)
        {
            options.AddAdditionalAppiumOption("appium:bundleId", iosPage.IosBundleId);
            if (!string.IsNullOrEmpty(ConfigProvider.IOS_DEVICE_UDID) && !ConfigProvider.USE_SAUCE_LABS)
            {
                options.AddAdditionalAppiumOption("udid", appPageOptions.Udid ?? ConfigProvider.IOS_DEVICE_UDID);
            }
        }

        return options;
    }

    private static void AddSauceLabsOptions(AppiumOptions options, AppPageOptions appPageOptions)
    {
        var sauceOptions = new Dictionary<string, object>
        {
            { "username", ConfigProvider.SAUCE_LABS_USERNAME! },
            { "accessKey", ConfigProvider.SAUCE_LABS_ACCESS_KEY! },
            { "build", ConfigProvider.SAUCE_LABS_BUILD_ID },
            { "sessionCreationTimeout", ConfigProvider.SESSION_CREATION_CONNECTION },
            { "cacheId", ConfigProvider.SAUCE_LAB_CACHE_ID },
            { "appiumVersion", ConfigProvider.SAUCE_LAB_APPIUM_VERSION },
            { "name", appPageOptions.TestName! }
        };
        options.AddAdditionalAppiumOption("sauce:options", sauceOptions);
    }
}
