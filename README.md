# SmartContract Hub (GLMS Part 2)

SmartContract Hub is a modern enterprise-style ASP.NET Core solution developed using a Service-Oriented Architecture (SOA) approach. The application separates presentation, business services, and data access into independent layers to improve maintainability, scalability, and reusability.

The solution consists of:

* ASP.NET Core MVC Application (Frontend)
* ASP.NET Core Web API (Backend Services)
* Entity Framework Core
* SQL Server
* Swagger/OpenAPI Documentation
* DTO-based API Communication
* Automated Integration Testing
* Docker Containerization
* Docker Hub Image Publishing
* GitHub Actions CI/CD Pipeline

The platform enables organizations to manage clients, contracts, service requests, agreement documentation, and automated currency conversion through a centralized management system.

---

# CI/CD Pipeline

The project implements a fully automated Continuous Integration and Continuous Deployment (CI/CD) pipeline using GitHub Actions and Docker Hub.

Pipeline workflow:

```text
Developer Push
        │
        ▼
GitHub Repository
        │
        ▼
GitHub Actions
        │
        ├── Build Backend Image
        │
        ├── Build Frontend Image
        │
        ├── Authenticate with Docker Hub
        │
        ├── Push Backend Image
        │
        └── Push Frontend Image
        ▼
Docker Hub
```

### Automated Workflow

Every push to the `master` branch automatically:

* Checks out the repository source code.
* Authenticates with Docker Hub using GitHub Secrets.
* Builds the ASP.NET Core Web API Docker image.
* Builds the ASP.NET Core MVC Docker image.
* Pushes the latest images to Docker Hub.
* Maintains deployment-ready container images.

### GitHub Actions Workflow

Location:

```text
.github/workflows/docker-publish.yml
```

The workflow uses:

* actions/checkout
* docker/login-action
* Docker Build
* Docker Push

---

# Docker Hub Repository

Container images are automatically published to Docker Hub.

Published Images:

```text
freshdjstail/gmls-backend-api
freshdjstail/gmls-frontend-web
```

Benefits:

* Centralized image storage.
* Simplified deployment process.
* Version-controlled container images.
* Environment consistency across development, testing, and production.

---

# Docker Containerization

Docker support has been implemented using:

* Dockerfile (GMLS.API)
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

Container Services:

### Frontend Container

```text
Container Name:
glms-frontend-web

Technology:
ASP.NET Core MVC

Port:
5000
```

### Backend Container

```text
Container Name:
glms-backend-api

Technology:
ASP.NET Core Web API

Port:
7152
```

### Database Container

```text
Container Name:
sql-server-db

Technology:
Microsoft SQL Server 2022

Port:
1433
```

---

# DevOps Achievements

Successfully implemented:

* Docker Image Creation
* Multi-Container Docker Compose Setup
* Docker Hub Publishing
* GitHub Actions Automation
* CI/CD Pipeline Integration
* Automated Image Deployment Workflow
* Linux-based Container Development
* Service-Oriented Architecture (SOA)
* RESTful API Development
* Entity Framework Core Integration
* Automated Testing

---

# Technologies Used

## Backend

* ASP.NET Core Web API
* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQL Server

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

## Containerization & DevOps

* Docker
* Docker Compose
* Docker Hub
* GitHub Actions
* CI/CD Pipelines
* Linux (Ubuntu)

## External Services

* Exchange Rate API

---

# Developer

Lucky Mkhatshwa

Advanced Diploma ICT

Backend Developer | ASP.NET Core | Entity Framework Core | Docker | GitHub Actions | CI/CD | Azure | REST APIs | Linux

---

# License

This project is for educational, portfolio, and demonstration purposes.
