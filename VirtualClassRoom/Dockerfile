# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files for all necessary projects
COPY ["VirtualClassRoom/VirtualClassRoom.csproj", "VirtualClassRoom/"]
COPY ["DTO/VirtualClassRoomDTO.csproj", "DTO/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["VirtualClassRoomMediator/VirtualClassRoomMediator.csproj", "VirtualClassRoomMediator/"]

# Restore dependencies
RUN dotnet restore "./VirtualClassRoom/VirtualClassRoom.csproj"

# Copy everything else
COPY . .

# Build the Persistence project
WORKDIR "/src/Persistence"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Build the main application
WORKDIR "/src/VirtualClassRoom"
RUN dotnet build "./VirtualClassRoom.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Install the EF Core tools globally
RUN dotnet tool install --global dotnet-ef

# Ensure that the PATH includes the directory for the global tools
ENV PATH="$PATH:/root/.dotnet/tools"

# Publish the main application
WORKDIR "/src/VirtualClassRoom"
RUN dotnet publish "./VirtualClassRoom.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Run migrations to update the database
WORKDIR /src/Persistence
RUN dotnet ef database update --no-build --project Persistence.csproj

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the connection string environment variable
ENV ConnectionStrings__DefaultConnection="Host=dpg-cs6ebhtumphs73b7t9pg-a;Port=5432;Database=virtualclassroom_postgres_instance;Username=admin;Password=WfJw0yGLMCMhnuNbkvCN3Bg1AjTOwot3;"

# Set the entry point for the application
ENTRYPOINT ["dotnet", "VirtualClassRoom.dll"]
