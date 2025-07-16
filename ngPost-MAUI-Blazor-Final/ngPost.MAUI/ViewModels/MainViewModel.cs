using CommunityToolkit.Mvvm.ComponentModel;
using ngPost.MAUI.Services;

namespace ngPost.MAUI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private string title = "ngPost v4.3 - Usenet Binary Poster";

    [ObservableProperty]
    private bool isLoading = false;

    public MainViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public async Task InitializeAsync()
    {
        IsLoading = true;
        
        // Initialize application settings
        await Task.Delay(500); // Simulate initialization
        
        IsLoading = false;
    }
}

