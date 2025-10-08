ARG DOTNET_VERSION=8.0

# Stage 0: Base Stage
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

COPY ["*.sln", "./"]
COPY ["src/KeepLock.API/KeepLock.API.csproj", "src/KeepLock.API/"]
COPY ["src/KeepLock.Application/KeepLock.Application.csproj", "src/KeepLock.Application/"]
COPY ["src/KeepLock.Domain/KeepLock.Domain.csproj", "src/KeepLock.Domain/"]
COPY ["src/KeepLock.Infrastructure/KeepLock.Infrastructure.csproj", "src/KeepLock.Infrastructure/"]

RUN dotnet restore "src/KeepLock.API/KeepLock.API.csproj"
COPY . .

WORKDIR "/src/src/KeepLock.API"
RUN dotnet build "KeepLock.API.csproj" -c Release -o /app/build

# Stage 2: Publish Stage
FROM build AS publish
RUN dotnet publish "KeepLock.API.csproj" -c Release -o /app/publish

# Stage 3: Run Stage
FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "KeepLock.API.dll" ]