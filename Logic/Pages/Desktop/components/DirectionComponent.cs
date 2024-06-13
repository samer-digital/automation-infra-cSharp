using Microsoft.Playwright;

public class DirectionComponent : ComponentBase
{
    private const string DirectionsSelector = "div[class='m6QErb XiKgde '] > div";

    public DirectionComponent(PageBase pageBase) : base(pageBase) { }
    private ILocator FirstDirectionRoute => _page.Locator("#section-directions-trip-0");
    private ILocator YourLocationSearchInput => _page.Locator("input[aria-controls='sbsg50']");
    private ILocator ErrorNoRoutes => _page.Locator("div[aria-live='assertive']");

    public async Task<int> GetNumOfDirections()
    {
        await WaitForDirectionsToBeVisible();
        var elements = await _page.QuerySelectorAllAsync(DirectionsSelector);
        return new List<IElementHandle>(elements).Count();
    }

    public async Task FillYourLocationSearchAsync(string searchTerm)
    {
        await YourLocationSearchInput.FillAsync(searchTerm);
    }

    public ILocator GetErrorMessageLocator(string text) // ???
    {
        return _page.GetByRole(AriaRole.Heading, new() { Name = "Google Maps can't find " + text });
    }

    public async Task<bool> IsErrorNoRouteVisible(string errorText)
    {
        return await ErrorNoRoutes.GetByText(errorText).IsVisibleAsync();
    }

    public async Task<string?> GetErrorNoRouteText()
    {
        // await ErrorNoRoutes.WaitForAsync();
        return await ErrorNoRoutes.TextContentAsync();
    }

    public async Task WaitForDirectionsToBeVisible()
    {
        await FirstDirectionRoute.WaitForAsync();
    }
}