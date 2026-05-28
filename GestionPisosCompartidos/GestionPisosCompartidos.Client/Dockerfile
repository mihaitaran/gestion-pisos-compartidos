FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GestionPisosCompartidos.Client/GestionPisosCompartidos.Client.csproj", "GestionPisosCompartidos.Client/"]
COPY ["GestionPisosCompartidos.Models/GestionPisosCompartidos.Models.csproj", "GestionPisosCompartidos.Models/"]

RUN dotnet restore "GestionPisosCompartidos.Client/GestionPisosCompartidos.Client.csproj"

COPY . .

WORKDIR "/src/GestionPisosCompartidos.Client"
RUN dotnet publish "GestionPisosCompartidos.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
COPY ["GestionPisosCompartidos.Client/nginx.conf", "/etc/nginx/nginx.conf"]