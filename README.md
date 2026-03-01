**CRUD Operations using ASP.NET Web API and SQL Server Database Connection via ODBC
This guide will help you set up a complete CRUD (Create, Read, Update, Delete) system using ASP.NET Core Web API with a SQL Server database connection. Follow the steps carefully for the best results.**

**Step 1: SQL Setup**

-1 **Create Database**
-   Create a new database named Employee.

-2 **Create Table**
-   In the Employee database, create a table called tbl_Employee.

-3 **Create Stored Procedures**
- Define the following stored procedures:

    -InsertEmployee
    -GetAllEmployee
    -GetAllEmployeeByID
    -DeleteEmployee
    -UpdateEmployee
  
**Step 2: ASP.NET Core Web API Setup**A 
1- **Create a New Project**

-     Select the ASP.NET Core Web API template.
-     This template includes features like:
  -     C# support
  -     Cross-platform compatibility (Linux, MacOS, Windows)
  -     Web API services and cloud integration

2- **Configure appsettings.json**

          What is appsettings.json?
    
-     It is a configuration file used in ASP.NET Core projects.
-     Stores important settings such as:
  -     Database connection strings
  -     API keys, URLs, logging settings
  -     Any custom configuration your application needs
-     Think of it as the central "settings file" for your project.

3- **Create Models**

  - Create a folder named Model to organize your data models.
  - Right-click on the Model folder → Add → Class.
  - Create the necessary models for your application.

4- **Create Controllers**

  - Create a folder named Controller.
  - Right-click → Add → Controller.
  - Choose API Controller – Empty to create a new API controller.
