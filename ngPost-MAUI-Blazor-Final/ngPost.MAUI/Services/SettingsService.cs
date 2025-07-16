using ngPost.MAUI.Models;
using System.Text.Json;

namespace ngPost.MAUI.Services;

public class SettingsService : ISettingsService
{
    private readonly string _settingsFolder;

    public SettingsService()
    {
        _settingsFolder = Path.Combine(FileSystem.AppDataDirectory, "Settings");
        Directory.CreateDirectory(_settingsFolder);
    }

    public async Task SaveServersAsync(IEnumerable<Server> servers)
    {
        var filePath = Path.Combine(_settingsFolder, "servers.json");
        var json = JsonSerializer.Serialize(servers, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<IEnumerable<Server>> LoadServersAsync()
    {
        var filePath = Path.Combine(_settingsFolder, "servers.json");
        
        if (!File.Exists(filePath))
        {
            // Return default server configuration
            return new List<Server>
            {
                new Server("news.usenetserver.com", 563, true, 60, "", "")
            };
        }

        var json = await File.ReadAllTextAsync(filePath);
        var servers = JsonSerializer.Deserialize<List<Server>>(json);
        return servers ?? new List<Server>();
    }

    public async Task SavePostingSettingsAsync(PostingSettings settings)
    {
        var filePath = Path.Combine(_settingsFolder, "posting_settings.json");
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<PostingSettings> LoadPostingSettingsAsync()
    {
        var filePath = Path.Combine(_settingsFolder, "posting_settings.json");
        
        if (!File.Exists(filePath))
        {
            return new PostingSettings();
        }

        var json = await File.ReadAllTextAsync(filePath);
        var settings = JsonSerializer.Deserialize<PostingSettings>(json);
        return settings ?? new PostingSettings();
    }

    public async Task SaveCompressionSettingsAsync(CompressionSettings settings)
    {
        var filePath = Path.Combine(_settingsFolder, "compression_settings.json");
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<CompressionSettings> LoadCompressionSettingsAsync()
    {
        var filePath = Path.Combine(_settingsFolder, "compression_settings.json");
        
        if (!File.Exists(filePath))
        {
            return new CompressionSettings();
        }

        var json = await File.ReadAllTextAsync(filePath);
        var settings = JsonSerializer.Deserialize<CompressionSettings>(json);
        return settings ?? new CompressionSettings();
    }
}

