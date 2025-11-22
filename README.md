#EmployeeProject – ASP.NET Core 8 Web API

A clean **ASP.NET Core 8 Web API** for managing employees with full **authentication** and **role-based authorization** using **JWT** and **Identity**.

---

##  Overview

This project is a simple RESTful API that allows you to:

- Register & login users using **ASP.NET Core Identity**
- Authenticate users using **JWT tokens**
- Apply **role-based authorization** (`Admin`, `User`)
- Perform **CRUD operations** on Employees:
  - Create new employee
  - Get employee by ID
  - Update employee
  - Delete employee

---

## 🧱 Technologies & Packages

### 🔹 Framework
- **.NET 8.0** (`TargetFramework: net8.0`)
- **ASP.NET Core 8 Web API**

###  NuGet Packages & Versions

| Package Name                                                   | Version   | Usage                                        |
|---------------------------------------------------------------|----------:|----------------------------------------------|
| `Microsoft.AspNetCore.Authentication.JwtBearer`               | **8.0.14** | JWT authentication (Bearer tokens)          |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore`           | **8.0.14** | Identity with Entity Framework Core         |
| `Microsoft.EntityFrameworkCore.SqlServer`                     | **8.0.14** | SQL Server provider for EF Core             |
| `Microsoft.EntityFrameworkCore.Tools`                         | **8.0.14** | EF Core migrations & design-time tools      |
| `Swashbuckle.AspNetCore`                                      | **6.6.2** | Swagger UI & OpenAPI documentation          |

###  Other Tools

- **Entity Framework Core 8**
- **SQL Server** (LocalDB or full instance)
- **Visual Studio 2022** (recommended)
- **Postman / Swagger UI** for testing the API

---

##  Project Structure

```text
TestProject
├── Controllers
│   ├── AccountController.cs       // Login / JWT / Identity
│   └── EmployeeController.cs      // Employee CRUD endpoints
├── Context
│   └── App_context.cs             // DbContext (EF Core)
├── DTOS
│   ├── Login_DTO.cs
│   └── Sign_DTO.cs
├── Models
│   └── Employee.cs                // Employee entity
├── appsettings.json               // DB connection string & JWT settings
└── Program.cs                     // Middleware & services configuration


