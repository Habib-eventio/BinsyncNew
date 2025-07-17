using Microsoft.Extensions.Logging;
using ngPost.MAUI.ViewModels;
using ngPost.MAUI.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace ngPost.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiBlazorWebView();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register Services
        builder.Services.AddSingleton<INntpService, NntpService>();
        builder.Services.AddSingleton<IFileManagerService, FileManagerService>();
        builder.Services.AddSingleton<ICompressionService, CompressionService>();
        builder.Services.AddSingleton<ISettingsService, SettingsService>();

        // Register ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<ServerConfigViewModel>();
        builder.Services.AddSingleton<PostingParametersViewModel>();
        builder.Services.AddSingleton<FileManagerViewModel>();
        builder.Services.AddSingleton<PostingLogViewModel>();

        // Register Blazor specific services
        builder.Services.AddBlazorWebView();

        return builder.Build();
    }
}

