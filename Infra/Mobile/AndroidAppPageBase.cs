using OpenQA.Selenium.Appium.Android;

public class AndroidAppPageBase : AppPageBase
{
    public override PlatformName platform => PlatformName.ANDROID;

    public string? AndroidAppPackage
    {
        get
        {
            return ConfigProvider.ANDROID_APP_PACKAGE;
        }
    }

    public string? AndroidAppActivity
    {
        get
        {
            return ConfigProvider.ANDROID_DEFAULT_ACTIVITY;
        }
    }

    public void PressKeyCode(int keyCode)
    {
        ((AndroidDriver)Driver).PressKeyCode(keyCode);
    }

}