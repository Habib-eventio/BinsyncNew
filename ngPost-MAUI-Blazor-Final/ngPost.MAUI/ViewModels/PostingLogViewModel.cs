using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ngPost.MAUI.Models;
using ngPost.MAUI.Services;
using System.Collections.ObjectModel;

namespace ngPost.MAUI.ViewModels;

public partial class PostingLogViewModel : ObservableObject
{
    private readonly INntpService _nntpService;
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private ObservableCollection<LogEntry> logEntries = new();

    [ObservableProperty]
    private double currentProgress = 0;

    [ObservableProperty]
    private bool isPosting = false;

    [ObservableProperty]
    private string currentStatus = "Ready";

    [ObservableProperty]
    private int availableConnections = 0;

    [ObservableProperty]
    private double averageSpeed = 0;

    [ObservableProperty]
    private string uploadSize = "0 MB";

    [ObservableProperty]
    private string uploadTime = "00:00:00";

    public PostingLogViewModel(INntpService nntpService, ISettingsService settingsService)
    {
        _nntpService = nntpService;
        _settingsService = settingsService;

        // Subscribe to NNTP service events
        _nntpService.LogEntryAdded += OnLogEntryAdded;
        _nntpService.ProgressUpdated += OnProgressUpdated;
        _nntpService.StatusUpdated += OnStatusUpdated;
    }

    public async Task InitializeAsync()
    {
        // Initialize with some sample log entries
        LogEntries.Add(new LogEntry("ngPost_v4.3_x86_setup.nzb"));
        LogEntries.Add(new LogEntry("file: ngPost_v4.3_x64_setup.nzb, rar name: ngPost_v4.3_x86_setup"));
        LogEntries.Add(new LogEntry(""));
        LogEntries.Add(new LogEntry("Start Post #13:"));
        LogEntries.Add(new LogEntry("ngPost_v4.3_x64_setup.nzb"));
        LogEntries.Add(new LogEntry(""));
        LogEntries.Add(new LogEntry("Compressing files..."));
        LogEntries.Add(new LogEntry(""));
        LogEntries.Add(new LogEntry("*****"));

        AvailableConnections = 60;
        UploadSize = "10.93 MB";
        UploadTime = "00:00:02.255";
        AverageSpeed = 4.85;

        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task StartPostingAsync()
    {
        // In a real implementation, this would get files and servers from other ViewModels
        // For demo purposes, create some sample data
        var files = new List<FileToPost>
        {
            new FileToPost("ngPost_v4.3_x64_setup.nzb", "/path/to/file", 1024 * 1024 * 10)
        };

        var servers = new List<Server>
        {
            new Server("news.usenetserver.com", 563, true, 60, "", "")
        };

        var settings = await _settingsService.LoadPostingSettingsAsync();

        await _nntpService.StartPostingAsync(files, servers, settings);
    }

    [RelayCommand]
    private async Task PausePostingAsync()
    {
        if (IsPosting)
        {
            await _nntpService.PausePostingAsync();
        }
        else
        {
            await _nntpService.ResumePostingAsync();
        }
    }

    [RelayCommand]
    private async Task StopPostingAsync()
    {
        await _nntpService.StopPostingAsync();
    }

    [RelayCommand]
    private void ClearLog()
    {
        LogEntries.Clear();
    }

    private void OnLogEntryAdded(object? sender, LogEntry logEntry)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            LogEntries.Add(logEntry);
        });
    }

    private void OnProgressUpdated(object? sender, double progress)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            CurrentProgress = progress;
        });
    }

    private void OnStatusUpdated(object? sender, string status)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            CurrentStatus = status;
            IsPosting = _nntpService.IsPosting;
            AvailableConnections = _nntpService.AvailableConnections;
            AverageSpeed = _nntpService.AverageSpeed;
        });
    }
}

