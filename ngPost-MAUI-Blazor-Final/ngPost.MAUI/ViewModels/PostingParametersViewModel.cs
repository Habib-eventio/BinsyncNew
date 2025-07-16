using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ngPost.MAUI.Models;
using ngPost.MAUI.Services;
using System.Collections.ObjectModel;

namespace ngPost.MAUI.ViewModels;

public partial class PostingParametersViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private PostingSettings settings = new();

    [ObservableProperty]
    private ObservableCollection<string> availableLanguages = new()
    {
        "EN", "FR", "DE", "ES", "NL", "PT", "ZH"
    };

    [ObservableProperty]
    private bool isLoading = false;

    public PostingParametersViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public async Task InitializeAsync()
    {
        IsLoading = true;
        Settings = await _settingsService.LoadPostingSettingsAsync();
        IsLoading = false;
    }

    [RelayCommand]
    private async Task SaveSettingsAsync()
    {
        await _settingsService.SavePostingSettingsAsync(Settings);
    }

    [RelayCommand]
    private async Task SelectNzbDestinationAsync()
    {
        // In a real implementation, this would open a folder picker dialog
        // For demo purposes, just simulate the action
        await Task.Delay(100);
    }

    [RelayCommand]
    private void ResetToDefaults()
    {
        Settings = new PostingSettings();
    }
}

