# 🏋️ Gym Management System

A full-featured **Gym Management System** built with **ASP.NET Core MVC and .NET 9**, designed to manage gym operations through a structured multi-layer architecture.

The system separates presentation, business logic, and data access responsibilities to provide a maintainable and scalable application structure.

## 🚀 Overview

The **Gym Management System** provides a centralized platform for managing the core operations of a gym, including members, trainers, plans, subscriptions, and other gym-related data.

The application follows a layered architecture with clear separation of concerns between the **Presentation Layer**, **Business Logic Layer**, and **Data Access Layer**.

## ✨ Key Features

* 👥 Member management
* 🏋️ Trainer management
* 📋 Gym plan management
* 💳 Membership and subscription management
* 🔐 Authentication and authorization
* 🗄️ SQL Server database integration
* 🔄 Entity Framework Core data access
* 🧩 AutoMapper for object mapping
* 📊 Structured business logic and service layer
* 🏗️ Layered architecture
* ✅ CRUD operations
* 📱 MVC-based web interface

## 🛠️ Technologies

### Backend

* **C#**
* **ASP.NET Core MVC**
* **.NET 9**
* **Entity Framework Core**
* **ASP.NET Core Identity**
* **AutoMapper**

### Database

* **Microsoft SQL Server**
* **Entity Framework Core SQL Server Provider**
* **EF Core Migrations**

### Frontend

* **Razor Views**
* **HTML5**
* **CSS3**
* **JavaScript**
* **Bootstrap**

### Development Tools

* **Visual Studio**
* **Git & GitHub**
* **SQL Server Management Studio**

## 🏗️ Architecture

The application follows a **3-Layer Architecture**:

GymManagementSystem
│
├── GymManagementPL
│   ├── Controllers
│   ├── Models
│   ├── Views
│   ├── wwwroot
│   └── Program.cs
│
├── GymManagementBLL
│   ├── Services
│   ├── ViewModels
│   ├── AutoMapping
│   └── Business Logic
│
├── GymManagementDAL
│   ├── Data
│   ├── Entities
│   ├── Repositories
│   └── Database Configuration
│
└── GymManagementSystemSolution.sln


### Presentation Layer — `GymManagementPL`

Responsible for handling user interaction and HTTP requests through:

* MVC Controllers
* Razor Views
* View Models
* Static files
* Application configuration

### Business Logic Layer — `GymManagementBLL`

Responsible for the application's business rules and application-level operations.

It contains:

* Services
* View Models
* AutoMapper profiles
* Business logic

### Data Access Layer — `GymManagementDAL`

Responsible for communication with the database.

It contains:

* Entity models
* Entity Framework Core configuration
* Repositories
* Database context
* Migrations

This separation helps keep the application organized and makes individual layers easier to maintain and extend.

## 🗄️ Database

The application uses **Microsoft SQL Server** with **Entity Framework Core** for database operations.

Entity Framework Core is responsible for:

* Database interaction
* Entity mapping
* Relationships
* CRUD operations
* Migrations
* Querying

The project also integrates **ASP.NET Core Identity** for authentication and user management.

## ⚙️ Getting Started

### Prerequisites

Make sure you have the following installed:

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* Microsoft SQL Server
* SQL Server Management Studio or another SQL client
* Visual Studio 2022 or another compatible IDE
* Git

### 1. Clone the Repository

```bash
git clone https://github.com/2302305/GymManagementSystem.git
```

```bash
cd GymManagementSystem
```

### 2. Configure the Database

Open:

```text
GymManagementPL/appsettings.json
```

Update the SQL Server connection string according to your local environment.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

> Make sure the connection string matches your SQL Server configuration.

### 3. Apply Entity Framework Core Migrations

From the solution directory, run:

```bash
dotnet ef database update
```

If required, specify the project explicitly:

```bash
dotnet ef database update --project GymManagementDAL --startup-project GymManagementPL
```

### 4. Restore Dependencies

```bash
dotnet restore
```

### 5. Build the Application

```bash
dotnet build
```

### 6. Run the Application

```bash
dotnet run --project GymManagementPL
```

Or open the solution:

```text
GymManagementSystemSolution.sln
```

in Visual Studio and run the `GymManagementPL` project.

## 🔐 Authentication

The project uses **ASP.NET Core Identity** to handle authentication and user management.

Identity provides a foundation for:

* User registration
* Login
* Password management
* User authentication
* Role-based authorization

## 📂 Project Structure

| Project                           | Responsibility                          |
| --------------------------------- | --------------------------------------- |
| `GymManagementPL`                 | Presentation layer and MVC application  |
| `GymManagementBLL`                | Business logic and application services |
| `GymManagementDAL`                | Data access and database layer          |
| `GymManagementSystemSolution.sln` | Main solution file                      |

## 🎯 Project Goals

The main goals of this project are to:

* Build a realistic gym management application.
* Apply **ASP.NET Core MVC** concepts.
* Implement a structured **3-layer architecture**.
* Practice **Entity Framework Core** and SQL Server integration.
* Apply separation of concerns.
* Implement reusable business services and repositories.
* Work with authentication and authorization.
* Build maintainable and scalable backend code.

## 🔮 Future Improvements

Possible future enhancements include:

* 📊 Advanced analytics dashboard
* 💰 Payment and billing management
* 📅 Training session scheduling
* 📱 Responsive mobile-first interface
* 📧 Email notifications
* 🔔 Subscription expiration notifications
* 📈 Member progress tracking
* 🔎 Advanced search and filtering
* 🌐 RESTful Web API
* ⚡ Redis caching
* 🐳 Docker containerization
* ☁️ Cloud deployment

## 👨‍💻 Author

**Saif Hamza**

Computer Science Graduate | Backend .NET Developer

### Technologies

`C#` `ASP.NET Core` `.NET 9` `MVC` `Entity Framework Core` `SQL Server` `Identity` `AutoMapper`

## 📄 License

This project is available for educational and portfolio purposes.
