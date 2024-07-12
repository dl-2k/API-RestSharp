# Automation Test API Project for DemoQA

## Overview
An automation test project for https://demoqa.com web, built on .NET 8 (C# is the main programming language), NUnit 3.

## Solution Structure

There are 3 projects in this solution:

1. **Core**: Contains essential components for working with APIs and reading configuration files.
2. **DemoQA.Service**: Provides functionalities for API interactions.
3. **DemoQA.Test**:  Tests are written here , dependent on Core and Services

## Dependency Packages

| Package         | Description                               |       Link                                     |
|-----------------|-------------------------------------------|------------------------------------------|
| ExtentReports   |                   | [https://extentreports.com/]
| FluentAssert   |                   | [https://fluentassertions.com/]


## Development Tools

The project is set up using Visual Studio 2022, which is recommended as the main IDE. Alternatively, you can use Visual Studio Code, but you'll need to install the .NET 5 SDK and relevant C# extensions for effective project management and execution.


## Configuration Files

- The `appsetting.json` file is the main config file of this project, it allows you to configure the application URL.

## Running Tests
Before running the test, you must log into https://demoqa.com/books to create new user and get the username, password, userId. Then, paste it into the file of folder `TestData/Users/user_info.json`

## How to Run Tests

1. **Visual Studio 2022**:
   - Use Test Explorer to select tests to run.
2. **Visual Code**:
   - Install the .NET Core Test Explorer extension and then select tests to run.
3. **Command Lines**:
   - Restore all dependency packages: `dotnet restore`
   - Build project: `dotnet build`
   - Run tests: `dotnet test`