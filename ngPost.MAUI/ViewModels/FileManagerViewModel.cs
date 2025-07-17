using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ngPost.MAUI.Models;
using ngPost.MAUI.Services;
using System.Collections.ObjectModel;

namespace ngPost.MAUI.ViewModels;

public partial class FileManagerViewModel : ObservableObject
{
    private readonly IFileManagerService _fileManagerService;
    private readonly ICompressionService _compressionService;
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private ObservableCollection<FileToPost> files = new();

    [ObservableProperty]
    private CompressionSettings compressionSettings = new();

    [ObservableProperty]
    private string nzbFilePath = "d5/nzb/ngPost_v4.3_x64_setup/ngPost_v4.3_x64_setup.nzb";

    [ObservableProperty]
    private string nzbPassword = string.Empty;

    [ObservableProperty]
    private bool showDebugInfo = false;

    [ObservableProperty]
    private bool isLoading = false;

    public FileManagerViewModel(
        IFileManagerService fileManagerService,
        ICompressionService compressionService,
        ISettingsService settingsService)
    {
        _fileManagerService = fileManagerService;
        _compressionService = compressionService;
        _settingsService = settingsService;
    }

    public async Task InitializeAsync()
    {
        IsLoading = true;
        CompressionSettings = await _settingsService.LoadCompressionSettingsAsync();
        IsLoading = false;
    }

    [RelayCommand]
    private async Task SelectFilesAsync()
    {
        var selectedFiles = await _fileManagerService.SelectFilesAsync();
        
        foreach (var file in selectedFiles)
        {
            Files.Add(file);
        }
    }

    [RelayCommand]
    private async Task SelectFolderAsync()
    {
        var folderFiles = await _fileManagerService.SelectFolderAsync();
        
        foreach (var file in folderFiles)
        {
            Files.Add(file);
        }
    }

    [RelayCommand]
    private void RemoveFile(FileToPost file)
    {
        Files.Remove(file);
    }

    [RelayCommand]
    private void RemoveAllFiles()
    {
        Files.Clear();
    }

    [RelayCommand]
    private async Task SaveCompressionSettingsAsync()
    {
        await _settingsService.SaveCompressionSettingsAsync(CompressionSettings);
    }

    [RelayCommand]
    private async Task ValidateRarPathAsync()
    {
        var isValid = await _compressionService.ValidateRarPathAsync(CompressionSettings.RarPath);
        // In a real app, you would show validation result to user
    }

    [RelayCommand]
    private async Task SelectCompressPathAsync()
    {
        // In a real implementation, this would open a folder picker dialog
        await Task.Delay(100);
    }

    [RelayCommand]
    private async Task SelectRarPathAsync()
    {
        // In a real implementation, this would open a file picker dialog
        await Task.Delay(100);
    }
}

