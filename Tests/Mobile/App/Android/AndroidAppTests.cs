namespace NativeApp;

[TestFixture, Description("Google Maps NativeApp Tests"), Category("NativeApp")]
public class GoogleMapsAppTests : BaseTest
{

    // [Test, Description("Test - Native App")]
    public async Task NavigateDirectionBrowserTest()
    {
        var mapMenuAppPage = await _testContext.GetAppPageAsync(() => new MapMenuAppPage(), new AppPageOptions
        {
            ContextKey = "default",
            ShouldRestartDriver = false,
            DeviceName = ConfigProvider.ANDROID_DEVICE_NAME,
            DeviceOrientation = DeviceOrientation.PORTRAIT,
            PlatformVersion = ConfigProvider.ANDROID_PLATFORM_VERSION,
            TestName = "Test Native App"
        });
        bool isSaveBtnVisible = await mapMenuAppPage.WaitForSaveThemeBtnToBeVisible(10000);
        if (isSaveBtnVisible)
        {
            mapMenuAppPage.ClickSaveThemeBtn();
        }
        mapMenuAppPage.TypeInSearchInput("Yalova Merkez/Yalova");
        bool isVisible = await mapMenuAppPage.WaitForImageToBeVisible(10000);
        Assert.That(isVisible);
    }

}
