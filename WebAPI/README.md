# LibraryAdmin
LibraryAdmin is a ASP.Net Core / Angular solution for managing a basic library, where books, authors and categories are registered in order to mantain the information organized and secure.

## Prerequisites
Before you continue, ensure you meet the following requirements:
```bash
  ASP.Net Core 3.1+
  Node.js 12.13+
  Angular CLI 8.3.20+
  MySQL 5.7+
```

## Installation

Clone or download the project

### Deploying the database
Replace the connection string in appsettings.json with a valid user and password and then run the following commands:
```bash
cd LibraryAdmin/WebAPI
dotnet ef database update
```

### Running the API

```bash
cd LibraryAdmin/WebAPI
dotnet run
```

### Running the client
```bash
cd LibraryAdmin/WebAPI/ClientApp
ng serve
```

## Usage
Navigate to https://localhost:5001