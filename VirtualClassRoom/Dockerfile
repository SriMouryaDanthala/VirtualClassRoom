#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VirtualClassRoom/VirtualClassRoom.csproj", "VirtualClassRoom/"]
COPY ["DTO/VirtualClassRoomDTO.csproj", "DTO/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["VirtualClassRoomMediator/VirtualClassRoomMediator.csproj", "VirtualClassRoomMediator/"]
RUN dotnet restore "./VirtualClassRoom/VirtualClassRoom.csproj"
COPY . .
WORKDIR "/src/VirtualClassRoom"
RUN dotnet build "./VirtualClassRoom.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VirtualClassRoom.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ConnectionStrings__DefaultConnection="Host=dpg-cs6ebhtumphs73b7t9pg-a;Port=5432;Database=virtualclassroom_postgres_instance;Username=admin;Password=WfJw0yGLMCMhnuNbkvCN3Bg1AjTOwot3;"

ENTRYPOINT ["bash", "-c","dotnet ef database update --no-build; dotnet VirtualClassRoom.dll"]