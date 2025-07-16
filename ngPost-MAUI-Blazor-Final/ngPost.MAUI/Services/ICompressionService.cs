using ngPost.MAUI.Models;

namespace ngPost.MAUI.Services;

public interface ICompressionService
{
    event EventHandler<string> CompressionStatusUpdated;
    event EventHandler<double> CompressionProgressUpdated;

    Task<string> CompressFilesAsync(IEnumerable<FileToPost> files, CompressionSettings settings);
    Task<string> GeneratePar2Async(string archivePath, CompressionSettings settings);
    Task<bool> ValidateRarPathAsync(string rarPath);
    Task<bool> ValidatePar2PathAsync(string par2Path);
}

