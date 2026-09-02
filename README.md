# KiaKooshar Identity Service

A production-grade **Identity & Authorization microservice** built with **.NET 9**, following **Clean Architecture** and **CQRS (Vertical Slice)** principles. This service handles authentication, role/permission-based authorization, user management, auditing, and background job processing for the KiaKooshar platform.

---

## 🏗️ Architecture

The solution follows **Clean Architecture** with strict dependency direction (`Domain ← Application ← Infrastructure ← Presentation`):

```
KiaKooshar.Domain          → Entities, Enums, no external dependencies
KiaKooshar.Application     → CQRS Handlers (MediatR), DTOs, Interfaces/Abstractions
KiaKooshar.Infrastructure  → EF Core, Redis, Hangfire, Serilog, external services
KiaKooshar.Presentation    → API Controllers, Middleware, Swagger, Program.cs (composition root)
```

Feature organization follows a **Vertical Slice** pattern within the Application layer — each feature (e.g. `Users`, `Roles`, `Permissions`, `Auth`) is self-contained with its own Commands, Queries, Handlers, and Validators, rather than being split horizontally by technical layer.

---

## ✅ Implemented Features

### 🔐 Authentication & Authorization
- **JWT-based Authentication** — short-lived Access Tokens + long-lived, hashed Refresh Tokens (SHA-256, stored server-side with rotation & revocation support)
- **Role-based and Permission-based Authorization** — custom `[HasPermission]` attribute built on a dynamic `IAuthorizationPolicyProvider`, avoiding the need to manually register a policy per permission
- Refresh token rotation with reuse detection, device/IP tracking, and forced logout support
- **User Sessions** tracking — per-device session records with revoke-single / revoke-all-sessions capability
- Login / Logout event logging (structured, via Serilog)

### 🗄️ Data Access & Persistence
- **EF Core 8** with **Fluent API** mappings (one configuration class per entity, applied via `ApplyConfigurationsFromAssembly`)
- **Soft Delete** — global query filter applied automatically to all entities inheriting `BaseEntity`
- **Row Versioning / Optimistic Concurrency** — `RowVersion` (SQL `rowversion`) applied globally to detect concurrent update conflicts
- **Entity Change Tracker / Audit Log** — captures Added/Modified/Deleted state on every `SaveChangesAsync`, logging only the *changed* properties (old value → new value), including the acting user, IP, and timestamp
- **Runtime Data Seeding** — idempotent seeders for Roles, Permissions, and Role-Permission mappings (avoids `HasData` migration snapshot issues from non-deterministic values)
- **Automatic Migrations** on application startup (`Database.MigrateAsync()`)

### ⚡ Caching & Resilience
- **Redis distributed caching** + in-memory caching, with a shared `ICacheService` abstraction
- Cached user profile including resolved **Roles** and **Permissions** (pre-joined via `UserRole → Role → RolePermission → Permission`) to minimize DB round-trips on every authorization check
- **Polly** resilience pipelines (Retry with exponential backoff + jitter, Timeout, Fallback) wrapping Redis operations, with structured logging on each retry/fallback event

### 🕒 Background Processing
- **Hangfire** for scheduled and background jobs, backed by SQL Server storage, with a protected `/hangfire` dashboard
- Job execution logic lives in the **Application layer** (framework-agnostic), decoupled from Hangfire via an `IBackgroundJobScheduler` abstraction implemented in Infrastructure
- Recurring jobs include: expired/revoked Refresh Token cleanup, Audit Log archiving

### 📊 Observability & Monitoring
- **Serilog** structured logging, sinked to **SEQ** for centralized log search/analysis
- **Health Checks** for SQL Server, Redis, and custom dependencies, split into `/health/live` (liveness) and `/health/ready` (readiness) endpoints
- **Health Checks UI** dashboard for visual, historical uptime monitoring
- MediatR pipeline behaviors for cross-cutting concerns: `LoggingBehavior` (logs every request/response) and `ValidationBehavior` (FluentValidation integration)

### 🛡️ API Hardening
- **Rate Limiting** (ASP.NET Core built-in, Fixed Window policy)
- **API Versioning** integrated with Swagger/Swashbuckle
- Centralized **Global Exception Handling** middleware
- **User Secrets** for local development configuration (no secrets committed to source control)

### 🧰 Code Quality & Patterns
- **CQRS** via MediatR (separate Commands/Queries, one Handler per use case)
- **Repository + Unit of Work** pattern abstracting EF Core from the Application layer
- **Cancellation Token** propagation through the entire request pipeline
- **Pagination** implemented as a reusable `IQueryable<T>` extension method
- Consistent use of **Func / Action / Predicate** delegates and **Expression Trees** for composable, EF-translatable query filters
- Clear separation of **DTO vs. ViewModel vs. Entity** responsibilities across layers
- **Regex** validation utilities for input sanitization

---

## 🛠️ Tech Stack

| Category | Technology |
|---|---|
| Framework | .NET 8, ASP.NET Core Web API |
| Data Access | Entity Framework Core, SQL Server |
| Caching | Redis, In-Memory Cache |
| Background Jobs | Hangfire |
| Logging | Serilog + SEQ |
| Resilience | Polly |
| Mediator / CQRS | MediatR |
| Validation | FluentValidation |
| Mapping | AutoMapper |
| API Docs | Swashbuckle (Swagger) with API Versioning |
| Auth | JWT Bearer Authentication |

---

## 📁 Project Structure

```
KiaKooshar/
├── Domain/
│   ├── Entities/            # BaseEntity, User, Role, Permission, RefreshToken, AuditLog, ...
│   ├── Constants/            # Roles, Permissions string constants
│   └── Enums/
├── Application/
│   ├── Features/
│   │   ├── Identities/Users/       # Commands, Queries, Handlers, Validators
│   │   ├── Identities/Admin/       # User management (Enable/Disable/Block), Role/Permission assignment
│   │   └── Jobs/                    # Background job definitions (framework-agnostic)
│   ├── Behaviors/                   # LoggingBehavior, ValidationBehavior
│   ├── DTOs/
│   └── Construct/DataBases/         # IUnitOfWork, repository interfaces
├── Infrastructure/
│   ├── Persistence/
│   │   ├── Mappings/                 # Fluent API IEntityTypeConfiguration classes
│   │   ├── Repositories/
│   │   └── Seed/                      # Runtime seeders (Roles, Permissions)
│   ├── AuditLog/                      # SaveChanges interceptor logic
│   ├── BackgroundJobs/                # Hangfire scheduler implementation
│   ├── Caching/                       # Redis/Memory cache service, Polly resilience
│   └── Authorization/                 # Permission-based policy provider & handler
└── Presentation/
    ├── Controllers/
    │   ├── Auth/
    │   └── Admin/UserManagement/
    ├── Middleware/                     # GlobalExceptionHandler
    └── Program.cs
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (local or containerized)
- Redis instance
- SEQ instance (optional, for centralized logging)

### Setup

```bash
# Restore dependencies
dotnet restore

# Apply User Secrets (local development)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
dotnet user-secrets set "Jwt:Key" "your-secret-key"

# Run the application (migrations + seeding apply automatically on startup)
dotnet run --project Presentation
```

### Key Endpoints

| Endpoint | Purpose |
|---|---|
| `/swagger` | API documentation |
| `/hangfire` | Background job dashboard (authenticated) |
| `/health` | Full health check report |
| `/health/live` | Liveness probe |
| `/health/ready` | Readiness probe (checks DB, Redis) |

---

## 🔒 Security Notes

- Refresh tokens are **never stored in plaintext** — only their SHA-256 hash is persisted
- Passwords are hashed using industry-standard algorithms before storage
- The Hangfire dashboard is protected by a custom `IDashboardAuthorizationFilter`
- Sensitive fields (password hashes, token hashes) are explicitly excluded from Audit Log capture
- All audit entries record the acting user, IP address, and UTC timestamp for traceability

---

## 📌 Roadmap

Planned/in-progress items not yet covered above:
- SignalR for real-time notifications
- Role-based dynamic menu rendering (frontend)
- CAPTCHA integration on sensitive endpoints
- Forced logout on critical account changes (password/email change)
- Admin group management UI
- Distributed transactions / Saga pattern (RabbitMQ or Kafka) for cross-service consistency

---

## 📄 License

Internal project — KiaKooshar. All rights reserved.
