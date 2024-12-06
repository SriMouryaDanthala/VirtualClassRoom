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

RUN dotnet tool install --global dotnet-ef
# Ensure that the PATH includes the directory for the global tools
ENV PATH="$PATH:/root/.dotnet/tools"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ConnectionStrings__DefaultConnection="Host=dpg-ct9f9356l47c73as0c0g-a;Port=5432;Database=virtualclassroom_5vmo;Username=virtualclassroom_5vmo_user;Password=Stjnf2Zjk2CUwbxC5aPwpFjlzA6qydlw;"

ENTRYPOINT ["dotnet", "VirtualClassRoom.dll"]
