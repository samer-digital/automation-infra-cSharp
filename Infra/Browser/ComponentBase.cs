using Microsoft.Playwright;

public abstract class ComponentBase
{
    protected PageBase PageBase { get; }

    protected ComponentBase(PageBase pageBase)
    {
        PageBase = pageBase;
    }

    protected IPage _page => PageBase.Page;
}