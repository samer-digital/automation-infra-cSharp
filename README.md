# Automation-Infra-Csharp

## Overview

This project is a C# automation infrastructure using Playwright and NUnit for UI testing. It supports testing web applications and mobile applications using Playwright and Appium. The project structure is designed for scalability and maintainability, with a clear separation of concerns.

## Project Structure

Automation-Infra-Csharp/ /r
├── .github/
│   └── workflows/
├── Assets/
│   └── Apps/
│       ├── google-maps.apk
│       └── GoogleMaps6-119-1.ipa
├── Infra/
│   ├── Api/
│   │   ├── ApiOptions.cs
│   │   └── ApiService.cs
│   ├── Browser/
│   │   ├── BrowserOptions.cs
│   │   ├── ComponentBase.cs
│   │   ├── PageBase.cs
│   │   └── PageOptions.cs
│   ├── Config/
│   │   └── ConfigProvider.cs
│   ├── Context/
│   │   ├── ContextHolder.cs
│   │   ├── ContextOptions.cs
│   │   ├── DbContext.cs
│   │   ├── TestContext.cs
│   │   └── WorkerContext.cs
│   ├── DB/
│   │   ├── DBType.cs
│   │   ├── IDBConnectionHolder.cs
│   │   ├── IDBLogicBase.cs
│   │   ├── MongoDBConnectionHolder.cs
│   │   ├── MySQLConnectionHolder.cs
│   │   └── PgConnectionHolder.cs
│   ├── Email/
│   │   └── EmailService.cs
│   ├── Mobile/
│   │   ├── AndroidAppPageBase.cs
│   │   ├── AppPageBase.cs
│   │   ├── AppPageOptions.cs
│   │   ├── Capabilities.cs
│   │   ├── IosAppPageBase.cs
│   │   └── Platform.cs
│   ├── Plugin/
│   │   └── ITestplugin.cs
│   │   └── IWorkerPlugin.cs
│   │   └── LoggingTestPlugin.cs
│   │   └── LoggingWorkerPlugin.cs
│   ├── Utils/
│       └── Utils.cs
├── Tests/
│   ├── Browser/
│   │   └── GoogleMapsTests.cs
│   ├── Mobile/
│   │   ├── App/
│   │   │   └── YourAppTests.cs
│   │   └── Browser/
│   │       └── GoogleMapsMobileBrowserTests.cs
│   ├── Resources/
│   │   ├── playwright-traces/
│   │   └── screenshots/
│   │       └── .gitkeep
│   ├── BaseTest.cs
│   └── Tests.csproj
├── browserOptions.json
├── contextOptions.json
├── storageState.json
├── .env
├── .env.secret.gpg
├── .gitignore
├── AutomationTestsCsharp.sln
└── README.md



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