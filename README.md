# KeepLock

KeepLock is a secure and scalable platform designed for people and organizations to manage sensitive data, credentials, and access controls. The system centralizes critical information, streamlines permission management, and provides robust auditing capabilities, making it ideal for organizations that require high standards of security and compliance.

## Project scope

**Development**

- The project will be a web system, developed with **C# (.NET 8)**, **Next.js** and **TypeScript**.
- The **backend** will be implemented as a **Modular Monolith** following **Clean Architecture**, **CQRS**  and **DDD** principles. The **MediatR** library will be used to implement the **CQRS pattern**.
- The **frontend** will be built in **Next.js** and **TypeScript**, providing static typing and enhanced scalability for the user interface.

**Product quality**

- Unit and integration tests (**xUnit**, **Moq** and **FluentAssertions**) will be implemented to ensure quality and code coverage.
- Quality is continuously measured via **SonarQube Cloud**.

## Project restrictions

- The system will not be a native mobile application (iOS/Android), working exclusively via a web browser.
- Offline usage is not supported.

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

- .NET 8 SDK
- Docker Desktop
- Docker Compose

### Running in Development

#### 1. Running local infrastructure

For the development environment, all infrastructure services (PostgreSQL, MongoDB, RabbitMQ) are managed with Docker.

First, start the containers:

```bash
docker-compose up -d
```

> **Services will be available on ports:**
> - **PostgreSQL:** 5432
> - **MongoDB:** 27017 
> - **Portainer:** 9000
> - **Seq (logs):** 5341

#### 2. Running the Backend

The backend runs in a separate Docker container that connects to the local infrastructure network

Build the Docker image from the project's Dockerfile

```bash
docker build -t keeplock-api .
```

Then run the container. The command below will connect the backend to the Docker Compose network.

> **Note:** Check your network name with `docker network ls`.

```bash
docker run -p 7001:8080 --network keeplock_default keeplock-api
```

#### 3. Access the Backend Application

- **Swagger UI**: `http://localhost:7001/swagger`
- **API**: `http://localhost:7001`