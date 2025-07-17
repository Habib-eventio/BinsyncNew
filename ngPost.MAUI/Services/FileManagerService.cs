using ngPost.MAUI.Models;

namespace ngPost.MAUI.Services;

public class FileManagerService : IFileManagerService
{
    public async Task<IEnumerable<FileToPost>> SelectFilesAsync()
    {
        // In a real implementation, this would open a file picker dialog
        // For demo purposes, return some sample files
        await Task.Delay(100);
        
        return new List<FileToPost>
        {
            new FileToPost("example_file1.zip", "/path/to/example_file1.zip", 1024 * 1024 * 10), // 10 MB
            new FileToPost("example_file2.rar", "/path/to/example_file2.rar", 1024 * 1024 * 25)  // 25 MB
        };
    }

    public async Task<IEnumerable<FileToPost>> SelectFolderAsync()
    {
        // In a real implementation, this would open a folder picker dialog
        // For demo purposes, return some sample files from a folder
        await Task.Delay(100);
        
        return new List<FileToPost>
        {
            new FileToPost("folder_content1.txt", "/path/to/folder/folder_content1.txt", 1024 * 512), // 512 KB
            new FileToPost("folder_content2.pdf", "/path/to/folder/folder_content2.pdf", 1024 * 1024 * 5) // 5 MB
        };
    }

    public async Task<FileToPost> GetFileInfoAsync(string filePath)
    {
        await Task.Delay(50);
        
        var fileName = Path.GetFileName(filePath);
        var fileSize = await GetFileSizeAsync(filePath);
        
        return new FileToPost(fileName, filePath, fileSize);
    }

    public async Task<bool> FileExistsAsync(string filePath)
    {
        await Task.Delay(10);
        return File.Exists(filePath);
    }

    public async Task<long> GetFileSizeAsync(string filePath)
    {
        await Task.Delay(10);
        
        if (File.Exists(filePath))
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }
        
        return 0;
    }
}

