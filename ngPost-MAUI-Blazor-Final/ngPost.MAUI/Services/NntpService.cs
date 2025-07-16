using ngPost.MAUI.Models;
using System.Collections.Concurrent;

namespace ngPost.MAUI.Services;

public class NntpService : INntpService
{
    private readonly ConcurrentQueue<LogEntry> _logQueue = new();
    private bool _isPosting = false;
    private double _currentProgress = 0;
    private string _currentStatus = "Ready";
    private int _availableConnections = 0;
    private double _averageSpeed = 0;

    public event EventHandler<LogEntry>? LogEntryAdded;
    public event EventHandler<double>? ProgressUpdated;
    public event EventHandler<string>? StatusUpdated;

    public bool IsPosting => _isPosting;
    public double CurrentProgress => _currentProgress;
    public string CurrentStatus => _currentStatus;
    public int AvailableConnections => _availableConnections;
    public double AverageSpeed => _averageSpeed;

    public async Task<bool> TestConnectionAsync(Server server)
    {
        AddLogEntry($"Testing connection to {server.Host}:{server.Port}...");
        
        // Simulate connection test
        await Task.Delay(1000);
        
        // For demo purposes, assume connection is successful
        AddLogEntry($"Connection to {server.Host}:{server.Port} successful");
        return true;
    }

    public async Task StartPostingAsync(IEnumerable<FileToPost> files, IEnumerable<Server> servers, PostingSettings settings)
    {
        _isPosting = true;
        _currentProgress = 0;
        UpdateStatus("Starting posting...");

        var serverList = servers.Where(s => s.IsEnabled).ToList();
        _availableConnections = serverList.Sum(s => s.Connections);

        AddLogEntry($"Starting posting with {_availableConnections} connections");
        AddLogEntry($"Posting to newsgroups: {settings.NewsGroups}");

        foreach (var file in files)
        {
            file.Status = PostingStatus.Compressing;
            AddLogEntry($"Compressing {file.Name}...");
            
            // Simulate compression
            await Task.Delay(2000);
            
            file.Status = PostingStatus.Posting;
            AddLogEntry($"Posting {file.Name}...");
            
            // Simulate posting progress
            for (int i = 0; i <= 100; i += 10)
            {
                if (!_isPosting) break;
                
                _currentProgress = i;
                file.Progress = i;
                _averageSpeed = 4.85; // MB/s
                
                ProgressUpdated?.Invoke(this, _currentProgress);
                await Task.Delay(500);
            }
            
            file.Status = PostingStatus.Completed;
            AddLogEntry($"Completed posting {file.Name}");
        }

        _isPosting = false;
        _currentProgress = 100;
        UpdateStatus("Posting completed");
        AddLogEntry("All files posted successfully");
    }

    public Task PausePostingAsync()
    {
        _isPosting = false;
        UpdateStatus("Posting paused");
        AddLogEntry("Posting paused by user");
        return Task.CompletedTask;
    }

    public Task ResumePostingAsync()
    {
        _isPosting = true;
        UpdateStatus("Posting resumed");
        AddLogEntry("Posting resumed");
        return Task.CompletedTask;
    }

    public Task StopPostingAsync()
    {
        _isPosting = false;
        _currentProgress = 0;
        UpdateStatus("Posting stopped");
        AddLogEntry("Posting stopped by user");
        return Task.CompletedTask;
    }

    private void AddLogEntry(string message, LogLevel level = LogLevel.Info)
    {
        var entry = new LogEntry(message, level);
        _logQueue.Enqueue(entry);
        LogEntryAdded?.Invoke(this, entry);
    }

    private void UpdateStatus(string status)
    {
        _currentStatus = status;
        StatusUpdated?.Invoke(this, status);
    }
}

