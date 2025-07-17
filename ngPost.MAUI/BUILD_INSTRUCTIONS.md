# Build Instructions for ngPost MAUI

## Prerequisites

### Windows Development
1. **Visual Studio 2022** (17.8 or later)
   - Install the ".NET Multi-platform App UI development" workload
   - Ensure Windows 10/11 SDK is installed

2. **Alternative: Visual Studio Code + .NET CLI**
   - Install .NET 8 SDK
   - Install MAUI workload: `dotnet workload install maui`

### macOS Development
1. **Visual Studio for Mac** (17.6 or later)
   - Install the ".NET Multi-platform App UI development" workload

2. **Alternative: Visual Studio Code + .NET CLI**
   - Install .NET 8 SDK
   - Install MAUI workload: `dotnet workload install maui`
   - Xcode 14.0 or later (for macOS deployment)

## Building the Application

### Using Visual Studio
1. Open `ngPost.MAUI.sln` (or the .csproj file)
2. Select target framework:
   - `net8.0-windows10.0.19041.0` for Windows
   - `net8.0-maccatalyst` for macOS
3. Build → Build Solution (Ctrl+Shift+B)

### Using .NET CLI

#### Windows
```bash
# Navigate to project directory
cd ngPost.MAUI

# Restore packages
dotnet restore

# Build for Windows
dotnet build -f net8.0-windows10.0.19041.0 -c Release

# Run the application
dotnet run -f net8.0-windows10.0.19041.0
```

#### macOS
```bash
# Navigate to project directory
cd ngPost.MAUI

# Restore packages
dotnet restore

# Build for macOS
dotnet build -f net8.0-maccatalyst -c Release

# Run the application
dotnet run -f net8.0-maccatalyst
```

## Publishing for Distribution

### Windows (MSIX Package)
```bash
# Publish as self-contained
dotnet publish -f net8.0-windows10.0.19041.0 -c Release -r win10-x64 --self-contained

# Create MSIX package (requires Windows SDK)
dotnet publish -f net8.0-windows10.0.19041.0 -c Release -p:PublishProfile=win10-x64 -p:GenerateAppxPackageOnBuild=true
```

### macOS (App Bundle)
```bash
# Publish as self-contained
dotnet publish -f net8.0-maccatalyst -c Release -r maccatalyst-x64 --self-contained

# Create app bundle
dotnet publish -f net8.0-maccatalyst -c Release -p:CreatePackage=true
```

## Troubleshooting

### Common Issues

1. **MAUI Workload Not Found**
   ```bash
   dotnet workload install maui
   ```

2. **Missing Windows SDK**
   - Install Windows 10/11 SDK through Visual Studio Installer
   - Or download from Microsoft Developer site

3. **macOS Code Signing Issues**
   - Ensure Apple Developer account is configured
   - Set up proper provisioning profiles

4. **NuGet Package Restore Failures**
   ```bash
   dotnet nuget locals all --clear
   dotnet restore --force
   ```

### Platform-Specific Notes

#### Windows
- Minimum OS: Windows 10 version 1809 (build 17763)
- Recommended: Windows 11 for best experience
- Requires Windows App SDK runtime for deployment

#### macOS
- Minimum OS: macOS 10.15 (Catalina)
- Recommended: macOS 12.0 or later
- Requires Xcode command line tools

## Development Tips

1. **Hot Reload**: Enable Blazor Hot Reload for faster UI development
2. **Debugging**: Use platform-specific debuggers (WinUI for Windows, Xcode for macOS)
3. **Testing**: Test on both platforms regularly during development
4. **Performance**: Profile memory usage, especially for large file operations

## Next Steps

After building successfully:
1. Test all major functionality (server config, file selection, posting simulation)
2. Implement actual NNTP communication (replace mock service)
3. Add proper file picker implementations
4. Integrate with external compression tools (RAR, PAR2)
5. Add comprehensive error handling and logging
6. Create installer packages for distribution

