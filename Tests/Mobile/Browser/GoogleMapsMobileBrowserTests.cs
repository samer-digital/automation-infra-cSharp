namespace MobileBrowser;

[Parallelizable(ParallelScope.Self)]
[TestFixture, Description("Google maps tests")]
public class GoogleMapsMobileBrowserTests : BaseTest
{

    [OneTimeSetUp]
    public void CheckBrowserType()
    {
        if (ConfigProvider.BROWSER == "firefox")
        {
            Assert.Ignore("Mobile tests are not applicable for Firefox.");
        }
    }

    [Test, Description("Test - search 4 different addresses and validate the results.")]
    [TestCase("X53R+F4 Çamlıhemşin, Rize")] // Can check also the image url per address
    [TestCase("XV2V+9C Lod, Israel")]
    [TestCase("G95C+PP Berlin, Germany")]
    [TestCase("3MX4+X7 Rothenthurm, Switzerland")]
    public async Task SearchAddressesMobileTests(string address)
    {
        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMobilePage(page), new GetPageOptions { ContextKey = "mobile", ShouldNavigate = true });
        await googleMapsPage.ClickKeepUsingWebBtn();
        await googleMapsPage.FillSearchAsync(address);

        var searchResultsComponent = googleMapsPage.GetSearchResultsComponent();
        await searchResultsComponent.WaitForLocationTitleToBeVisible();
        string? locationTitle = await searchResultsComponent.GetLocationTitle();
        string code = Utils.ExtractFirstWord(address);
        Assert.That(locationTitle, Is.EqualTo(code));
    }

    [Test, Description("Negative Test - address that not exists and validate results.")]
    public async Task InvalidAddressSearchMobileTest()
    {
        var invalidAddress = "Invalid Address Test 12345";

        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMobilePage(page), new GetPageOptions { ContextKey = "mobile", ShouldNavigate = true });
        await googleMapsPage.ClickKeepUsingWebBtn();

        await googleMapsPage.FillSearchAsync(invalidAddress);
        string? errorText = await googleMapsPage.IsNoResultPopupVisible();
        Assert.That(errorText, Is.EqualTo("No results found for your search."), "Could not find the error message for no Results...");
    }
}