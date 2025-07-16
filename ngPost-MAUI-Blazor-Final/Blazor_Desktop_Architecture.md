# ngPost Clone: Blazor Desktop (MAUI Hybrid) Application Architecture

## 1. Introduction

This document outlines the revised architecture for the ngPost clone desktop application, transitioning from a pure .NET MAUI (XAML) approach to a Blazor Desktop (MAUI Hybrid) model. The primary motivation for this change is to leverage web technologies (HTML, CSS, C# Blazor components) for UI development while retaining the cross-platform capabilities provided by .NET MAUI for desktop environments (Windows and macOS).

## 2. Core Principles and Design Goals (Revised)

The architectural design for the ngPost Blazor Desktop clone will continue to adhere to the core principles established in the previous MAUI architecture, with an emphasis on integrating web-based UI development:

*   **Modularity:** The application will still be broken down into distinct, independent modules or components. Blazor components will form the modular units of the UI.
*   **Separation of Concerns (SoC):** The MVVM pattern will largely remain for the backend logic (ViewModels and Models), but the View layer will now be implemented using Blazor components. Communication between Blazor components and ViewModels will be managed through established Blazor patterns (e.g., component parameters, event callbacks, dependency injection).
*   **Code Reusability:** A significant goal is to maximize the reuse of C# code, particularly business logic and data models, between the MAUI host and the Blazor components. Future potential for sharing UI components with a web application is also a consideration.
*   **Extensibility & Maintainability:** The architecture will facilitate easy integration of new Blazor components and updates to existing logic.
*   **Performance:** While Blazor Hybrid apps run within a WebView, attention will still be paid to optimizing component rendering and data transfer to ensure a smooth user experience.
*   **Testability:** ViewModels and Services will remain easily testable, and Blazor components can be tested using standard Blazor testing methodologies.

## 3. High-Level Architecture Overview (Blazor Desktop)

In the Blazor Desktop (MAUI Hybrid) model, the MAUI application acts as a host for a WebView2 control (on Windows) or WKWebView (on macOS), which renders the Blazor UI. The Blazor components are essentially web pages running within this embedded browser, but they can directly interact with .NET services and APIs provided by the MAUI host application.

```mermaid
graph TD
    A[User Interface (Blazor Components)] --> B(Blazor Web View)
    B --> C[MAUI Host Application]
    C --> D(ViewModels)
    D --> E(Models)
    D --> F(Services)
    F --> G(Data Access / External APIs)
    G --> H(Usenet Server / Local File System)
```

*   **User Interface (Blazor Components):** This layer will consist of `.razor` files, which combine HTML, CSS, and C# code to define the UI elements and their behavior. These components will replace the XAML Views.
*   **Blazor Web View:** This is the MAUI control (`BlazorWebView`) that hosts the Blazor components. It provides the bridge between the web-based UI and the native MAUI application.
*   **MAUI Host Application:** The native MAUI application that contains the `BlazorWebView` and provides access to platform-specific features and the underlying .NET services.
*   **ViewModels, Models, Services, Data Access / External APIs, Usenet Server / Local File System:** These layers will largely remain the same as defined in the pure MAUI architecture. The key change is how Blazor components interact with these existing .NET components.

## 4. Detailed Component Breakdown (Revised)

### 4.1. Presentation Layer (Blazor Components)

The XAML Views will be replaced by Blazor components. The structure will still be organized around the main sections of the ngPost application.

#### 4.1.1. Main Layout and Navigation

Instead of `AppShell.xaml`, a Blazor `MainLayout.razor` component will define the overall application layout, including navigation (e.g., using a `NavLink` component for tabs). Each main section (Servers, Parameters, Files, Posting Log) will be a separate Blazor page component.

#### 4.1.2. Server Configuration Component

*   **Component:** `ServerConfig.razor`. This component will contain the HTML and C# logic to display and manage NNTP server configurations. It will include input fields, a table/list for servers, and buttons for adding/removing/testing connections.
*   **Interaction with ViewModel:** The `ServerConfig.razor` component will interact with the existing `ServerConfigViewModel.cs`. This can be achieved by injecting the ViewModel into the Blazor component using Dependency Injection. The component will then bind its UI elements to properties and commands exposed by the ViewModel.

#### 4.1.3. Posting Parameters Component

*   **Component:** `PostingParameters.razor`. This component will provide the UI for configuring posting parameters. It will include input fields, checkboxes, and dropdowns for various settings.
*   **Interaction with ViewModel:** Similar to the Server Configuration, this component will inject and bind to the `PostingParametersViewModel.cs`.

#### 4.1.4. File Management Component

*   **Component:** `FileManager.razor`. This will be a more complex component, likely using multiple sub-components for different sections (e.g., compression settings, file list, NZB settings). It will include UI for file selection, compression options, and action buttons.
*   **Interaction with ViewModel:** This component will interact with the `FileManagerViewModel.cs`, injecting it and binding to its properties and commands. File selection (e.g., opening a file picker dialog) will need to be handled by invoking platform-specific services from the Blazor component.

#### 4.1.5. Posting Log Component

*   **Component:** `PostingLog.razor`. This component will display real-time logs, progress, and statistics. It will include a text area for logs, a progress bar, and control buttons.
*   **Interaction with ViewModel:** This component will inject and bind to the `PostingLogViewModel.cs`. Real-time updates from the `NntpService` (via the ViewModel) will trigger UI updates in the Blazor component.

### 4.2. Integration of Existing ViewModels and Services

The existing `ViewModels` and `Services` (e.g., `NntpService`, `FileManagerService`, `CompressionService`, `SettingsService`) can largely be reused as they are pure .NET classes, independent of the UI framework. They will continue to handle the business logic, data access, and interactions with external systems.

#### 4.2.1. Dependency Injection

Dependency Injection will be crucial for connecting the Blazor components to the ViewModels and Services. The `MauiProgram.cs` will be updated to register these services and ViewModels, making them available for injection into Blazor components.

#### 4.2.2. Communication between Blazor and MAUI Host

*   **Invoking MAUI Services from Blazor:** Blazor components can inject and call methods on services registered in the MAUI host's DI container. This is how file pickers, platform-specific APIs, or complex background operations will be triggered from the Blazor UI.
*   **Updating Blazor UI from MAUI Services:** ViewModels and Services can notify Blazor components of changes (e.g., progress updates, new log entries) using standard .NET event patterns or `StateHasChanged()` calls within the Blazor component when a bound property changes.

### 4.3. Data Access Layer (Unchanged)

The Data Access Layer, including the NNTP client library and file system access, remains unchanged. The services will continue to interact with these underlying components.

## 5. Cross-Cutting Concerns (Adaptations)

### 5.1. Dependency Injection (DI)

DI will be configured in `MauiProgram.cs` to register all ViewModels and Services. Blazor components will then use the `@inject` directive to obtain instances of these services.

### 5.2. Error Handling and Logging

Error handling will continue to be managed by the ViewModels and Services. Log entries will be passed to the `PostingLogViewModel`, which will then update the `PostingLog.razor` component.

### 5.3. Asynchronous Programming

`async/await` will continue to be used extensively in ViewModels and Services. Blazor components also support asynchronous operations, ensuring a responsive UI.

### 5.4. Data Validation

Validation logic will remain in the ViewModels. Blazor components can display validation messages based on properties exposed by the ViewModels.

## 6. Development Workflow and Tools (Adaptations)

*   **IDE:** Visual Studio (Windows) or Visual Studio for Mac (macOS) will be used. The tooling for Blazor Hybrid development is well-integrated.
*   **Version Control:** Git remains the standard.
*   **Package Management:** NuGet for .NET libraries, and potentially npm/yarn for any client-side JavaScript libraries if needed within the Blazor components.
*   **Testing:** Unit tests for ViewModels and Services. Blazor components can be tested using libraries like bUnit.

## 7. Conclusion

Migrating to a Blazor Desktop (MAUI Hybrid) architecture offers the best of both worlds: the native application capabilities of .NET MAUI combined with the rapid UI development and web technology familiarity of Blazor. This approach allows for significant code reuse, particularly for business logic, and provides a flexible framework for building a modern ngPost clone. The existing MVVM structure for ViewModels and Services will integrate seamlessly with the new Blazor UI layer.

## 8. References

[1] Build a .NET MAUI Blazor Hybrid app | Microsoft Learn. (n.d.). [https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui?view=aspnetcore-9.0](https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui?view=aspnetcore-9.0)
[2] Blazor Hybrid: Build Cross-Platform Apps with .NET MAUI - YouTube. (n.d.). [https://www.youtube.com/watch?v=Ou0k5XKcIh4](https://www.youtube.com/watch?v=Ou0k5XKcIh4)
[3] What is Blazor Hybrid? (Part 1 of 8) - Learn Microsoft. (n.d.). [https://learn.microsoft.com/en-us/shows/blazor-hybrid-for-beginners/what-is-blazor-hybrid-blazor-hybrid-for-beginners](https://learn.microsoft.com/en-us/shows/blazor-hybrid-for-beginners/what-is-blazor-hybrid-blazor-hybrid-for-beginners)


