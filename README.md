# RoomMate: Sistema Web para la Gestión Integral de Pisos Compartidos

Aplicación web full-stack para la gestión de pisos compartidos entre propietarios e inquilinos. Permite administrar viviendas, gastos, pagos, incidencias, tareas del hogar y comunicación en tiempo real.

## Tecnologías

- **Backend**: ASP.NET Core (.NET 10) — API REST
- **Frontend**: Blazor WebAssembly + MudBlazor
- **Base de datos**: SQL Server 2022 + Entity Framework Core (Code First)
- **Autenticación**: JWT
- **Chat en tiempo real**: SignalR
- **APIs externas**: OpenStreetMap (Nominatim + Overpass)
- **Despliegue**: Docker + Hetzner VPS

## Funcionalidades principales

### Propietario
- Gestión de viviendas e inquilinos
- Registro y reparto de gastos entre inquilinos
- Control del estado de pagos
- Gestión de incidencias (cambio de estado)
- Creación y asignación de tareas del hogar
- Chat en tiempo real con los inquilinos

### Inquilino
- Consulta y gestión de sus pagos pendientes
- Reporte de incidencias
- Participación en tareas asignadas
- Chat en tiempo real con los compañeros de piso

### Compartidas
- Calendario de tareas mensual con tareas recurrentes
- Búsqueda de lugares cercanos a la vivienda (supermercados, farmacias, restaurantes...)
- Autocompletado de direcciones al registrar una vivienda
- Edición de perfil y cambio de contraseña

## Modelo de datos

Las entidades principales del sistema son:

- **Usuario** — base de la especialización Propietario/Inquilino
- **Vivienda** — gestionada por un propietario
- **InquilinosVivienda** — interrelación entre Inquilino y Vivienda (con historial)
- **Gasto** — registrado por un usuario y repartido entre inquilinos
- **Pago** — generado automáticamente al crear un gasto
- **Incidencia** — reportada por un inquilino en su vivienda
- **TareaCalendario** — creada y/o asignada a usuarios de la vivienda
- **Mensaje** — mensajes del chat de la vivienda

## Ejecutar con Docker (recomendado)

### Requisitos
- Docker Desktop instalado y en ejecución

### Pasos

1. Clona el repositorio:
```bash
git clone https://github.com/mihaitaran/gestion-pisos-compartidos.git
cd gestion-pisos-compartidos/GestionPisosCompartidos
```

2. Levanta los contenedores:
```bash
docker compose up --build
```

3. Abre el navegador en:
```
http://localhost
```

La API estará disponible en `http://localhost:8080` y la base de datos se crea automáticamente con todas las migraciones aplicadas.

Para parar los contenedores:
```bash
docker compose down
```

> Los datos de la base de datos se persisten en un volumen Docker y no se pierden al parar los contenedores. Para borrar también los datos usa `docker compose down -v`.

## Ejecutar en local (desarrollo)

### Requisitos
- .NET 10 SDK
- SQL Server (local o Express)
- Visual Studio 2026

### Pasos

1. Configura la cadena de conexión en `GestionPisosCompartidos.API/appsettings.json`
2. Aplica las migraciones:
```
Update-Database -Project GestionPisosCompartidos.Repositories -StartupProject GestionPisosCompartidos.API
```
3. Asegúrate de que `GestionPisosCompartidos.Client/wwwroot/appsettings.json` tiene:
```json
{
  "ApiUrl": "https://localhost:7024"
}
```
4. Ejecuta el proyecto desde Visual Studio

## Demo en producción

La aplicación está desplegada en: [http://roommate-app.duckdns.org](http://roommate-app.duckdns.org)

## Estructura del proyecto

```
GestionPisosCompartidos/
├── GestionPisosCompartidos.API/          # Controllers, Hubs, Program.cs
├── GestionPisosCompartidos.Client/       # Blazor WebAssembly (frontend)
├── GestionPisosCompartidos.Models/       # Entidades y DTOs
├── GestionPisosCompartidos.Repositories/ # EF Core, AppDbContext, Migrations
├── GestionPisosCompartidos.Services/     # Lógica de negocio
├── GestionPisosCompartidos.Tests/        # Tests unitarios (xUnit)
└── docker-compose.yml
```

## Tests

El proyecto incluye tests unitarios con xUnit, Moq y FluentAssertions:

```
dotnet test
```

## Autor

Mihai Taran
