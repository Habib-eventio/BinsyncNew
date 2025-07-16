using ngPost.MAUI.Models;

namespace ngPost.MAUI.Services;

public interface IFileManagerService
{
    Task<IEnumerable<FileToPost>> SelectFilesAsync();
    Task<IEnumerable<FileToPost>> SelectFolderAsync();
    Task<FileToPost> GetFileInfoAsync(string filePath);
    Task<bool> FileExistsAsync(string filePath);
    Task<long> GetFileSizeAsync(string filePath);
}

