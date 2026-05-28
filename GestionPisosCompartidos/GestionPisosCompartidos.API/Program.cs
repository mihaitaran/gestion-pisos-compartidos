using GestionPisosCompartidos.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Repositories.Implementations;
using GestionPisosCompartidos.Services.Interfaces;
using GestionPisosCompartidos.Services.Implementations;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using GestionPisosCompartidos.Services.Integrations;

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
        policy.WithOrigins("https://localhost:7037", "http://localhost:7024")
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
}

app.Run();
