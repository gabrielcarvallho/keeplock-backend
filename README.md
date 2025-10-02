# KeepLock

## Project scope

**Development**

- The project will be a web system, developed with **C# (.NET 8)**, **Next.js** and **TypeScript**.
- The **backend** will be implemented as a **Modular Monolith** following **Clean Architecture**, **CQRS**  and **DDD** principles. The **MediatR** library will be used to implement the **CQRS pattern**.
- The **frontend** will be built in **Next.js** and **TypeScript**, providing static typing and enhanced scalability for the user interface.

**Product quality**

- Unit and integration tests (using **xUnit**) will be implemented in the backend to ensure the quality and code coverage of business rules, commands and queries.

> **Related repositories:**
> - 📚 **Documentation**: [credential-manager-docs](https://github.com/gabrielcarvallho/keeplock-docs.git) (Optional)

## Stack

- **Backend:** C#, .NET 8, ASP.NET Core
- **Backend Libraries:**
  - **Core:** MediatR, Entity Framework Core, AutoMapper
  - **Authentication:** ASP.NET Identity, JWT Bearer
  - **Validation:** FluentValidation, Newtonsoft.Json
  - **Logging and jobs**: Hangfire, Serilog
- **Frontend:** Next.js, TypeScript
- **Database:** PostgreSQL
- **Cache:** Redis
- **Tests:** xUnit
- **Infraestrutura:** Docker, GitHub Actions