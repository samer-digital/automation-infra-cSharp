namespace Browser;

[Parallelizable(ParallelScope.Self)]
[TestFixture, Description("Google Maps Functional Tests"), Category("Functional")]
public class GoogleMapsFunctionalBrowserTests : BaseTest
{

    [Test, Description("Test - search 4 different addresses and validate the results.")]
    [TestCase("X53R+F4 Çamlıhemşin, Rize")] // Can check also the image url per address
    [TestCase("XV2V+9C Lod, Israel")]
    [TestCase("G95C+PP Berlin, Germany")]
    [TestCase("3MX4+X7 Rothenthurm, Switzerland")]
    public async Task SearchAddressesBrowserTest(string address)
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

    [Test, Description("Test - Navigate to address and get directions")]
    public async Task NavigateDirectionBrowserTest()
    {
        var addressDestination= "G95C+PP Berlin, Germany";
        var addressStart= "3MX4+X7 Rothenthurm, Switzerland";

        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(addressDestination);
        await googleMapsPage.ClickDirectionBtn();

        var searchResultsComponent = googleMapsPage.GetDirectionComponent();
        await searchResultsComponent.FillYourLocationSearchAsync(addressStart);
        await googleMapsPage.KeyboardPress("Enter");
        int numOfDirections = await searchResultsComponent.GetNumOfDirections();
        Assert.That(numOfDirections, Is.GreaterThanOrEqualTo(2), "Number of routes are less then the expected");
    }
}