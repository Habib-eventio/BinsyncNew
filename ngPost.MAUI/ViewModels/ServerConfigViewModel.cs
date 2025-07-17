using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ngPost.MAUI.Models;
using ngPost.MAUI.Services;
using System.Collections.ObjectModel;

namespace ngPost.MAUI.ViewModels;

public partial class ServerConfigViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;
    private readonly INntpService _nntpService;

    [ObservableProperty]
    private ObservableCollection<Server> servers = new();

    [ObservableProperty]
    private Server? selectedServer;

    [ObservableProperty]
    private string newServerHost = string.Empty;

    [ObservableProperty]
    private int newServerPort = 563;

    [ObservableProperty]
    private bool newServerSsl = true;

    [ObservableProperty]
    private int newServerConnections = 60;

    [ObservableProperty]
    private string newServerUsername = string.Empty;

    [ObservableProperty]
    private string newServerPassword = string.Empty;

    [ObservableProperty]
    private bool isLoading = false;

    public ServerConfigViewModel(ISettingsService settingsService, INntpService nntpService)
    {
        _settingsService = settingsService;
        _nntpService = nntpService;
    }

    public async Task InitializeAsync()
    {
        IsLoading = true;
        var loadedServers = await _settingsService.LoadServersAsync();
        
        Servers.Clear();
        foreach (var server in loadedServers)
        {
            Servers.Add(server);
        }
        
        IsLoading = false;
    }

    [RelayCommand]
    private async Task AddServerAsync()
    {
        if (string.IsNullOrWhiteSpace(NewServerHost))
            return;

        var newServer = new Server(
            NewServerHost,
            NewServerPort,
            NewServerSsl,
            NewServerConnections,
            NewServerUsername,
            NewServerPassword
        );

        Servers.Add(newServer);
        await SaveServersAsync();
        ClearNewServerFields();
    }

    [RelayCommand]
    private async Task RemoveServerAsync(Server server)
    {
        if (server != null)
        {
            Servers.Remove(server);
            await SaveServersAsync();
        }
    }

    [RelayCommand]
    private async Task TestConnectionAsync(Server server)
    {
        if (server != null)
        {
            IsLoading = true;
            var result = await _nntpService.TestConnectionAsync(server);
            IsLoading = false;
            
            // In a real app, you would show a message to the user about the result
        }
    }

    [RelayCommand]
    private async Task SaveServersAsync()
    {
        await _settingsService.SaveServersAsync(Servers);
    }

    private void ClearNewServerFields()
    {
        NewServerHost = string.Empty;
        NewServerPort = 563;
        NewServerSsl = true;
        NewServerConnections = 60;
        NewServerUsername = string.Empty;
        NewServerPassword = string.Empty;
    }
}

