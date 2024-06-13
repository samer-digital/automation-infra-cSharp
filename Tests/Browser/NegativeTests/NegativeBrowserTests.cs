namespace Browser;

[Parallelizable(ParallelScope.Self)]
[TestFixture, Description("Google Maps Negative Tests"), Category("Negative")]
public class NegativeBrowserTests : BaseTest
{

    public string invalidAddress = "Invalid Address Test 12345";

    [Test, Description("Negative Test - address that not exists and validate results.")]
    public async Task InvalidAddressSearchBrowserTest()
    {
        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(invalidAddress);
        await googleMapsPage.ClickSearchButton();

        var searchResultsComponent = googleMapsPage.GetSearchResultsComponent();
        await searchResultsComponent.WaitForErrorToBeVisible();
        var errorMessage = await searchResultsComponent.GetErrorText();
        Assert.That(errorMessage, Does.Contain(invalidAddress), "Could not find the error...");
    }

    [Test, Description("Test searching for an address using an extremely long string.")]
    public async Task SearchWithLongString()
    {
        var longString = new string('a', 1000);
        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = true });

        await googleMapsPage.FillSearchAsync(longString);
        await googleMapsPage.ClickSearchButton();

        var searchResultsComponent = googleMapsPage.GetSearchResultsComponent();
        await searchResultsComponent.WaitForErrorToBeVisible();
        var errorMessage = await searchResultsComponent.GetErrorText();
        Assert.That(errorMessage, Does.Contain(longString), "Could not find the error...");
    }

    [Test, Description("Test navigating to a non-existent address and validate the error message.")]
    public async Task NavigateToNonExistentAddress()
    {
        var addressDestination = "G95C+PP Berlin, Germany";

        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(addressDestination);
        await googleMapsPage.ClickDirectionBtn();

        var directionComponent = googleMapsPage.GetDirectionComponent();
        await directionComponent.FillYourLocationSearchAsync(invalidAddress);
        await googleMapsPage.KeyboardPress("Enter");

        await Expect(directionComponent.GetErrorMessageLocator(invalidAddress)).ToBeVisibleAsync(); // ???
    }


    [Test, Description("Test navigating to a non-existent address and validate the error message.")]
    public async Task NavigateWithNoRoutes()
    {
        var addressDestination = "Cuba";
        var addressStart = "sadfasdf, asdfas, 34500 asdfasdf/İstanbul";

        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = true });
        await googleMapsPage.FillSearchAsync(addressDestination);
        await googleMapsPage.ClickDirectionBtn();

        var directionComponent = googleMapsPage.GetDirectionComponent();
        await directionComponent.FillYourLocationSearchAsync(addressStart);
        await googleMapsPage.KeyboardPress("Enter");
        string? textError = await directionComponent.GetErrorNoRouteText();

        Assert.That(textError, Is.EqualTo($"Sorry, we could not calculate directions from \"{addressStart}\" to \"{addressDestination}\""), "Could not find the error...");
    }

}