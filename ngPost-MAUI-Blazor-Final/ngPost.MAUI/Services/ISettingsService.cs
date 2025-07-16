using ngPost.MAUI.Models;

namespace ngPost.MAUI.Services;

public interface ISettingsService
{
    Task SaveServersAsync(IEnumerable<Server> servers);
    Task<IEnumerable<Server>> LoadServersAsync();
    Task SavePostingSettingsAsync(PostingSettings settings);
    Task<PostingSettings> LoadPostingSettingsAsync();
    Task SaveCompressionSettingsAsync(CompressionSettings settings);
    Task<CompressionSettings> LoadCompressionSettingsAsync();
}

