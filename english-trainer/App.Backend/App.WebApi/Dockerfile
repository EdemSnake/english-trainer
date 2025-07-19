#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all .csproj files and restore as a separate layer to optimize caching
COPY ["App.Backend/App.WebApi/App.WebApi.csproj", "App.Backend/App.WebApi/"]
COPY ["App.Backend/App.Application/App.Application.csproj", "App.Backend/App.Application/"]
COPY ["App.Backend/App.Domain/App.Domain.csproj", "App.Backend/App.Domain/"]
COPY ["App.Backend/App.Persistence/App.Persistence.csproj", "App.Backend/App.Persistence/"]
RUN dotnet restore "App.Backend/App.WebApi/App.WebApi.csproj"

# Copy the rest of the application code
COPY . .
WORKDIR "/src/App.Backend/App.WebApi"
RUN dotnet build "App.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "App.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.WebApi.dll"]