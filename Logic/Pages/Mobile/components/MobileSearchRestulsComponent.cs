using Microsoft.Playwright;

public class SearchResultsMobileComponent : ComponentBase
{

    public SearchResultsMobileComponent(PageBase pageBase) : base(pageBase) { }

    private ILocator LocationTitle => _page.Locator("h1[class='DUwDvf fontTitleLarge']");
    private ILocator DirectionBtn => _page.Locator("button[data-value='Directions']");
    public async Task<string?> GetLocationTitle()
    {
        return await LocationTitle.TextContentAsync();
    }

    public async Task WaitForLocationTitleToBeVisible()
    {
        await PageBase.WaitForElementToBeVisibleAsync(LocationTitle);
    }

    public async Task ClickDirectionBtn()
    {
        await DirectionBtn.ClickAsync();
    }
}