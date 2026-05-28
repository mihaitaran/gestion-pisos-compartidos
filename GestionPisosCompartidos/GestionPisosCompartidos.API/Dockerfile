FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GestionPisosCompartidos.API/GestionPisosCompartidos.API.csproj", "GestionPisosCompartidos.API/"]
COPY ["GestionPisosCompartidos.Services/GestionPisosCompartidos.Services.csproj", "GestionPisosCompartidos.Services/"]
COPY ["GestionPisosCompartidos.Repositories/GestionPisosCompartidos.Repositories.csproj", "GestionPisosCompartidos.Repositories/"]
COPY ["GestionPisosCompartidos.Models/GestionPisosCompartidos.Models.csproj", "GestionPisosCompartidos.Models/"]

RUN dotnet restore "GestionPisosCompartidos.API/GestionPisosCompartidos.API.csproj"

COPY . .

WORKDIR "/src/GestionPisosCompartidos.API"
RUN dotnet build "GestionPisosCompartidos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestionPisosCompartidos.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestionPisosCompartidos.API.dll"]