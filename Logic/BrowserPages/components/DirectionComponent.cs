using Microsoft.Playwright;

public class DirectionComponent : ComponentBase
{
    private const string DirectionsSelector = "div[class='m6QErb XiKgde '] > div";

    public DirectionComponent(PageBase pageBase) : base(pageBase) { }
    private ILocator _directionsForm => _page.Locator("div[class='m6QErb XiKgde ']");
    private ILocator _yourLocationSearchInput => _page.Locator("input[aria-controls='sbsg50']");

    public async Task<int> GetNumOfDirections()
    {
        await WaitForDirectionsToBeVisible();
        var elements = await _page.QuerySelectorAllAsync(DirectionsSelector);
        return new List<IElementHandle>(elements).Count();
    }

    public async Task FillYourLocationSearchAsync(string searchTerm)
    {
        await _yourLocationSearchInput.FillAsync(searchTerm);
    }

    public ILocator getTextResults(string text) // ???
    {
        return _page.GetByRole(AriaRole.Heading, new() { Name = "Google Maps can't find " + text });
    }


    public async Task WaitForDirectionsToBeVisible()
    {
        await _directionsForm.WaitForAsync();
    }
}