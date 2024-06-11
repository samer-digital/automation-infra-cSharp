public class CommonCapabilities
{
    public string? PlatformVersion { get; set; }
    public string? AutomationName { get; set; }
    public string? App { get; set; }
    public string? DeviceName { get; set; }
    public DeviceOrientation? DeviceOrientation { get; set; }
    public bool? NoReset { get; set; }
}

public class AndroidCapabilities : CommonCapabilities
{
    public PlatformName PlatformName { get; set; } = PlatformName.ANDROID;
    public string? AppPackage { get; set; }
    public string? AppActivity { get; set; }
    public bool? AutoGrantPermissions { get; set; }
}

public class IosCapabilities : CommonCapabilities
{
    public PlatformName PlatformName { get; set; } = PlatformName.IOS;
    public string? Udid { get; set; }
    public string? BundleId { get; set; }
    public bool? AutoAcceptAlerts { get; set; }
    public bool IncludeSafariInWebviews { get; set; }
}

public class Capabilities
{
    public AndroidCapabilities? Android { get; set; }
    public IosCapabilities? Ios { get; set; }
}

public class AdditionalCapabilitiesForPage
{
    public string? DeviceName { get; set; }
    public DeviceOrientation? DeviceOrientation { get; set; }
    public string? PlatformVersion { get; set; }
    public string? Udid { get; set; }
    public string? TestName { get; set; }
}

public class CapabilityProvider
{
    public static Capabilities GetCapabilitiesForPage(AppPageBase appPage, AdditionalCapabilitiesForPage additionalCapabilities)
    {
        Capabilities capabilities = new Capabilities();

        switch (appPage.platform)
        {
            case PlatformName.ANDROID:
                var androidCapabilities = new AndroidCapabilities
                {
                    PlatformVersion = additionalCapabilities.PlatformVersion ?? ConfigProvider.ANDROID_PLATFORM_VERSION,
                    AutomationName = ConfigProvider.ANDROID_AUTOMATION_NAME,
                    DeviceName = additionalCapabilities.DeviceName ?? ConfigProvider.ANDROID_DEVICE_NAME,
                    AutoGrantPermissions = ConfigProvider.AUTO_GRANT_PERMISSION,
                    App = appPage.AppPath,
                    DeviceOrientation = additionalCapabilities.DeviceOrientation ?? DeviceOrientation.PORTRAIT,
                    NoReset = ConfigProvider.NO_RESET
                };

                if (appPage is AndroidAppPageBase androidAppPage)
                {
                    androidCapabilities.AppPackage = androidAppPage.AndroidAppPackage;
                    androidCapabilities.AppActivity = androidAppPage.AndroidAppActivity;
                }

                capabilities.Android = androidCapabilities;
                break;

            case PlatformName.IOS:
                var iosCapabilities = new IosCapabilities
                {
                    PlatformVersion = additionalCapabilities.PlatformVersion ?? ConfigProvider.IOS_PLATFORM_VERSION,
                    AutomationName = ConfigProvider.IOS_AUTOMATION_NAME,
                    DeviceName = additionalCapabilities.DeviceName ?? ConfigProvider.IOS_DEVICE_NAME,
                    AutoAcceptAlerts = ConfigProvider.AUTO_ACCEPT_ALERTS,
                    NoReset = ConfigProvider.NO_RESET,
                    App = appPage.AppPath,
                    DeviceOrientation = additionalCapabilities.DeviceOrientation ?? DeviceOrientation.PORTRAIT
                };

                if (appPage is IosAppPageBase iosAppPage)
                {
                    iosCapabilities.BundleId = iosAppPage.IosBundleId;
                }

                if ((additionalCapabilities.Udid != null || ConfigProvider.IOS_DEVICE_UDID != null) && !ConfigProvider.USE_SAUCE_LABS)
                {
                    iosCapabilities.Udid = additionalCapabilities.Udid ?? ConfigProvider.IOS_DEVICE_UDID;
                }

                capabilities.Ios = iosCapabilities;
                break;

            default:
                throw new ArgumentException("Unsupported platform type");
        }

        if (ConfigProvider.USE_SAUCE_LABS)
        {
            // Add Sauce Labs specific capabilities
            // Assuming there is a dictionary or a similar property to hold these in capabilities
        }

        return capabilities;
    }
}