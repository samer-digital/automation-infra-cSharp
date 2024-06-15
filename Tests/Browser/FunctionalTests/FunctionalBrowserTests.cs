namespace Browser;

[Parallelizable(ParallelScope.Self)]
[TestFixture, Description("Google Maps Functional Tests"), Category("Functional")]
public class FunctionalBrowserTests : BaseTest
{

    public string BerlinGermany= "G95C+PP Berlin, Germany";
    public string Switzerland= "3MX4+X7 Rothenthurm, Switzerland";

    [Test, Description("Test - search 4 different addresses and validate the results.")]
    [TestCase("X53R+F4 Çamlıhemşin, Rize")]
    [TestCase("XV2V+9C Lod, Israel")]
    [TestCase("G95C+PP Berlin, Germany")]
    [TestCase("3MX4+X7 Rothenthurm, Switzerland")]
    public async Task SearchAddressesBrowserTest(string address)
    {
        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = true });
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
        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(BerlinGermany);
        await googleMapsPage.ClickDirectionBtn();

        var searchResultsComponent = googleMapsPage.GetDirectionComponent();
        await searchResultsComponent.FillYourLocationSearchAsync(Switzerland);
        await googleMapsPage.KeyboardPress("Enter");
        int numOfDirections = await searchResultsComponent.GetNumOfDirections();
        Assert.That(numOfDirections, Is.GreaterThanOrEqualTo(2), "Number of routes are less then the expected");
    }

    [Test, Description("Test - Copy the address link and navigate")] // This test is a challenge in the ci with webkit browser :/
    public async Task CopyAddressLinkAndNavigateTest()
    {

        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(Switzerland);
        await googleMapsPage.ClickSearchButton();

        var searchResultsComponent = googleMapsPage.GetSearchResultsComponent();
        await searchResultsComponent.ClickSharehLocationBtn();
        string? link = await searchResultsComponent.getLinkToShare();
        
        if (!string.IsNullOrEmpty(link))
        {
            await googleMapsPage.CustomNavigateAsync(link);;
        }
        string code = Utils.ExtractFirstWord(Switzerland);
        Assert.That(await searchResultsComponent.GetLocationTitle(), Is.EqualTo(code));
    }

    [Test, Description("Test - Search and validate the history")]
    public async Task HistorySearchTest()
    {
        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(BerlinGermany);
        await googleMapsPage.ClickSearchButton();

        string? locationTitle = await googleMapsPage.GetFirstHistoryItemAsync();
        if (!string.IsNullOrEmpty(locationTitle)) 
        {
            locationTitle = locationTitle.Replace(" ", "");
        }
        string code = Utils.ExtractFirstWord(BerlinGermany);
        Assert.That(locationTitle, Is.EqualTo(code));;
    }
}