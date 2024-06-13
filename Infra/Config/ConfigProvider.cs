using DotNetEnv;

public static class ConfigProvider
{

    static ConfigProvider()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePathConfig = Path.Combine(currentDirectory, @"../../../../.env");
        string fullPathConfig = Path.GetFullPath(filePathConfig);
        Env.Load(fullPathConfig);

        string filePathSecrets = Path.Combine(currentDirectory, @"../../../../.env.secret");
        string fullPathSecrets = Path.GetFullPath(filePathSecrets);
        Env.Load(fullPathSecrets);
        _sauceLabsBuildId = Environment.GetEnvironmentVariable("SAUCE_LABS_BUILD_ID") ?? $"{NAME}-{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}";
        _sauceLabCacheId = Environment.GetEnvironmentVariable("SAUCE_LAB_CACHE_ID") ?? Utils.GenerateRandomString(10);
    }

    private static readonly string _sauceLabsBuildId;
    public static string SAUCE_LABS_BUILD_ID => _sauceLabsBuildId;

    private static readonly string _sauceLabCacheId;
    public static string SAUCE_LAB_CACHE_ID => _sauceLabCacheId;

    public static string NAME => Environment.GetEnvironmentVariable("NAME") ?? "samer-framework-dotnet";
    public static string VERSION => Environment.GetEnvironmentVariable("VERSION") ?? "0.0.1";
    public static bool LOG_COLORIZE => bool.Parse(Environment.GetEnvironmentVariable("LOG_COLORIZE") ?? "false");
    public static string LOG_FORMAT => Environment.GetEnvironmentVariable("LOG_FORMAT") ?? "pretty";
    public static string LOG_LEVEL => Environment.GetEnvironmentVariable("LOG_LEVEL") ?? "info";
    
    public static string BROWSER => Environment.GetEnvironmentVariable("BROWSER") ?? "chromium";
    public static string WEBSITE_BASE_URL => Environment.GetEnvironmentVariable("WEBSITE_BASE_URL") ?? "";
    public static string API_BASE_URL => Environment.GetEnvironmentVariable("API_BASE_URL") ?? "";

    public static string PG_DB_HOST => Environment.GetEnvironmentVariable("PG_DB_HOST") ?? "localhost";
    public static int PG_DB_PORT => int.Parse(Environment.GetEnvironmentVariable("PG_DB_PORT") ?? "5432");
    public static string? PG_DB_NAME => Environment.GetEnvironmentVariable("PG_DB_NAME");
    public static string? PG_DB_USER => Environment.GetEnvironmentVariable("PG_DB_USER");
    public static string? PG_DB_PASSWORD => Environment.GetEnvironmentVariable("PG_DB_PASSWORD")!;

    public static string MYSQL_DB_HOST => Environment.GetEnvironmentVariable("MYSQL_DB_HOST") ?? "localhost";
    public static string MYSQL_DB_PORT => Environment.GetEnvironmentVariable("MYSQL_DB_PORT") ?? "3306";
    public static string? MYSQL_DB_NAME => Environment.GetEnvironmentVariable("MYSQL_DB_NAME");
    public static string? MYSQL_DB_USER => Environment.GetEnvironmentVariable("MYSQL_DB_USER")!;
    public static string? MYSQL_DB_PASSWORD => Environment.GetEnvironmentVariable("MYSQL_DB_PASSWORD")!;

    public static string MONGO_DB_HOST => Environment.GetEnvironmentVariable("MONGO_DB_HOST") ?? "localhost";
    public static int MONGO_DB_PORT => int.Parse(Environment.GetEnvironmentVariable("MONGO_DB_PORT") ?? "27017");
    public static string? MONGO_DB_NAME => Environment.GetEnvironmentVariable("MONGO_DB_NAME");
    public static string? MONGO_DB_USER => Environment.GetEnvironmentVariable("MONGO_DB_USER");
    public static string? MONGO_DB_PASSWORD => Environment.GetEnvironmentVariable("MONGO_DB_PASSWORD");

    public static string? EMAIL_SMTP_HOST => Environment.GetEnvironmentVariable("EMAIL_SMTP_HOST");
    public static int EMAIL_SMTP_PORT => int.Parse(Environment.GetEnvironmentVariable("EMAIL_SMTP_PORT") ?? "587");
    public static string? EMAIL_ADDRESS => Environment.GetEnvironmentVariable("EMAIL_ADDRESS");
    public static string? EMAIL_PASSWORD => Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

    public static string APPIUM_HOST => Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "localhost";
    public static string? APPIUM_BASE_URL => Environment.GetEnvironmentVariable("APPIUM_BASE_URL");
    public static int APPIUM_PORT => int.Parse(Environment.GetEnvironmentVariable("APPIUM_PORT") ?? "4723");
    public static string APPIUM_PATH => Environment.GetEnvironmentVariable("APPIUM_PATH") ?? "/";

    public static string? ANDROID_DEVICE_NAME => Environment.GetEnvironmentVariable("ANDROID_DEVICE_NAME");
    public static string? ANDROID_PLATFORM_VERSION => Environment.GetEnvironmentVariable("ANDROID_PLATFORM_VERSION");
    public static string ANDROID_AUTOMATION_NAME => Environment.GetEnvironmentVariable("ANDROID_AUTOMATION_NAME") ?? "UiAutomator2";
    public static string? ANDROID_APP_PACKAGE => Environment.GetEnvironmentVariable("ANDROID_APP_PACKAGE");
    public static string? ANDROID_APP_PATH => Environment.GetEnvironmentVariable("ANDROID_APP_PATH");
    public static string? ANDROID_DEFAULT_ACTIVITY => Environment.GetEnvironmentVariable("ANDROID_DEFAULT_ACTIVITY");
    public static bool AUTO_GRANT_PERMISSION => bool.Parse(Environment.GetEnvironmentVariable("AUTO_GRANT_PERMISSION") ?? "false");
    
    public static string? IOS_DEVICE_NAME => Environment.GetEnvironmentVariable("IOS_DEVICE_NAME");
    public static string? IOS_PLATFORM_VERSION => Environment.GetEnvironmentVariable("IOS_PLATFORM_VERSION");
    public static string? IOS_DEVICE_UDID => Environment.GetEnvironmentVariable("IOS_DEVICE_UDID");
    public static string? IOS_BUNDLE_ID => Environment.GetEnvironmentVariable("IOS_BUNDLE_ID");
    public static string? IOS_APP_PATH => Environment.GetEnvironmentVariable("IOS_APP_PATH");
    public static string IOS_AUTOMATION_NAME => Environment.GetEnvironmentVariable("IOS_AUTOMATION_NAME") ?? "XCUITest";
    public static bool AUTO_ACCEPT_ALERTS => bool.Parse(Environment.GetEnvironmentVariable("AUTO_ACCEPT_ALERTS") ?? "false");

    public static bool NO_RESET => bool.Parse(Environment.GetEnvironmentVariable("NO_RESET") ?? "true");
    public static bool USE_SAUCE_LABS => bool.Parse(Environment.GetEnvironmentVariable("USE_SAUCE_LABS") ?? "false");
    public static string? SAUCE_LABS_USERNAME => Environment.GetEnvironmentVariable("SAUCE_LABS_USERNAME");
    public static string? SAUCE_LABS_ACCESS_KEY => Environment.GetEnvironmentVariable("SAUCE_LABS_ACCESS_KEY");
    public static int SESSION_CREATION_CONNECTION => int.Parse(Environment.GetEnvironmentVariable("SESSION_CREATION_CONNECTION") ?? "500000");
    public static string SAUCE_LAB_APPIUM_VERSION => Environment.GetEnvironmentVariable("SAUCE_LAB_APPIUM_VERSION") ?? "2.0.0";
}