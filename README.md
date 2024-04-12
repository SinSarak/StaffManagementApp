<div align="center">
<img crossorigin="anonymous" width="150"
src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/Logo.jpg" alt="Logo" />
	<h1> Staff Management </h1>
	<br>
	<p>
		<b>An open source and free platform to launch your own staff management</b>
	</p>
</div>

<p align="center">
	<a href="https://github.com/SinSarak"><img src="https://img.shields.io/badge/Github-Github-brightgreen" alt="Github"></a>
	<a href="https://www.linkedin.com/in/sarak-sin-04091715b/"><img src="https://img.shields.io/badge/Linkedin-Linkedin-yellowgreen" alt="Documentation"></a>
	<a href="https://t.me/sarakshing"><img src="https://img.shields.io/badge/Telegram-Telegram-green" alt="License"></a>
</p>

## Introduction

This project is an Web API Open-Source Template that includes ASP.NET Core MVC 6, Web API 6, clean n-tier architecture, Unit Testing, Integration Testing, Mssql, databases with a lot of best practices.

## Requirements

In order to run Staff Management you will need to following local development configurations and Database.

Local Development Configurations

- **IDE**: Visual Studio 2022 17
- **ASP.NET Core**: SDK 6.0.421
- **Mssql version**: 2014+ installed with a database created
- **Required Visual Studio extensions**: Microsoft RDLC Report Designer 2022
  [Download from Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftRdlcReportDesignerforVisualStudio2022)

## Solution

<img crossorigin="anonymous" width="400"
src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/Solution_Panel.png" alt="Solution" />

### Database

1. Change the Default Connection and Test Connection.

StaffManagementApp -> appsettings.json

<img crossorigin="anonymous"
src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/ConnString.png" alt="Database Connection String" />

2. Migrate Database

Run the following commands on Package Manager Console in the Project's Directory

```
update-database
```

3. Run

### Web API

You can view endpoints with swagger
<img crossorigin="anonymous"
src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/swagger.png" alt="Swagger API" />

### Web App

You can navicate pages

1. Home page
   <img crossorigin="anonymous"
   src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/webmenu.png" alt="Home page" />
2. Staff information page
   <img crossorigin="anonymous"
   src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/web_staffinfo.png" alt="Staff information page" />
3. Advanced search page
   <img crossorigin="anonymous"
   src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/web_searchstaff.png" alt="Advanced search page" />
4. Export as Excel file
   <img crossorigin="anonymous"
   src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/web_exportexcel.png" alt="Export as Excel file" />
5. Export as PDF file
   <img crossorigin="anonymous"
   src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/web_exportpdf.png" alt="Export as PDF file" />

### Unit Tests

You can perform Unit Testing with In Memory Database and pre-defined data seed.
<img crossorigin="anonymous"
src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/unittest.png" alt="Unit Tests" />

### Integration Tests

You can perform Integration Testing with Testing Database and pre-defined data seed.
<img crossorigin="anonymous"
src="https://github.com/SinSarak/StaffManagementApp/blob/main/ReadMeResource/integrationtest.png" alt="Integration Tests" />

## Technologies

- ASP.NET Core 6 Web Api
- ASP.NET Core MVC 6
- xUnit Test - Unit Test
- xUnit Test - Integration Test
- MSSQL
- AutoMapper
- Swagger Open Api
- Microsoft RDLC Report Designer 2022

## Features

- [x] Net Core Web API 6
- [x] Net Core MVC 6
- [x] N-Tier Architecture
- [x] Restful
- [x] Entity Framework Core - Code First
- [x] Repository Pattern - Generic
- [x] UnitOfWork
- [x] Response Wrappers
- [x] Database Seeding
- [x] Custom Exception Handling Middlewares
- [x] Automapper
- [x] Swagger UI
- [x] Complete staff Management Module (Create / Edit / Delete Display / Search / Export as File)
- [x] Unit Test
- [x] Integration Test
- [x] .Net 6 migration
- [x] Mssql Operations

## Purpose of this Project

This template project has been developed to ensure that I can pass the testing for senior position 😁.

## Give a Star ⭐️

If you found this Implementation helpful or used it in your Projects, do give it a star. Thanks!

## About the Author

### Sin Sarak

- Github [github.com/SinSarak](https://github.com/SinSarak)
- Linkedin - [Sin Sarak](https://www.linkedin.com/in/sarak-sin-04091715b/)
