using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

public static class Logger
{
    public static readonly ILogger Instance;

    static Logger()
    {
        var logLevel = GetLogLevel(ConfigProvider.LOG_LEVEL);
        
        var loggerConfiguration = new LoggerConfiguration()
            .Enrich.WithProperty("Service", ConfigProvider.NAME)
            .Enrich.WithProperty("Version", ConfigProvider.VERSION)
            .Enrich.FromLogContext()
            .MinimumLevel.Is(logLevel);

        if (ConfigProvider.LOG_COLORIZE)
        {
            loggerConfiguration = loggerConfiguration.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}");
        }
        else if (ConfigProvider.LOG_FORMAT == "json")
        {
            loggerConfiguration = loggerConfiguration.WriteTo.Console(new JsonFormatter());
        }
        else
        {
            loggerConfiguration = loggerConfiguration.WriteTo.Console();
        }

        Instance = loggerConfiguration.CreateLogger();
    }

    private static LogEventLevel GetLogLevel(string logLevel)
    {
        return logLevel switch
        {
            "Verbose" => LogEventLevel.Verbose,
            "Debug" => LogEventLevel.Debug,
            "Information" => LogEventLevel.Information,
            "Warning" => LogEventLevel.Warning,
            "Error" => LogEventLevel.Error,
            "Fatal" => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };
    }
}