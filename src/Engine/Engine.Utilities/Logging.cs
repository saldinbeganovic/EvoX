using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace EvoX.Engine.Utilities;

public static class Logging
{
    private static readonly string _layout = "${longdate} [${level:uppercase=true}] ${logger}: ${message} ${onexception:\n ---> ${exception:format=message:maxInnerExceptionLevel=5:innerFormat=message:innerExceptionSeparator=\n ---> }}";
    /// <summary>
    /// Initialize logging.
    /// </summary>
    public static void ConfigureLogging(string fileName, bool debugConsole) //TODO: Config
    {
        string logfilePath = Path.Join( Directory.CreateDirectory("./logs").FullName, $"{fileName}_log.txt");
        string logArchivefilePath = Path.Join(Directory.CreateDirectory("./logs/archive").FullName, $"{fileName}_{DateTime.Now:yyyyMMdd}_{{###}}.txt");

        NLog.Config.LoggingConfiguration config = new NLog.Config.LoggingConfiguration();
        FileTarget logfile = new FileTarget("logfile")
        {
            FileName = logfilePath,
            Layout = _layout,
            KeepFileOpen = true,
            OpenFileCacheTimeout = 30,
            AutoFlush = true,
            ConcurrentWrites = false,
            ArchiveOldFileOnStartup = true,
            ArchiveAboveSize = 1000000,
            ArchiveNumbering = ArchiveNumberingMode.Rolling,
            MaxArchiveFiles = 100,
            ArchiveFileName = logArchivefilePath
        };

        ColoredConsoleTarget logconsole = new ColoredConsoleTarget("logconsole")
        {
            Layout =_layout
        };

        logconsole.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
        {
            Condition = "level == LogLevel.Debug",
            ForegroundColor = ConsoleOutputColor.Cyan
        });

        LimitingTargetWrapper consoleLimiter = new LimitingTargetWrapper("limitedConsole", logconsole)
        {
            Interval = TimeSpan.FromSeconds(1),
            MessageLimit = 100
        };

        config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleLimiter);
        config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);


        // Apply config           
        LogManager.Configuration = config;

        if (debugConsole)
            LogConsole.Start();
    }
}
