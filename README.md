# Automation-Infra-Csharp

## Overview

This project is a C# automation infrastructure using Playwright and NUnit for UI testing. It supports testing web applications and mobile applications using Playwright and Appium. The project structure is designed for scalability and maintainability, with a clear separation of concerns.

## Project Structure

Automation-Infra-Csharp/<br>
├── .github/<br>
│   └── workflows/<br>
├── Assets/<br>
│   └── Apps/<br>
│       ├── google-maps.apk<br>
│       └── GoogleMaps6-119-1.ipa<br>
├── Infra/<br>
│   ├── Api/<br>
│   │   ├── ApiOptions.cs<br>
│   │   └── ApiService.cs<br>
│   ├── Browser/<br>
│   │   ├── BrowserOptions.cs<br>
│   │   ├── ComponentBase.cs<br>
│   │   ├── PageBase.cs<br>
│   │   └── PageOptions.cs<br>
│   ├── Config/<br>
│   │   └── ConfigProvider.cs<br>
│   ├── Context/<br>
│   │   ├── ContextHolder.cs<br>
│   │   ├── ContextOptions.cs<br>
│   │   ├── DbContext.cs<br>
│   │   ├── TestContext.cs<br>
│   │   └── WorkerContext.cs<br>
│   ├── DB/<br>
│   │   ├── DBType.cs<br>
│   │   ├── IDBConnectionHolder.cs<br>
│   │   ├── IDBLogicBase.cs<br>
│   │   ├── MongoDBConnectionHolder.cs<br>
│   │   ├── MySQLConnectionHolder.cs<br>
│   │   └── PgConnectionHolder.cs<br>
│   ├── Email/<br>
│   │   └── EmailService.cs<br>
│   ├── Mobile/<br>
│   │   ├── AndroidAppPageBase.cs<br>
│   │   ├── AppPageBase.cs<br>
│   │   ├── AppPageOptions.cs<br>
│   │   ├── Capabilities.cs<br>
│   │   ├── IosAppPageBase.cs<br>
│   │   └── Platform.cs<br>
│   ├── Plugin/<br>
│   │   └── ITestplugin.cs<br>
│   │   └── IWorkerPlugin.cs<br>
│   │   └── LoggingTestPlugin.cs<br>
│   │   └── LoggingWorkerPlugin.cs<br>
│   ├── Utils/<br>
│       └── Utils.cs<br>
├── Tests/<br>
│   ├── Browser/<br>
│   │   └── GoogleMapsTests.cs<br>
│   ├── Mobile/<br>
│   │   ├── App/<br>
│   │   │   └── YourAppTests.cs<br>
│   │   └── Browser/<br>
│   │       └── GoogleMapsMobileBrowserTests.cs<br>
│   ├── Resources/<br>
│   │   ├── playwright-traces/<br>
│   │   └── screenshots/<br>
│   │       └── .gitkeep<br>
│   ├── BaseTest.cs<br>
│   └── Tests.csproj<br>
├── browserOptions.json<br>
├── contextOptions.json<br>
├── storageState.json<br>
├── .env<br>
├── .env.secret.gpg<br>
├── .gitignore<br>
├── AutomationTestsCsharp.sln<br>
└── README.md<br>



## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (for Playwright)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Setup

1. **Clone the repository**:
    ```sh
    git clone 
    cd Automation-Infra-Csharp
    ```

2. **Restore NuGet packages**:
    ```sh
    dotnet restore
    ```

3. **Install Playwright**:
    ```sh
    npx playwright install
    ```

4. **Set up environment variables**:
    - Create a `.env` file in the root directory and add necessary environment variables as per your requirements.
    - Decrypt `.env.secret` with gpg with passphrase for sensetive information security.

5. **Run tests**:
    ```sh
    dotnet test
    ```

## Project Structure Explanation

- **Assets/Apps**: Contains APK and IPA files for mobile application testing.
- **Config**: Contains configuration files like `browserOptions.json`, `contextOptions.json`, and `storageState.json`.
- **Infra**:
  - **Api**: Contains API-related classes and services.
  - **Browser**: Contains browser-specific configurations and base classes.
  - **Config**: Contains configuration provider classes.
  - **Context**: Contains context management classes.
  - **DB**: Contains database connection and logic classes.
  - **Email**: Contains email-related services.
  - **Mobile**: Contains mobile-specific configurations and base classes.
  - **Plugin**: Contains plugin-related classes.
  - **Utils**: Contains utility classes.

- **Tests**:
  - **Browser**: Contains browser-based test cases.
  - **Mobile**: Contains mobile-based test cases.
  - **Resources**: Contains resources like screenshots and traces.

## Key Components

### ConfigProvider.cs

Manages configuration settings loaded from JSON files.

### ContextHolder.cs

Manages browser contexts, including creating new contexts and disposing of them.

### TestContext.cs

Manages test contexts, including pages, tear-down actions, and capturing screenshots.

### DBContext.cs

Manages database connections and logic.

## Writing Tests

To write a test, create a new class in the `Tests` folder and extend from `BaseTest`. Use the `TestContext` to manage pages and contexts.


## Contributing

Feel free to submit issues, fork the repository, and send pull requests. For major changes, please open an issue first to discuss what you would like to change.