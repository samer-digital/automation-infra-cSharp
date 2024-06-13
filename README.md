# Automation-Infra-Csharp

## Overview

This project is a C# automation infrastructure using Playwright and NUnit for UI testing. It supports testing web applications and mobile applications using Playwright and Appium. The project structure is designed for scalability and maintainability, with a clear separation of concerns.

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

This project leverages GitHub Actions for continuous integration (CI). The workflow configuration file is located in the .github/workflows directory. Our CI pipeline is designed to ensure the quality and stability of the codebase by performing the following tasks automatically on each push and pull request:

- **Parallel Cross-Browser Testing**: Runs tests in parallel across multiple browsers (Chrome, Firefox, Webkit) to ensure compatibility and robustness.
- **Failure Handling**: Captures screenshots and traces in case of test failures, providing detailed insights for debugging.
- **Code Coverage Reporting**: Generates and attaches code coverage reports, offering comprehensive visibility into the test coverage of the codebase.

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