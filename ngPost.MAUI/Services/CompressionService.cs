using ngPost.MAUI.Models;

namespace ngPost.MAUI.Services;

public class CompressionService : ICompressionService
{
    public event EventHandler<string>? CompressionStatusUpdated;
    public event EventHandler<double>? CompressionProgressUpdated;

    public async Task<string> CompressFilesAsync(IEnumerable<FileToPost> files, CompressionSettings settings)
    {
        CompressionStatusUpdated?.Invoke(this, "Starting compression...");
        
        // Simulate compression process
        for (int i = 0; i <= 100; i += 20)
        {
            CompressionProgressUpdated?.Invoke(this, i);
            CompressionStatusUpdated?.Invoke(this, $"Compressing... {i}%");
            await Task.Delay(500);
        }
        
        var outputPath = Path.Combine(settings.CompressPath, "compressed_archive.rar");
        CompressionStatusUpdated?.Invoke(this, $"Compression completed: {outputPath}");
        
        return outputPath;
    }

    public async Task<string> GeneratePar2Async(string archivePath, CompressionSettings settings)
    {
        if (settings.Par2Redundancy <= 0)
            return string.Empty;
            
        CompressionStatusUpdated?.Invoke(this, "Generating PAR2 files...");
        
        // Simulate PAR2 generation
        for (int i = 0; i <= 100; i += 25)
        {
            CompressionProgressUpdated?.Invoke(this, i);
            CompressionStatusUpdated?.Invoke(this, $"Generating PAR2... {i}%");
            await Task.Delay(300);
        }
        
        var par2Path = Path.ChangeExtension(archivePath, ".par2");
        CompressionStatusUpdated?.Invoke(this, $"PAR2 generation completed: {par2Path}");
        
        return par2Path;
    }

    public async Task<bool> ValidateRarPathAsync(string rarPath)
    {
        await Task.Delay(100);
        
        // In a real implementation, this would check if the RAR executable exists
        // For demo purposes, assume it's valid if the path is not empty
        return !string.IsNullOrWhiteSpace(rarPath);
    }

    public async Task<bool> ValidatePar2PathAsync(string par2Path)
    {
        await Task.Delay(100);
        
        // In a real implementation, this would check if the PAR2 executable exists
        // For demo purposes, assume it's valid if the path is not empty
        return !string.IsNullOrWhiteSpace(par2Path);
    }
}

