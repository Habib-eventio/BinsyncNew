using CommunityToolkit.Mvvm.ComponentModel;

namespace ngPost.MAUI.Models;

public partial class LogEntry : ObservableObject
{
    [ObservableProperty]
    private DateTime timestamp;

    [ObservableProperty]
    private string message = string.Empty;

    [ObservableProperty]
    private LogLevel level;

    public LogEntry()
    {
        Timestamp = DateTime.Now;
    }

    public LogEntry(string message, LogLevel level = LogLevel.Info)
    {
        Timestamp = DateTime.Now;
        Message = message;
        Level = level;
    }

    public string FormattedTimestamp => Timestamp.ToString("HH:mm:ss");
}

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error
}

