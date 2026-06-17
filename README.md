# SmartContract Hub

SmartContract Hub is a modern enterprise-style ASP.NET Core MVC web application designed for managing clients, contracts, service requests, PDF agreements, and automated currency conversion.

The system provides a centralized platform for organizations to efficiently manage contract lifecycles, customer records, service operations, and agreement documentation.

---

# Features

## Client Management
- Create, edit, view, and delete clients
- Store client contact details and regions
- Search clients by name
- Filter clients by region

---

## Contract Management
- Create and manage contracts
- Upload signed agreement PDF files
- Download uploaded agreements
- Search and filter contracts
- Filter by:
  - Status
  - Start Date
  - End Date

### Contract Statuses
- Active
- Expired
- On Hold
- Draft

---

## Service Request Management
- Create service requests linked to contracts
- Automatically convert USD costs to ZAR
- Track request progress

### Request Statuses
- Pending
- In Progress
- Completed

---

## Dashboard Analytics
- Enterprise-style dashboard
- Statistics cards
- Quick actions panel
- Recent activity section
- Responsive design
- Modern SaaS-inspired UI

---

## File Validation
- PDF-only upload validation
- File size validation
- Secure file handling

---

## Currency Conversion API
- Real-time USD to ZAR conversion
- External exchange rate API integration
- Automated currency calculations

---

## Unit Testing
The project includes xUnit testing for:
- Currency conversion logic
- File validation logic
- Business service testing

---

# Technologies Used

## Backend
- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server LocalDB

---

## Frontend
- Razor Views
- Bootstrap 5
- Bootstrap Icons
- Custom CSS

---

## Testing
- xUnit
- Microsoft.NET.Test.Sdk

---

## APIs
- Exchange Rate API

---

# Application Screenshots

---

## Dashboard

![Dashboard](images/DashboardView.jpeg)

The enterprise dashboard provides:
- analytics cards
- quick actions
- recent activity
- centralized management

---

## Client Management

![Clients](images/ClientView.jpeg)

The client module allows administrators to:
- create clients
- search clients
- filter by region
- manage customer information

---

## Contract Management

![Contracts](images/ContractView.jpeg)

The contracts module supports:
- contract filtering
- PDF agreement downloads
- status tracking
- service level management

---

## Unit Testing

![Unit Tests](images/UnitTest.jpeg)

The project includes successful xUnit tests for:
- currency conversion
- file validation
- business logic services

---

# Project Structure

```text
SmartContractHub/
│
├── Controllers/
├── Models/
├── Views/
├── Services/
├── Data/
├── wwwroot/
│
├── images/
│   ├── DashboardView.jpeg
│   ├── ClientView.jpeg
│   ├── ContractView.jpeg
│   └── UnitTest.jpeg
│
├── GMLS_Part2.Tests/
│
└── README.md
```

---

# Setup Instructions

## 1. Clone Repository

Open:

```text
GitHub Desktop
```

OR clone using:

```text
git clone https://github.com/DGMadombolo/GMLS_Part2.git
```

---

## 2. Open the Project

Open the solution file in:

```text
Visual Studio 2022
```

---

## 3. Restore NuGet Packages

In Visual Studio:

```text
Tools
→ NuGet Package Manager
→ Manage NuGet Packages for Solution
```

Restore all missing packages.

---

## 4. Configure Database

Open:

```text
Package Manager Console
```

Run:

```powershell
Update-Database
```

This will create the SQL Server LocalDB database automatically.

---

## 5. Run the Application

Press:

```text
Ctrl + F5
```

OR click:

```text
Start Without Debugging
```

---

# Requirements

- Windows 10/11
- Visual Studio 2022
- SQL Server LocalDB
- .NET 8 SDK

---

# Unit Testing

Run tests using:

```text
Test Explorer
```

OR using Package Manager Console:

```powershell
dotnet test
```

---

# Future Improvements

- Authentication & Authorization
- Role-based access
- Email notifications
- Real-time dashboard analytics
- Chart.js integration
- Cloud deployment
- Dark mode
- Audit logging

---

# Developer

Developed by:

## Lucky Mkhatshwa

---

# License

This project is for educational, portfolio, and demonstration purposes.
