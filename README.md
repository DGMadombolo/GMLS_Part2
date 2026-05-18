# SmartContract Hub

SmartContract Hub is a modern enterprise-style ASP.NET Core MVC web application designed for managing clients, contracts, service requests, PDF agreements, and automated currency conversion.

The system provides a centralized dashboard for organizations to efficiently manage contract lifecycles and customer service operations.

---

# Features

## Client Management
- Create, edit, view, and delete clients
- Store client contact details and regions
- Search and filter clients

## Contract Management
- Create and manage contracts
- Upload signed agreement PDFs
- Download uploaded contracts
- Track contract statuses:
  - Active
  - Expired
  - On Hold
  - Draft

## Service Request Management
- Create service requests linked to contracts
- Track request statuses:
  - Pending
  - In Progress
  - Completed
- Automatically convert USD costs to ZAR

## Dashboard Analytics
- Enterprise-style dashboard
- Statistics cards
- Recent activity section
- Quick actions panel
- Modern responsive UI

## File Validation
- PDF validation service
- File size validation
- Secure file uploads

## Currency Conversion API
- Live USD to ZAR conversion
- External exchange rate API integration

## Unit Testing
- xUnit testing
- CurrencyService tests
- FileValidationService tests

---

# Technologies Used

## Backend
- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server LocalDB

## Frontend
- Razor Views
- Bootstrap 5
- Bootstrap Icons
- Custom CSS

## Testing
- xUnit
- Microsoft.NET.Test.Sdk

## APIs
- Exchange Rate API

---

# Project Structure

```text
GMLS_Part2/
│
├── Controllers/
├── Models/
├── Views/
├── Services/
├── Data/
├── wwwroot/
│
├── GMLS_Part2.Tests/
│
└── README.md
```

---

# Setup Instructions

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

# Dashboard Preview

The application includes:

- Enterprise dashboard
- Analytics cards
- Contract management
- Service request tracking
- PDF uploads
- Responsive design

---

# Unit Testing

Run tests using:

```bash
dotnet test
```

Tests include:
- Currency conversion calculations
- File validation logic
- Business service testing

---

# Future Improvements

- Authentication & Authorization
- Role-based access
- Real-time notifications
- Chart analytics
- Email notifications
- Dark mode
- Cloud deployment

---

# Developer

Developed by:

## Lucky Mkhatshwa

---

# License

This project is for educational and portfolio purposes.
