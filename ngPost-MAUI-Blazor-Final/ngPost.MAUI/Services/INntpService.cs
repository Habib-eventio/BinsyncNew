using ngPost.MAUI.Models;

namespace ngPost.MAUI.Services;

public interface INntpService
{
    event EventHandler<LogEntry> LogEntryAdded;
    event EventHandler<double> ProgressUpdated;
    event EventHandler<string> StatusUpdated;

    Task<bool> TestConnectionAsync(Server server);
    Task StartPostingAsync(IEnumerable<FileToPost> files, IEnumerable<Server> servers, PostingSettings settings);
    Task PausePostingAsync();
    Task ResumePostingAsync();
    Task StopPostingAsync();
    
    bool IsPosting { get; }
    double CurrentProgress { get; }
    string CurrentStatus { get; }
    int AvailableConnections { get; }
    double AverageSpeed { get; }
}

