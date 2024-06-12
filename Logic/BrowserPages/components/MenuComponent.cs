using Microsoft.Playwright;

public class MenuComponent : ComponentBase
{
    public MenuComponent(PageBase pageBase) : base(pageBase) { }

    private ILocator _closeMenu => _page.Locator("button[jsaction='settings.close']");
    private ILocator _languagesBtn => _page.Locator("button[jsaction='settings.languages']");


    public async Task ClickToCloseMenu()
    {
        await _closeMenu.ClickAsync();
    }

    public async Task ClickLanguagesBtn()
    {
        await _languagesBtn.ClickAsync();
    }

    public async Task SelectLanguage(string language){
        await _page.GetByText(language).ClickAsync();
    }
}