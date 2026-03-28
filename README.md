<div align="center">

# 🔄 Workflow Manager

[![CodeFactor](https://www.codefactor.io/repository/github/wojcikmm/workflow-manager/badge)](https://www.codefactor.io/repository/github/wojcikmm/workflow-manager)
[![.NET Core](https://img.shields.io/badge/.NET%20Core-3.1-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-10-DD0031?logo=angular)](https://angular.io/)
[![Docker](https://img.shields.io/badge/Docker-ready-2496ED?logo=docker)](https://www.docker.com/)
[![Kubernetes](https://img.shields.io/badge/Kubernetes-ready-326CE5?logo=kubernetes)](https://kubernetes.io/)

A full-stack, cloud-native **workflow management system** built with a microservices architecture, demonstrating real-world use of **CQRS**, **Event Sourcing**, **API Gateway**, and **containerized deployment**. The project also includes an alternative **modular monolith** implementation of the same business logic.

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Architecture](#-architecture)
  - [Microservices](#microservices)
  - [Modular Monolith](#modular-monolith)
- [Tech Stack](#-tech-stack)
- [CQRS & Event Sourcing](#-cqrs--event-sourcing)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Running with Docker Compose](#running-with-docker-compose)
  - [Running with Kubernetes](#running-with-kubernetes)
- [API Reference](#-api-reference)
- [Frontend](#-frontend)
- [Roadmap](#-roadmap)

---

## 🔍 Overview

**Workflow Manager** is a distributed system for defining and executing configurable business workflows. It allows users to:

- Define **processes** with custom statuses and transition rules
- Manage **users and roles** with JWT-based authentication
- Track **operations** and receive **real-time notifications** via WebSockets
- Access all services through a unified **API Gateway**

The project was built to showcase enterprise-grade software architecture patterns in a practical, end-to-end scenario. Two architectural approaches are provided side by side:

| Approach | Description |
|---|---|
| **Microservices** (primary) | Independent services communicating asynchronously via RabbitMQ, deployed on Kubernetes |
| **Modular Monolith** (alternative) | Same business logic packaged as a single deployable unit with clear internal module boundaries |

---

## 🏗 Architecture

### Microservices

The system is composed of five independent microservices behind an **Ocelot API Gateway**:

```
                         ┌──────────────────────────┐
  Browser / Client ───▶  │   API Gateway (Ocelot)   │  :8000
                         └────────────┬─────────────┘
                                      │ routes
              ┌───────────────────────┼──────────────────────┐
              │                       │                      │
   ┌──────────▼──────────┐  ┌─────────▼──────────┐  ┌───────▼────────────┐
   │  Identity Service   │  │ Configuration Svc  │  │ Operations Service │
   │     :5000           │  │      :8001          │  │      :8002         │
   │  JWT / Users/Roles  │  │  Processes/Statuses │  │  Workflow Tracking │
   └─────────────────────┘  └─────────┬──────────┘  └───────┬────────────┘
                                      │                      │
                             ┌────────▼──────────────────────▼───┐
                             │         RabbitMQ  :5672            │
                             │    (async event distribution)      │
                             └────────────────┬───────────────────┘
                                              │
                             ┌────────────────▼───────────────────┐
                             │     Notifications Service           │
                             │    SignalR WebSocket Hub            │
                             └────────────────────────────────────┘
```

#### Services at a Glance

| Service | Port | Responsibility |
|---|---|---|
| **API Gateway** | 8000 | Request routing, Swagger aggregation |
| **Identity Service** | 5000 | Authentication, JWT, user & role management |
| **Configuration Service** | 8001 | Process & status configuration (CQRS + Event Sourcing) |
| **Operations Service** | 8002 | Workflow operations tracking |
| **Notifications Service** | — | Real-time SignalR hub for event updates |
| **RabbitMQ** | 5672 / 15672 | Async message broker |
| **SQL Server** | 1433 | Persistent data store (per-service databases) |

### Modular Monolith

Located in `src/Monolith/`, this alternative demonstrates the same domain using a layered, modular design within a **single deployable unit**:

```
WorkflowManagerMonolith/
├── Core/           # Domain entities, repository interfaces, exceptions
├── Infrastructure/ # EF Core, AutoMapper, service implementations
├── Application/    # Business logic (Applications, Statuses, Transactions)
└── Web/
    ├── Server/     # ASP.NET Core API (Controllers per domain)
    ├── Client/     # Blazor / SPA client
    └── Shared/     # DTOs and shared models
```

**Key differences from the microservices approach:**

| Aspect | Microservices | Modular Monolith |
|---|---|---|
| Database | Per-service SQL Server | Single shared SQL Server |
| Communication | Async (RabbitMQ / MassTransit) | In-process, direct |
| Pattern | CQRS + Event Sourcing | Service layer |
| Deployment | Kubernetes / Docker Compose | Single container / process |
| Complexity | Higher (distributed) | Lower (simpler ops) |

---

## 🛠 Tech Stack

### Backend

| Category | Technology |
|---|---|
| **Framework** | ASP.NET Core 3.1 |
| **CQRS** | Custom `WorkflowManager.CQRS` library |
| **Event Sourcing** | NEventStore 7.0 |
| **ORM** | Entity Framework Core 3.1 |
| **Messaging** | MassTransit 7.0 + RabbitMQ |
| **API Gateway** | Ocelot |
| **Authentication** | ASP.NET Core Identity + JWT Bearer |
| **Real-time** | SignalR Core |
| **Mapping** | AutoMapper 10 |
| **API Docs** | Swashbuckle / Swagger |
| **Database** | Microsoft SQL Server |

### Frontend

| Category | Technology |
|---|---|
| **Framework** | Angular 10 |
| **Monorepo** | Nx 10 |
| **State Management** | NGXS 3.7 |
| **UI Components** | Angular Material 10 |
| **Authentication** | oidc-client 1.10 |
| **Charts** | Chart.js 2.9 |
| **Testing** | Jest + Cypress |

### Infrastructure

| Category | Technology |
|---|---|
| **Containers** | Docker |
| **Orchestration** | Kubernetes (Kustomize) |
| **Local Dev** | Docker Compose |
| **CI / Quality** | CodeFactor |

---

## ⚙️ CQRS & Event Sourcing

The **Configuration Service** (and shared library `WorkflowManager.CQRS`) implements a full CQRS + Event Sourcing pipeline:

```
  HTTP Request
      │
      ▼
  Controller
      │ maps DTO → ICommand
      ▼
  MassTransit Publisher ──▶ RabbitMQ ──▶ CommandHandler
                                               │
                                               ▼
                                     Aggregate Root
                                    (applies IEvent)
                                               │
                                               ▼
                                       Event Store (NEventStore / MSSQL)
                                               │
                                               ▼
                                     EventHandler (Read Side)
                                               │
                                               ▼
                                       Read Model (MSSQL)
                                               │
                                               ▼
                               Query returns denormalized data
```

### Core Abstractions (`WorkflowManager.CQRS`)

```csharp
// Write side
public interface ICommand {
    Guid AggregateId { get; }
    int  Version     { get; }
    Guid CorrelationId { get; }
}

public interface IEvent {
    Guid Id            { get; }
    int  Version       { get; set; }
    Guid AggregateId   { get; }
    Guid CorrelationId { get; set; }
}

// Aggregate base
public abstract class AggregateRoot {
    public IEnumerable<IEvent> GetUncommittedChanges() { ... }
    public void LoadsFromHistory(IEnumerable<IEvent> history) { ... }
    protected void ApplyEvent(IEvent @event) { ... }
}

// Read side
public interface IReadModel { }
public interface IReadModelRepository<T> where T : IReadModel { }
```

### Event Lifecycle Example — Creating a Process

1. `POST /api/processes` arrives at **Configuration Service**
2. Controller maps request → `CreateProcessCommand`
3. Command published to **RabbitMQ** via MassTransit
4. `CreateProcessCommandHandler` instantiates `Process` aggregate, applies `ProcessCreatedEvent`
5. Event persisted to **NEventStore**
6. `ProcessCreatedEventHandler` updates the **read model** in SQL Server
7. `GET /api/processes` returns the updated, denormalized view

---

## 📁 Project Structure

```
workflow-manager/
├── src/
│   ├── Services/                        # Microservices
│   │   ├── IdentityService/
│   │   ├── ConfigurationService/
│   │   ├── OperationsService/
│   │   ├── NotificationsService/
│   │   └── GatewayService/              # Ocelot API Gateway
│   ├── Monolith/                        # Alternative monolith implementation
│   │   ├── WorkflowManagerMonolith.Core/
│   │   ├── WorkflowManagerMonolith.Infrastructure/
│   │   ├── WorkflowManagerMonolith.Application/
│   │   └── WorkflowManagerMonolith.Web/
│   ├── WebApp/                          # Angular + Nx frontend
│   ├── WorkflowManager.CQRS/            # Shared CQRS abstractions & framework
│   └── WorkflowManager.Common/          # Shared utilities and base classes
├── kubernetes/
│   ├── base/                            # Base Kustomize definitions
│   │   ├── identity-service/
│   │   ├── processes-service/
│   │   └── rabbit-mq/
│   └── env/
│       └── dev/                         # Dev environment overlays
├── docker-compose.yml
└── WorkflowConfigurationService.sln
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1)
- [Node.js 14+](https://nodejs.org/) and npm
- [Docker](https://www.docker.com/get-started) and Docker Compose
- (Optional) [kubectl](https://kubernetes.io/docs/tasks/tools/) and a Kubernetes cluster for K8s deployment

### Running with Docker Compose

The fastest way to run the full stack locally:

```bash
# Clone the repository
git clone https://github.com/WojcikMM/workflow-manager.git
cd workflow-manager

# Start all services (RabbitMQ, SQL Server, all microservices)
docker-compose up --build
```

Once running, the services are available at:

| Service | URL |
|---|---|
| API Gateway | http://localhost:8000 |
| API Gateway Swagger | http://localhost:8000/swagger |
| Identity Service | http://localhost:5000 |
| Configuration Service | http://localhost:8001 |
| Operations Service | http://localhost:8002 |
| RabbitMQ Management | http://localhost:15672 |

> **Default RabbitMQ credentials:** `guest` / `guest`

### Running with Kubernetes

The project uses [Kustomize](https://kustomize.io/) for Kubernetes configuration management.

```bash
# Deploy the dev environment
kubectl apply -k kubernetes/env/dev

# Verify all pods are running
kubectl get pods

# Access the Identity Service externally (LoadBalancer)
kubectl get svc workflowmanager-identityservice-ext
```

For detailed Kubernetes configuration information, see [`kubernetes/README.md`](kubernetes/README.md).

#### Resource Limits (per pod)

| Component | CPU Request | CPU Limit | Memory Request | Memory Limit |
|---|---|---|---|---|
| API Services | 100m | 200m | 100Mi | 200Mi |
| SQL Server | 100m | 300m | 100Mi | 3Gi |

---

## 📡 API Reference

All endpoints are accessible through the API Gateway at `:8000` and documented via aggregated Swagger UI.

### Identity Service — `/api/identity/...`

| Method | Path | Auth | Description |
|---|---|---|---|
| `POST` | `/api/account/login` | — | Authenticate and receive JWT |
| `POST` | `/api/account/register` | — | Register a new user |
| `GET` | `/api/users` | — | List all users |
| `GET` | `/api/users/{id}` | ✅ | Get user by ID |
| `GET` | `/api/users/{id}/roles` | ✅ | Get roles for a user |
| `GET` | `/api/roles` | — | List all roles |

### Configuration Service — `/api/configuration/...`

| Method | Path | Auth | Role | Description |
|---|---|---|---|---|
| `GET` | `/api/processes` | — | — | List all processes |
| `GET` | `/api/processes/{id}` | — | — | Get a process |
| `POST` | `/api/processes` | ✅ | `processes_manager` | Create a process |
| `PATCH` | `/api/processes/{id}` | ✅ | `processes_manager` | Update a process |
| `GET` | `/api/statuses` | — | — | List all statuses |
| `GET` | `/api/statuses/{id}` | — | — | Get a status |
| `POST` | `/api/statuses` | ✅ | `processes_manager` | Create a status |
| `PATCH` | `/api/statuses/{id}` | ✅ | `processes_manager` | Update a status |

### Operations Service — `/api/operations/...`

| Method | Path | Auth | Description |
|---|---|---|---|
| `GET` | `/api/operations/{id}` | — | Get operation by ID |

---

## 🖥 Frontend

The Angular 10 frontend is located in `src/WebApp/` and built as a **Nx monorepo** with feature-based library organization:

```
apps/
└── workflow-manager-frontend/    # Main application shell

libs/
├── features/
│   └── management/
│       ├── processes/            # Process management UI
│       ├── statuses/             # Status configuration UI
│       └── transactions/         # Transaction management UI
└── shared/
    ├── core/                     # HTTP clients, auth guards
    ├── states/                   # NGXS state definitions
    └── theme/                    # Angular Material theme
```

To run the frontend in development:

```bash
cd src/WebApp
npm install
npx nx serve workflow-manager-frontend
```

---

## 🗺 Roadmap

- [ ] Soft delete for processes and statuses
- [ ] Restructure folder layout for cleaner separation
- [ ] Explore Kustomize advanced features (`secretGenerator`, `vars`, `images`)
- [ ] Add integration tests for microservices
- [ ] Add CI/CD pipeline configuration

---

<div align="center">

Built with ❤️ to demonstrate enterprise-grade .NET architecture patterns.

</div>
