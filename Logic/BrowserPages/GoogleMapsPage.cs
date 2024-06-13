using Microsoft.Playwright;

public class GoogleMapsPage : PageBase
{
    private ILocator _menuOpen;
    private ILocator _searchInputLocator;
    private ILocator _searchButton;
    private ILocator _directionButton;
    private ILocator _currentLocationBtn;

    public GoogleMapsPage(IPage page) : base(page)
    {
        _menuOpen = page.Locator("button[jsaction='navigationrail.more']");
        _searchInputLocator = page.Locator(".searchboxinput");
        _searchButton = page.Locator("#searchbox-searchbutton");
        _directionButton = page.Locator(".hArJGc");
        _currentLocationBtn = page.Locator(".sVuEFc");
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

    public DirectionComponent GetDirectionComponent()
    {
        return new DirectionComponent(this);
    }

    public async Task FillSearchAsync(string searchTerm)
    {
        await _searchInputLocator.FillAsync(searchTerm);
    }

    public async Task ClickToOpenMenu()
    {
        await _menuOpen.ClickAsync();
    }

    public async Task ClickDirectionBtn()
    {
        await _directionButton.ClickAsync();
    }

    public async Task ClickOnGetCurrentLocationBtn()
    {
        await _currentLocationBtn.ClickAsync();
    }

    public async Task ClickSearchButton()
    {
        await _searchButton.ClickAsync();
    }
}