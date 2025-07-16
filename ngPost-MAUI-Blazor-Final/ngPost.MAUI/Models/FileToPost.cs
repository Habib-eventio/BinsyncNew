using CommunityToolkit.Mvvm.ComponentModel;

namespace ngPost.MAUI.Models;

public enum PostingStatus
{
    Pending,
    Compressing,
    Posting,
    Completed,
    Failed
}

public partial class FileToPost : ObservableObject
{
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string path = string.Empty;

    [ObservableProperty]
    private long size;

    [ObservableProperty]
    private PostingStatus status = PostingStatus.Pending;

    [ObservableProperty]
    private double progress;

    public Guid Id { get; set; } = Guid.NewGuid();

    public FileToPost()
    {
    }

    public FileToPost(string name, string path, long size)
    {
        Name = name;
        Path = path;
        Size = size;
    }

    public string FormattedSize => FormatBytes(Size);

    private static string FormatBytes(long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
        int counter = 0;
        decimal number = bytes;
        while (Math.Round(number / 1024) >= 1)
        {
            number /= 1024;
            counter++;
        }
        return $"{number:n1} {suffixes[counter]}";
    }
}

