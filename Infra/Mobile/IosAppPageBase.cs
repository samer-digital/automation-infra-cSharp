
public class IosAppPageBase : AppPageBase
{
    public override PlatformName platform => PlatformName.IOS;


    public string? IosBundleId
    {
        get
        {
            return ConfigProvider.IOS_BUNDLE_ID;
        }
    }

}