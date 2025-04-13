# TaskManager Application

TaskManager is a full-stack CRUD application designed to help caseworkers manage their tasks. The backend API is built using ASP.NET Core (.NET 9) with Entity Framework Core, and the frontend is built using Angular with modern, responsive design (custom Flexbox, CSS Grid, and SCSS variables).

## Features

- **Task CRUD:** Create, retrieve, update, and delete task items.
- **Modern & Responsive UI:** Built using custom Flexbox and CSS Grid layouts with SCSS variables.
- **Form Validation:** Inline form validation with clear error messages.
- **Global Error Handling:** Centralized error handling using custom ExceptionMiddleware.
- **Swagger API Documentation:** Interactive API documentation via Swagger.
- **Cross-Platform Support:** Detailed instructions for Windows and macOS users, including optional Docker support for SQL Server on macOS.

## Architecture & Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 9), Entity Framework Core, SQL Server (or Dockerized SQL Server on macOS)
- **Frontend:** Angular 19, SCSS (using Sass modules), Angular Forms
- **Error Handling:** Custom global ExceptionMiddleware
- **API Documentation:** Swagger / OpenAPI via Swashbuckle
- **Testing:** xUnit for unit tests

## Prerequisites

Before running the application, make sure you have installed:

- **.NET 9 SDK:** [Download .NET 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Node.js (LTS):** [Download Node.js](https://nodejs.org/en/) (npm is included)
- **Angular CLI:** Install globally using:
  ```bash
  npm install -g @angular/cli

## Git
Download and install Git from [Git Downloads](https://git-scm.com/downloads).

## SQL Server

### On Windows
Use SQL Server Express or LocalDB.

### On macOS
Use Docker to run SQL Server (see Docker Setup below).

## Installation & Setup

### Clone the Repository
Clone the repository and navigate into the project folder:
git clone https://github.com/YourUsername/TaskManager.git
cd TaskManager

### Backend Setup (ASP.NET Core)

#### Restore NuGet Packages
Run the following command to restore NuGet packages: dotnet restore

#### Configure Database Connection
Open `appsettings.json` in the backend project and update the connection string:

- **On Windows:**-
- "ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TaskManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

- **On macOS (using Docker):**
- "ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=TaskManagerDb;User Id=sa;Password=Your_password123;"
}


#### Apply Migrations / Create the Database
Run the following command to apply migrations and create the database:
dotnet ef database update


### Frontend Setup (Angular)

#### Navigate to the ClientApp Folder
Navigate to the `ClientApp` folder
cd ClientApp

#### Install npm Dependencies
Run the following command to install npm dependencies: 
npm install

#### Build the Angular App for Production
Build the Angular app for production:The build output will be placed in `ClientApp/dist/client-app`.
ng build --configuration production

### Optional: Docker Setup for macOS Users
For macOS users without a native SQL Server installation, run SQL Server in Docker:

1. **Install Docker Desktop for Mac:**  
   [Download Docker Desktop](https://www.docker.com/products/docker-desktop/)

2. **Pull the SQL Server Image:**
   docker pull mcr.microsoft.com/mssql/server:2019-latest

3. **Run SQL Server in a Container:**
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest

4. Ensure your `appsettings.json` connection string matches these credentials.

## Running the Application

### On Windows
1. **Start the Backend:**  
   From the repository root (where the `.sln` file is located), run:
dotnet run --urls "https://localhost:7178;http://localhost:5096"

2. **Access the Application:**  
   The ASP.NET Core app serves the Angular application (from `ClientApp/dist/client-app`) via SPA middleware. Open your browser and navigate to: https://localhost:7178

3. **For Live Development:**  
   Open a new terminal, navigate to the `ClientApp` folder, and run:
 ng serve --proxy-config proxy.conf.json
 Then open [http://localhost:4200](http://localhost:4200) in your browser.

### On macOS
1. **Start the Backend:**  
   From the repository root, run:
   dotnet run --urls "https://localhost:7178;http://localhost:5096"
   (Ensure your Docker container for SQL Server is running if you're using Docker.)

2. **Access the Application:**  
   Open your browser and navigate to:
   https://localhost:7178

3. **For Live Development:**  
   Open a new terminal, navigate to the `ClientApp` folder, and run:Then browse to [http://localhost:4200](http://localhost:4200).

## API Documentation

Swagger is integrated in development mode. Once the backend is running, navigate to:
https://localhost:7178/swagger to view interactive API documentation, explore endpoints, and test API calls.

## Testing

Unit tests have been implemented using xUnit.

1. **Navigate to the TaskManager.Tests:**
2. **Run the Tests:**

## Global Error Handling & Logging

The application implements global error handling via a custom `ExceptionMiddleware`. This middleware is registered early in the pipeline (in `Program.cs`) to:

- Log detailed error information using `ILogger`.
- Return detailed error responses in development (including stack traces) and generic messages in production.
- Centralize error handling so you don’t need to scatter `try/catch` blocks across your controllers and services.
- 

License
This project is licensed under the MIT License.
