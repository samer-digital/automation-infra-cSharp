using Microsoft.Playwright;

public class GoogleMapsPage : PageBase
{
    private ILocator _menuOpen;
    private ILocator _searchInputLocator;
    private ILocator _searchButton;
    private ILocator _directionButton;

    public GoogleMapsPage(IPage page) : base(page)
    {
        _menuOpen = page.Locator("button[jsaction='navigationrail.more']");
        _searchInputLocator = page.Locator(".searchboxinput");
        _searchButton = page.Locator("#searchbox-searchbutton");
        _directionButton = page.Locator(".hArJGc");
    }



    public override string PageUrl => BaseUrl;


    public SearchResultsComponent GetSearchResultsComponent()
    {
        return new SearchResultsComponent(this);
    }

    public MenuComponent GetMenuComponent() 
    {
        return new MenuComponent(this);
    }

    public async Task FillSearchAsync(string searchTerm)
    {
        await _searchInputLocator.FillAsync(searchTerm);
        await Page.WaitForTimeoutAsync(3000);
        await _searchButton.ClickAsync();
    }

    public async Task ClickToOpenMenu()
    {
        await _menuOpen.ClickAsync();
    }
 
    public async Task ClickDirectionBtn()
    {
        await _directionButton.ClickAsync();
    }

    public async Task ClickSearchButton()
    {
        await _searchButton.ClickAsync();
    }
}