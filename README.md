# KeepLock

## Project scope

**Development**

- The project will be a web system, developed with **C# (.NET 8)**, **Next.js** and **TypeScript**.
- The **backend** will be implemented as a **Modular Monolith** following **Clean Architecture**, **CQRS**  and **DDD** principles. The **MediatR** library will be used to implement the **CQRS pattern**.
- The **frontend** will be built in **Next.js** and **TypeScript**, providing static typing and enhanced scalability for the user interface.

> **Related repositories:**
> - 💻 **Frontend:** [keeplock-frontend](https://github.com/gabrielcarvallho/keeplock-frontend.git)
> - 📚 **Documentation**: [keeplock-docs](https://github.com/gabrielcarvallho/keeplock-docs.git)

## Stack

- **Backend:** C#, .NET 8, ASP.NET Core
- **Backend Libraries:**
  - **Core:** MediatR, Entity Framework Core, AutoMapper
  - **Authentication:** ASP.NET Identity, JWT Bearer
  - **Validation:** FluentValidation, Newtonsoft.Json
  - **Logging and jobs**: Hangfire, Serilog
- **Frontend:** Next.js, TypeScript
- **Databases:**
  - **Relational:** PostgreSQL  
  - **NoSQL:** MongoDB
- **Tests:** xUnit
- **Infrastructure:** Docker

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)

### Running the Infrastructure

The project uses Docker Compose to manage external dependencies (PostgreSQL, MongoDB, Seq, and Portainer).

1. **Start all infrastructure services:**
   ```bash
   docker-compose up -d
   ```

2. **Verify all containers are running:**
   ```bash
   docker-compose ps
   ```

4. **Stop all services:**
   ```bash
   docker-compose stop
   ```

5. **Remove all containers (keeps volumes):**
   ```bash
   docker-compose down
   ```

6. **Remove all containers and volumes:**
   ```bash
   docker-compose down -v
   ```

### Running the API

**Option 1: Using Visual Studio / Rider**
- Open `KeepLock.sln`
- Set `KeepLock.API` as startup project
- Press F5 or click Run

**Option 2: Using .NET CLI**
```bash
cd src/KeepLock.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger: `http://localhost:5000/swagger`

### Building with Docker

To build and run the API in a Docker container:

```bash
# Build the image
docker build -t keeplock-api:local -f Dockerfile .

# Run the container
docker run --rm -it -p 7001:8080 keeplock-api:local
```

The API will be available at `http://localhost:7001`

## Project Structure

```
KeepLock/
├── src/
│   ├── KeepLock.API/          # Presentation Layer (Controllers, Middleware)
│   ├── KeepLock.Application/  # Application Layer (Use Cases, CQRS)
│   ├── KeepLock.Domain/       # Domain Layer (Entities, Value Objects)
│   └── KeepLock.Infrastructure/ # Infrastructure Layer (Data Access, External Services)
├── docker/
│   └── docker-compose.yml     # Infrastructure services configuration
├── Dockerfile                 # API containerization
└── KeepLock.sln              # Solution file

```
