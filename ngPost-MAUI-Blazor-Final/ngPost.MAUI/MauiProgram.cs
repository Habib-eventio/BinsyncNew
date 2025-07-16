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
            //.UseMauiBlazorWebView() // ✅ this extension comes from the NuGet + using above
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddMauiBlazorWebView(); // ✅ required for BlazorWebView to function

        return builder.Build();
    }
}

