using CommunityToolkit.Mvvm.ComponentModel;

namespace ngPost.MAUI.Models;

public partial class Server : ObservableObject
{
    [ObservableProperty]
    private string host = string.Empty;

    [ObservableProperty]
    private int port = 563;

    [ObservableProperty]
    private bool sslEnabled = true;

    [ObservableProperty]
    private int connections = 60;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private bool isEnabled = true;

    public Guid Id { get; set; } = Guid.NewGuid();

    public Server()
    {
    }

    public Server(string host, int port, bool sslEnabled, int connections, string username, string password, bool isEnabled = true)
    {
        Host = host;
        Port = port;
        SslEnabled = sslEnabled;
        Connections = connections;
        Username = username;
        Password = password;
        IsEnabled = isEnabled;
    }

    public override string ToString()
    {
        return $"{Host}:{Port} ({Connections} connections)";
    }
}

