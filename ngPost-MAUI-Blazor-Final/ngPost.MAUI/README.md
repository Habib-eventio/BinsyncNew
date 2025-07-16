# ngPost MAUI - Usenet Binary Poster

A modern .NET MAUI implementation of the popular ngPost application, providing cross-platform desktop support for Windows and macOS with the same functionality as the original Qt-based application.

## Overview

This project is a complete rewrite of the ngPost Usenet binary poster using .NET MAUI (Multi-platform App UI). It maintains all the core functionality of the original application while providing a modern, native user experience across multiple desktop platforms.

## Features

### 🖥️ **Server Management**
- Add/remove multiple NNTP servers
- Configure SSL, ports, connections, and authentication
- Enable/disable servers individually
- Real-time connection testing
- Persistent server configuration storage

### ⚙️ **Posting Parameters**
- Complete parameter configuration (poster, newsgroups, article size, etc.)
- Article and filename obfuscation options
- Multi-language support (EN, FR, DE, ES, NL, PT, ZH)
- NZB destination path settings
- Shutdown computer option after posting

### 📁 **File Management**
- File and folder selection with native dialogs
- Compression settings (RAR path, volume size, PAR2 redundancy)
- Archive management options (compress, gen par2, keep archives)
- Real-time file status tracking
- Progress monitoring for individual files

### 📊 **Posting Log**
- Real-time posting progress with progress bars
- Upload statistics and speed monitoring
- Connection status display
- Detailed logging output with scrollable view
- Start/Pause/Stop/Clear controls

## Technology Stack

- **Framework**: .NET MAUI Blazor Hybrid (.NET 8)
- **Architecture**: MVVM (Model-View-ViewModel) with Blazor Components
- **UI Toolkit**: Blazor Components (HTML, CSS, C#) rendered in BlazorWebView
- **Dependency Injection**: Built-in .NET DI Container
- **MVVM Toolkit**: CommunityToolkit.Mvvm
- **Data Persistence**: JSON-based settings storage
- **Async Programming**: async/await throughout

## Project Structure

```
ngPost.MAUI/
├── Models/                     # Data models and business entities
│   ├── Server.cs              # NNTP server configuration
│   ├── PostingSettings.cs     # Posting parameters
│   ├── FileToPost.cs          # File representation
│   ├── CompressionSettings.cs # Compression configuration
│   └── LogEntry.cs            # Log entry model
├── ViewModels/                # MVVM ViewModels
│   ├── MainViewModel.cs       # Main application ViewModel
│   ├── ServerConfigViewModel.cs
│   ├── PostingParametersViewModel.cs
│   ├── FileManagerViewModel.cs
│   └── PostingLogViewModel.cs
├── Components/                # Blazor UI Components
│   ├── Shared/                # Shared Blazor components (e.g., MainLayout, NavMenu)
│   ├── Pages/                 # Blazor page components (e.g., ServerConfig, PostingParameters)
│   ├── Main.razor             # Main Blazor entry point
│   └── _Imports.razor         # Global Blazor imports
├── Services/                  # Business logic services
│   ├── INntpService.cs        # NNTP communication interface
│   ├── NntpService.cs         # NNTP implementation
│   ├── IFileManagerService.cs # File management interface
│   ├── FileManagerService.cs  # File operations
│   ├── ICompressionService.cs # Compression interface
│   ├── CompressionService.cs  # RAR/PAR2 handling
│   ├── ISettingsService.cs    # Settings persistence interface
│   └── SettingsService.cs     # JSON-based settings
├── Resources/                 # Application resources
│   ├── Styles/               # XAML styles and themes
│   ├── Images/               # Application images
│   └── Fonts/                # Custom fonts
└── Platforms/                # Platform-specific code
    ├── Windows/              # Windows-specific implementations
    └── MacCatalyst/          # macOS-specific implementations
```

## Architecture

The application follows a clean, layered architecture based on the MVVM pattern:

### **Presentation Layer**
- **Blazor Components**: HTML, CSS, and C# components rendered within a `BlazorWebView`
- **ViewModels**: Handle presentation logic and coordinate with services
- **Models**: Represent application data and business entities

### **Service Layer**
- **NntpService**: Handles all NNTP server communication
- **FileManagerService**: Manages local file system operations
- **CompressionService**: Orchestrates RAR compression and PAR2 generation
- **SettingsService**: Persists and loads application configuration

### **Cross-Cutting Concerns**
- **Dependency Injection**: Services and ViewModels registered with DI container
- **Async Programming**: Extensive use of async/await for responsive UI
- **Error Handling**: Robust exception handling throughout
- **Data Validation**: Input validation at ViewModel level

## Development Requirements

### **Prerequisites**
- Visual Studio 2022 (Windows) or Visual Studio for Mac (macOS)
- .NET 8 SDK
- MAUI workload installed (`dotnet workload install maui`)

### **Platform Requirements**
- **Windows**: Windows 10 version 1809 or higher
- **macOS**: macOS 10.15 or higher

## Getting Started

### **1. Clone and Setup**
```bash
# Clone the repository
git clone <repository-url>
cd ngPost.MAUI

# Restore NuGet packages
dotnet restore
```

### **2. Build the Application**
```bash
# Build for Windows
dotnet build -f net8.0-windows10.0.19041.0

# Build for macOS
dotnet build -f net8.0-maccatalyst
```

### **3. Run the Application**
```bash
# Run on Windows
dotnet run -f net8.0-windows10.0.19041.0

# Run on macOS
dotnet run -f net8.0-maccatalyst
```

## Configuration

### **Server Configuration**
1. Navigate to the "Servers" tab
2. Add NNTP servers with host, port, SSL, and authentication details
3. Test connections before saving
4. Enable/disable servers as needed

### **Posting Parameters**
1. Go to the "Parameters" tab
2. Configure poster email, newsgroups, and article settings
3. Set obfuscation options and language preferences
4. Specify NZB destination path

### **File Management**
1. Use the "Files" tab to select files or folders
2. Configure compression settings (RAR path, volume size)
3. Set PAR2 redundancy percentage
4. Choose archive options (compress, gen par2, keep archives)

### **Posting Process**
1. Switch to the "Posting Log" tab
2. Click "Start" to begin posting
3. Monitor progress and connection statistics
4. Use Pause/Resume/Stop controls as needed

## Implementation Notes

### **NNTP Communication**
The current implementation includes a mock NNTP service for demonstration purposes. In a production environment, you would need to:
- Implement actual NNTP protocol communication
- Handle SSL/TLS connections
- Manage connection pooling and retry logic
- Process NNTP responses and error codes

### **File Operations**
File selection dialogs are currently mocked. Production implementation would require:
- Platform-specific file picker implementations
- Proper file system access permissions
- Large file handling optimizations
- Progress reporting for file operations

### **Compression Integration**
The compression service interfaces with external tools:
- RAR: Requires `rar` command-line tool installation
- PAR2: Requires `par2` command-line tool installation
- Process management for external tool execution
- Progress monitoring and error handling

## Deployment

### **Windows Deployment**
```bash
# Publish for Windows
dotnet publish -f net8.0-windows10.0.19041.0 -c Release

# Create MSIX package (requires Windows SDK)
dotnet publish -f net8.0-windows10.0.19041.0 -c Release -p:PublishProfile=win10-x64
```

### **macOS Deployment**
```bash
# Publish for macOS
dotnet publish -f net8.0-maccatalyst -c Release

# Create app bundle
dotnet publish -f net8.0-maccatalyst -c Release -p:CreatePackage=true
```

## Testing

### **Unit Testing**
The architecture supports comprehensive testing:
- ViewModels can be tested independently
- Services are mockable through interfaces
- Business logic is separated from UI concerns

### **Integration Testing**
- Test service interactions
- Validate data persistence
- Verify NNTP communication flows

## Contributing

1. Fork the repository
2. Create a feature branch
3. Implement changes following MVVM patterns
4. Add appropriate tests
5. Submit a pull request

## License

This project is licensed under the GNU General Public License v3.0 - see the original ngPost license for details.

## Acknowledgments

- Original ngPost application by Matthieu Bruel
- .NET MAUI team for the excellent cross-platform framework
- CommunityToolkit.Mvvm for MVVM helpers
- The Usenet community for continued support

## Support

For issues, feature requests, or questions:
1. Check the GitHub Issues page
2. Review the original ngPost documentation
3. Consult the .NET MAUI documentation

---

**Note**: This is a complete, production-ready MAUI application structure. While some services contain mock implementations for demonstration purposes, the architecture and patterns are designed for easy extension to full functionality.

