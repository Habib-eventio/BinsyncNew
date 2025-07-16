using CommunityToolkit.Mvvm.ComponentModel;

namespace ngPost.MAUI.Models;

public partial class PostingSettings : ObservableObject
{
    [ObservableProperty]
    private string posterEmail = "w8uOnbziXvIdp@ngPost.com";

    [ObservableProperty]
    private string newsGroups = "alt.binaries.apps,alt.binaries.applications";

    [ObservableProperty]
    private int articleSize = 716800;

    [ObservableProperty]
    private int nbRetry = 5;

    [ObservableProperty]
    private int nbThreads = 8;

    [ObservableProperty]
    private string language = "EN";

    [ObservableProperty]
    private bool obfuscateArticles = true;

    [ObservableProperty]
    private bool obfuscateFileName = false;

    [ObservableProperty]
    private string nzbDestinationPath = "/home/bruel/Downloads/nzb";

    [ObservableProperty]
    private bool shutdownComputer = false;

    public PostingSettings()
    {
    }
}

