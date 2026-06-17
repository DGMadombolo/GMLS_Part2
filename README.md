# SmartContract Hub (GLMS Part 2)

SmartContract Hub is a modern enterprise-style ASP.NET Core solution built using a Service-Oriented Architecture (SOA) approach.

The solution consists of:

* ASP.NET Core MVC Application (Frontend)
* ASP.NET Core Web API (Backend Services)
* Entity Framework Core
* SQL Server LocalDB
* Swagger/OpenAPI Documentation
* DTO-based API Communication

The platform enables organizations to manage clients, contracts, service requests, agreement documentation, and automated currency conversion through a centralized management system.

---

# Architecture

```text
┌─────────────────────┐
│ ASP.NET Core MVC    │
│ (Presentation Layer)│
└──────────┬──────────┘
           │ HTTP Calls
           ▼
┌─────────────────────┐
│ ASP.NET Core Web API│
│ (Service Layer)     │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ Entity Framework    │
│ Core                │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ SQL Server LocalDB  │
└─────────────────────┘
```

---

# Features

## Client Management

* Create clients
* Edit clients
* Delete clients
* View client details
* Search clients
* Filter clients by region
* API-integrated CRUD operations

---

## Contract Management

* Create contracts
* Edit contracts
* Delete contracts
* View contract details
* Upload signed PDF agreements
* Download agreements
* Track contract status
* Filter by:

  * Status
  * Start Date
  * End Date
* API-integrated CRUD operations

### Contract Statuses

* Draft
* Active
* Expired
* On Hold

---

## Service Request Management

* Create service requests
* Edit service requests
* Delete service requests
* View service request details
* Link requests to contracts
* Track request progress
* API-integrated CRUD operations

### Request Statuses

* Pending
* In Progress
* Completed

---

## Currency Conversion

* Automatic USD to ZAR conversion
* External Exchange Rate API integration
* Real-time currency calculations

---

## File Management

* PDF agreement uploads
* PDF validation
* File size validation
* Secure file handling
* Agreement downloads

---

## Swagger API Documentation

The Web API includes:

* Swagger UI
* Endpoint testing
* Request/Response inspection
* API documentation

Endpoints include:

```text
/api/Clients
/api/Contracts
/api/ServiceRequests
```

---

## Data Transfer Objects (DTOs)

DTOs are used to:

* Separate API contracts from entities
* Improve security
* Reduce over-posting risks
* Simplify API communication

Implemented DTOs include:

```text
CreateContractDto
UpdateContractDto
UpdateContractStatusDto
```

---

# Technologies Used

## Backend

* ASP.NET Core Web API
* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQL Server LocalDB

---

## Frontend

* Razor Views
* Bootstrap 5
* Bootstrap Icons
* Custom CSS

---

## API Technologies

* RESTful API
* Swagger/OpenAPI
* DTO Pattern
* HttpClient

---

## Testing

* xUnit
* Microsoft.NET.Test.Sdk

---

## External Services

* Exchange Rate API

---

# Project Structure

```text
GLMS Solution
│
├── GLMS.API
│   ├── Controllers
│   ├── DTOs
│   ├── Models
│   ├── Data
│   └── Swagger
│
├── GMLS_Part2
│   ├── Controllers
│   ├── Models
│   ├── Views
│   ├── Services
│   ├── Data
│   └── wwwroot
│
├── GMLS_Part2.Tests
│
└── README.md
```

---

# Setup Instructions

## 1. Clone Repository

```bash
git clone https://github.com/DGMadombolo/GMLS_Part2.git
```

---

## 2. Open Solution

Open:

```text
Visual Studio 2022
```

Load:

```text
GLMS Solution.sln
```

---

## 3. Restore Packages

```text
Build
→ Restore NuGet Packages
```

---

## 4. Configure Database

Open Package Manager Console:

```powershell
Update-Database
```

---

## 5. Run Multiple Startup Projects

Configure:

```text
GLMS.API
GMLS_Part2
```

as startup projects.

---

## 6. Run Application

Start both projects:

```text
F5
```

Swagger:

```text
https://localhost:7152/swagger
```

MVC:

```text
https://localhost:<mvc-port>
```

---

# Integration Achievements

Completed:

* MVC to API communication
* CRUD operations through API
* Swagger testing
* DTO implementation
* Service-Oriented Architecture
* Entity Framework Core integration

---

# Future Improvements

* Authentication & Authorization
* Role-Based Access Control (RBAC)
* Email Notifications
* Docker Containerization
* CI/CD Pipeline
* Azure Deployment
* Real-Time Analytics
* Dashboard Charts
* Audit Logging

---

# Developer

Lucky Mkhatshwa

Advanced Diploma ICT
Backend Development | ASP.NET Core | Entity Framework Core | Docker | Azure

---

# License

This project is for educational, portfolio, and demonstration purposes.
