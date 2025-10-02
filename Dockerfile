ARG DOTNET_VERSION=8.0

# Stage 0: Base Stage
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS Build
WORKDIR /src

COPY ["*.sln", "Directory.Packages.props", "Directory.Build.props", "nuget.config", "./"]
COPY ["src/KeepLock.API/KeepLock.API.csproj", "src/KeepLock.API/"]
COPY ["src/KeepLock.Application/KeepLock.Application.csproj", "src/KeepLock.Application/"]
COPY ["src/KeepLock.Domain/KeepLock.Domain.csproj", "src/KeepLock.Domain/"]
COPY ["src/KeepLock.Infrastructure/KeepLock.Infrastructure.csproj", "src/KeepLock.Infrastructure/"]

# Restore
RUN dotnet restore "src/KeepLock.API/KeepLock.API.csproj"
COPY . .

# Build
WORKDIR "/src/KeepLock.API"
RUN dotnet build "KeepLock.API.csproj" -c Release -o /app/build

# Stage 2: Publish Stage
FROM build AS publish
RUN dotnet publish "KeepLock.API.csproj" -c Release -o /app/publish

# Stage 3: Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final

USER root
RUN apt-get update && apt-get install -y --no-install-recommends ca-certificates gnupg \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "KeepLock.API.dll" ]

# How to build and run project
# docker build -t keeplock-api:local -f Dockerfile .
# docker run --rm -it -p 7001:8080 credentialmanager-api:local