using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Implementations;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Implementations;
using GestionPisosCompartidos.Services.Integrations;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{environment}.json", optional: true);

// Conexión a la BD
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IViviendaRepository, ViviendaRepository>();
builder.Services.AddScoped<IGastoRepository, GastoRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IIncidenciaRepository, IncidenciaRepository>();
builder.Services.AddScoped<ITareaCalendarioRepository, TareaCalendarioRepository>();
builder.Services.AddScoped<IInquilinoViviendaRepository, InquilinoViviendaRepository>();
builder.Services.AddScoped<IMensajeRepository, MensajeRepository>();

// Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IViviendaService, ViviendaService>();
builder.Services.AddScoped<IGastoService, GastoService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IIncidenciaService, IncidenciaService>();
builder.Services.AddScoped<ITareaCalendarioService, TareaCalendarioService>();
builder.Services.AddScoped<IInquilinoViviendaService, InquilinoViviendaService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMensajeService, MensajeService>();
builder.Services.AddScoped<ILugaresService, LugaresService>();

builder.Services.AddSignalR();

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7037", "http://localhost:7024", "http://localhost", "http://65.108.150.72", "http://roommate-app.duckdns.org")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<GestionPisosCompartidos.API.Hubs.ChatHub>("/chathub");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    if (!db.Usuarios.Any())
    {
        var propietario = new Usuario
        {
            Nombre = "Admin",
            Apellidos = "Propietario",
            Email = "admin@roommate.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            Rol = "Propietario",
            FechaRegistro = DateTime.UtcNow
        };

        var inquilino = new Usuario
        {
            Nombre = "Inquilino",
            Apellidos = "Demo",
            Email = "inquilino@roommate.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Inquilino123!"),
            Rol = "Inquilino",
            FechaRegistro = DateTime.UtcNow
        };

        db.Usuarios.AddRange(propietario, inquilino);
        db.SaveChanges();

        var vivienda = new Vivienda
        {
            Calle = "Rúa da Xesteira",
            Numero = "2",
            Piso = "4",
            Puerta = "A",
            Ciudad = "Perillo",
            CodigoPostal = "15172",
            Descripcion = "Piso de prueba para demo",
            PropietarioId = propietario.Id,
            FechaCreacion = DateTime.UtcNow
        };

        db.Viviendas.Add(vivienda);
        db.SaveChanges();

        db.InquilinosViviendas.Add(new InquilinosVivienda
        {
            InquilinoId = inquilino.Id,
            ViviendaId = vivienda.Id,
            FechaInicio = DateOnly.FromDateTime(DateTime.Today),
            Activo = true
        });

        var gasto = new Gasto
        {
            ViviendaId = vivienda.Id,
            Concepto = "Factura de la luz",
            Categoria = "Luz",
            ImporteTotal = 60,
            FechaGasto = DateOnly.FromDateTime(DateTime.Today),
            FechaRegistro = DateTime.UtcNow,
            CreadoPorId = propietario.Id
        };

        db.Gastos.Add(gasto);
        db.SaveChanges();

        db.Pagos.Add(new Pago
        {
            GastoId = gasto.Id,
            InquilinoId = inquilino.Id,
            Importe = 60,
            Estado = "Pendiente"
        });

        db.Incidencias.Add(new Incidencia
        {
            ViviendaId = vivienda.Id,
            ReportadaPorId = inquilino.Id,
            Titulo = "Gotera en el baño",
            Descripcion = "Hay una gotera en el techo del baño",
            Prioridad = "Alta",
            Estado = "Abierta",
            FechaCreacion = DateTime.UtcNow
        });

        db.TareasCalendarios.Add(new TareasCalendario
        {
            ViviendaId = vivienda.Id,
            Titulo = "Limpiar cocina",
            Descripcion = "Limpiar encimera y suelo",
            FechaProgramada = DateTime.Today.AddDays(2),
            CreadaPorId = propietario.Id,
            AsignadaAid = inquilino.Id,
            Completada = false,
            Recurrente = true,
            FrecuenciaDias = 7,
            FechaCreacion = DateTime.UtcNow
        });

        db.Mensajes.Add(new Mensaje
        {
            ViviendaId = vivienda.Id,
            EmisorId = propietario.Id,
            Contenido = "Bienvenido al piso, cualquier duda pregunta aquí.",
            FechaEnvio = DateTime.UtcNow
        });

        db.SaveChanges();
    }
}


app.Run();
