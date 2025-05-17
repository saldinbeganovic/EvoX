
using EvoX.Engine.Utilities;
using EvoX.Hexbound;
using Microsoft.Xna.Framework;
using NLog;

class Program
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private static Hexbound? _game;

    static void Main(string[] args)
    {
        Logging.ConfigureLogging("Hexbound", true);

        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            _logger.Fatal(e.ExceptionObject as Exception, "Unhandled domain-level exception.");
            Shutdown(isCrash: true);
        };

        _logger.Info("Application starting at {time}...", DateTime.Now);

        _game = new Hexbound();
        _game.Exiting += GameShuttingDown;
        _game.Window.Title = "Hexbound";              

        try
        {
            _game.Run();
        }
        catch (Exception ex)
        {
            _logger.Fatal(ex, "Unhandled exception occurred. The application will shut down.");
            Shutdown(isCrash: true);
        }
    }

    private static void GameShuttingDown(object? sender, ExitingEventArgs e)
    {
        Shutdown();
    }

    private static void Shutdown(bool isCrash = false)
    {
        if (LogManager.Configuration != null)
        {
            if (isCrash)
                _logger.Info("Application shutting down due to fatal error at {time}...", DateTime.Now);
            else
                _logger.Info("Application shutdown at {time}...", DateTime.Now);

            LogManager.Shutdown();
            Environment.Exit(isCrash ? 1 : 0);
        }
    }
}