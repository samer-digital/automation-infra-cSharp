namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture, Description("Google maps tests")]
public class GoogleMapsTests : BaseTest
{

    [Test, Description("Test - search 4 different addresses and validate the results.")]
    [TestCase("X53R+F4 Çamlıhemşin, Rize")] // Can check also the image url per address
    [TestCase("XV2V+9C Lod, Israel")]
    [TestCase("G95C+PP Berlin, Germany")]
    [TestCase("3MX4+X7 Rothenthurm, Switzerland")]
    public async Task SearchAddressesInGoogleMaps(string address)
    {
        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(address);
        await googleMapsPage.ClickSearchButton();

        var searchResultsComponent = googleMapsPage.GetSearchResultsComponent();
        await searchResultsComponent.WaitForLocationTitleToBeVisible();
        string? locationTitle = await searchResultsComponent.GetLocationTitle();
        string code = Utils.ExtractFirstWord(address);
        Assert.That(locationTitle, Is.EqualTo(code));
    }

    [Test, Description("Negative Test - address that not exists and validate results.")]
    public async Task InvalidAddressSearch()
    {
        var invalidAddress = "Invalid Address Test 12345";

        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(invalidAddress);
        await googleMapsPage.ClickSearchButton();

        var searchResultsComponent = googleMapsPage.GetSearchResultsComponent();
        await searchResultsComponent.WaitForErrorToBeVisible();
        var errorMessage = await searchResultsComponent.GetErrorText();
        Assert.That(errorMessage, Does.Contain(invalidAddress), "Could not find the address...");
    }
}