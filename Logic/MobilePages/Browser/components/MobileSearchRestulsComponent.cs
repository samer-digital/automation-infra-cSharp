using Microsoft.Playwright;

public class SearchResultsMobileComponent : ComponentBase
{

    public SearchResultsMobileComponent(PageBase pageBase) : base(pageBase) { }

    private ILocator _locationTitle => _page.Locator("h1[class='DUwDvf fontTitleLarge']");
    private ILocator _directionBtn => _page.Locator("button[data-value='Directions']");
    public async Task<string?> GetLocationTitle()
    {
        return await _locationTitle.TextContentAsync();
    }

    public async Task WaitForLocationTitleToBeVisible()
    {
        await PageBase.WaitForElementToBeVisibleAsync(_locationTitle);
    }

    public async Task ClickDirectionBtn()
    {
        await _directionBtn.ClickAsync();
    }
}