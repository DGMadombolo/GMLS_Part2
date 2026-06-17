# SmartContract Hub (GLMS Part 2)

SmartContract Hub is a modern enterprise-style ASP.NET Core solution developed using a Service-Oriented Architecture (SOA) approach. The application separates presentation, business services, and data access into independent layers to improve maintainability, scalability, and reusability.

The solution consists of:

* ASP.NET Core MVC Application (Frontend)
* ASP.NET Core Web API (Backend Services)
* Entity Framework Core
* SQL Server LocalDB
* Swagger/OpenAPI Documentation
* DTO-based API Communication
* Automated Integration Testing
* Docker Containerization Support

The platform enables organizations to manage clients, contracts, service requests, agreement documentation, and automated currency conversion through a centralized management system.

---

# Architecture

```text
┌─────────────────────┐
│ ASP.NET Core MVC    │
│ (Presentation Layer)│
└──────────┬──────────┘
           │ HttpClient
           ▼
┌─────────────────────┐
│ ASP.NET Core Web API│
│ (Service Layer)     │
└──────────┬──────────┘
           │ EF Core
           ▼
┌─────────────────────┐
│ SQL Server LocalDB  │
│ (Data Layer)        │
└─────────────────────┘
```

---

# Key Features

## Client Management

* Create clients
* Edit clients
* Delete clients
* View client details
* Search clients
* Filter clients
* API-driven CRUD operations

---

## Contract Management

* Create contracts
* Edit contracts
* Delete contracts
* View contract details
* Upload signed PDF agreements
* Download agreements
* Contract status tracking
* Date filtering
* Service level management

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
* Request status tracking
* Automatic currency conversion
* Contract validation rules

### Request Statuses

* Pending
* In Progress
* Completed

---

## Business Rules

Implemented business validation includes:

* Requests cannot be created for expired contracts.
* Requests cannot be created for contracts on hold.
* Automatic USD to ZAR conversion.
* PDF file validation before upload.

---

## Swagger API Documentation

The Web API includes:

* Interactive Swagger UI
* Endpoint testing
* Request/Response inspection
* API documentation

Available endpoints:

```text
/api/Clients
/api/Contracts
/api/ServiceRequests
```

---

## DTO Implementation

The API uses DTOs to:

* Separate API contracts from database entities.
* Prevent over-posting attacks.
* Improve maintainability.
* Control data exposure.

Implemented DTOs:

```text
CreateContractDto
UpdateContractDto
UpdateContractStatusDto
```

---

## Automated Testing

The project includes automated testing using xUnit.

### Unit Tests

* CurrencyServiceTests
* FileValidationServiceTests

### Integration Tests

* ClientsApiTests
* ContractsApiTests
* ServiceRequestsApiTests

### Test Results

```text
11 Tests Passed
0 Failed
0 Skipped
```

---

## Docker Containerization

Docker support has been implemented using:

* Dockerfile (GLMS.API)
* Dockerfile (GMLS_Part2)
* docker-compose.yml
* docker-compose.override.yml

Container architecture:

```text
glms-frontend-web
        │
        ▼
glms-backend-api
        │
        ▼
sql-server-db
```

Docker Compose is used to orchestrate communication between services and ensure consistency across environments.

---

# Technologies Used

## Backend

* ASP.NET Core Web API
* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQL Server LocalDB

## Frontend

* Razor Views
* Bootstrap 5
* Bootstrap Icons
* Custom CSS

## API Technologies

* RESTful API
* Swagger/OpenAPI
* DTO Pattern
* HttpClient

## Testing

* xUnit
* Microsoft.NET.Test.Sdk
* Integration Testing

## Containerization

* Docker
* Docker Compose

## External Services

* Exchange Rate API

---

# Integration Achievements

Successfully completed:

* Service-Oriented Architecture (SOA)
* MVC to API communication
* HttpClient integration
* CRUD operations through API
* Swagger testing
* DTO implementation
* Automated testing
* Docker containerization setup
* Entity Framework Core integration

---

# Future Improvements

* Authentication & Authorization
* Role-Based Access Control (RBAC)
* Email Notifications
* Azure Deployment
* CI/CD Pipeline
* Real-Time Analytics
* Dashboard Charts
* Audit Logging

---

# Developer

Lucky Mkhatshwa

Advanced Diploma ICT

Backend Development | ASP.NET Core | Entity Framework Core | Docker | Azure | REST APIs

---

# License

This project is for educational, portfolio, and demonstration purposes.
