using GestionPisosCompartidos.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Repositories.Implementations;
using GestionPisosCompartidos.Services.Interfaces;
using GestionPisosCompartidos.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Conexión a la BD
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IViviendaRepository, ViviendaRepository>();
builder.Services.AddScoped<IGastoRepository, GastoRepository>();

// Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IViviendaService, ViviendaService>();
builder.Services.AddScoped<IGastoService, GastoService>();

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
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
