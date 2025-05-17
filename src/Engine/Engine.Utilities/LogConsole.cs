using System.Runtime.InteropServices;

namespace EvoX.Engine.Utilities;
/// <summary>
/// Provides functionality to allocate, display, and position a console window for applications (e.g., WPF apps).
/// </summary>
internal static class LogConsole
{
    /// <summary>
    /// Allocates a new console for the calling process.
    /// </summary>
    /// <returns><c>true</c> if the console was successfully allocated; otherwise, <c>false</c>.</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AllocConsole();
    /// <summary>
    /// Retrieves the window handle used by the console associated with the calling process.
    /// </summary>
    /// <returns>The handle to the console window, or <c>IntPtr.Zero</c> if there is no associated console.</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetConsoleWindow();
    /// <summary>
    /// Changes the position and dimensions of the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="X">The new position of the left side of the window.</param>
    /// <param name="Y">The new position of the top of the window.</param>
    /// <param name="nWidth">The new width of the window.</param>
    /// <param name="nHeight">The new height of the window.</param>
    /// <param name="bRepaint">Indicates whether the window should be repainted.</param>
    /// <returns><c>true</c> if successful; otherwise, <c>false</c>.</returns>
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    /// <summary>
    /// Allocates and configures the console window.
    /// </summary>
    internal static void Start()
    {
        if (GetConsoleWindow() != IntPtr.Zero)
            return; // Already enabled
        AllocConsole();
        IntPtr handle = GetConsoleWindow();
        MoveWindow(handle, 20, 70, 1280, 720, true);
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        Console.SetError(new StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
        Console.WriteLine("Console logging enabled.\n");
    }
}