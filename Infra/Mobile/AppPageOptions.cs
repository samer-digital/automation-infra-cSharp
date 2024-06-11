public class GetAppPageOptions
{
    public string? ContextKey { get; set; }
    public bool ShouldRestartDriver { get; set; }
    public string? DeviceName { get; set; }
    public DeviceOrientation DeviceOrientation { get; set; }
    public string? PlatformVersion { get; set; }
    public string? Udid { get; set; }
    public string? TestName { get; set; }
}