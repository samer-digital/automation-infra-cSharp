using Microsoft.Playwright;

public abstract class GoogleMapsMenuPageBase : PageBase
{
    protected GoogleMapsMenuPageBase(IPage page) : base(page) { }

    // Common locators
    protected ILocator MenuOpenBtn => Page.Locator("button[aria-label='Menu']");
    protected ILocator DirectionBtn => Page.Locator("button[aria-label='Directions']");


    public async Task ClickOnTheMenuOpenBtn()
    {
        await MenuOpenBtn.ClickAsync();
    }


    public async Task ClickDirectionBtn()
    {
        await DirectionBtn.ClickAsync();
    }
}
