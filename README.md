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
│   ├── Utils/<br>
│       └── Utils.cs<br>
├── Infra/<br>
│   ├── Api/<br>
│   ├── App/<br>
│   ├── DB/<br>
│   │   └── MongoDB<br>
│   │   └── MySQL<br>
│   │   └── PG<br>
│   ├── Pages/<br>
│   │   └── Desktop<br>
│   │   └── Mobile<br>
├── Tests/<br>
│   ├── Api/<br>
│   ├── Browser/<br>
│   │   └── FunctionalTests<br>
│   │   │    └── GoogleMapsTests.cs<br>
│   │   └── NegativeTests<br>
│   │   │    └── NegativeBrowserTests.cs<br>
│   │   └── PerformanceTests<br>
│   │        └── PerformanceBrowserTests.cs<br>
│   ├── Mobile/<br>
│   │   ├── App/<br>
│   │   │   └── AppTests.cs<br>
│   │   └── Browser/<br>
│   │       └── MobileBrowserTests.cs<br>
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
    git clone https://github.com/samer-digital/automation-infra-cSharp.git
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
    dotnet test --collect:"XPlat Code Coverage" --results-directory Tests/Resources/code
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

- **Logic**:
  - **Api**: Contains classes related to API interactions such as GoogleMapsApi.cs and DirectionsResponse.cs.
  - **App**: Contains classes related to mobile app pages, separated into Android and iOS directories.
  - **DB**: Contains database-related logic, divided by database types (Mongo, MySQL, PostgreSQL). Each type has its own models and logic classes.
  - **Pages**: Contains page object models for both desktop and mobile applications. The components directory under Desktop includes specific components used in the pages.

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

## CI Integration

This project is integrated with GitHub Actions for continuous integration. The workflow file is located in .github/workflows. It runs tests automatically on each push and pull request to ensure the quality and stability of the codebase.

## Code Quality

The project follows best practices for code quality and maintainability. It includes:

- Modular Design: Each component is designed to be reusable and testable, reducing the overall complexity.
- Separation of Concerns: Clear separation between the test logic, page objects, and API interactions, following best practices for maintainable test automation.
- Error Handling and Reporting: Comprehensive error handling and detailed reporting mechanisms to provide clear insights into test failures.

## Writing Tests

To write a test, create a new class in the `Tests` folder and extend from `BaseTest`. Use the `TestContext` to manage pages and contexts.


## Contributing

Feel free to submit issues, fork the repository, and send pull requests. For major changes, please open an issue first to discuss what you would like to change.

## Documentation

The project includes basic documentation to help you get started. Please refer to the README file and the comments within the code for guidance on using the various components of the infrastructure.