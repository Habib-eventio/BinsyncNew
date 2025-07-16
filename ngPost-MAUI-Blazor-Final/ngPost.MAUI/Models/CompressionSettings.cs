using CommunityToolkit.Mvvm.ComponentModel;

namespace ngPost.MAUI.Models;

public partial class CompressionSettings : ObservableObject
{
    [ObservableProperty]
    private string compressPath = "/tmp";

    [ObservableProperty]
    private string rarPath = "/usr/bin/rar";

    [ObservableProperty]
    private int volSize = 42;

    [ObservableProperty]
    private bool limitRarNumber = false;

    [ObservableProperty]
    private int par2Redundancy = 0;

    [ObservableProperty]
    private bool compressEnabled = true;

    [ObservableProperty]
    private bool generatePar2 = false;

    [ObservableProperty]
    private bool keepArchives = false;

    public CompressionSettings()
    {
    }
}

