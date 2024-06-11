public class BrowserOptions
{
    public bool Headless { get; set; }
    public int SlowMo { get; set; }
    public string[] Args { get; set; } = Array.Empty<string>();
    public int Timeout { get; set; }
}