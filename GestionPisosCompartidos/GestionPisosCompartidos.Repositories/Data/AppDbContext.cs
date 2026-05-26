using GestionPisosCompartidos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gasto> Gastos { get; set; }
    public virtual DbSet<Incidencia> Incidencias { get; set; }
    public virtual DbSet<InquilinosVivienda> InquilinosViviendas { get; set; }
    public virtual DbSet<Pago> Pagos { get; set; }
    public virtual DbSet<TareasCalendario> TareasCalendarios { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Vivienda> Viviendas { get; set; }
    public virtual DbSet<Mensaje> Mensajes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // USUARIO
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Apellidos).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.Rol).HasMaxLength(20);
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            // CHECK de Rol
            entity.ToTable(t => t.HasCheckConstraint("CK_Usuarios_Rol",
                "Rol IN ('Propietario', 'Inquilino')"));
        });

        // VIVIENDA
        modelBuilder.Entity<Vivienda>(entity =>
        {
            entity.HasIndex(e => e.PropietarioId);

            entity.Property(e => e.Direccion).HasMaxLength(300);
            entity.Property(e => e.Numero).HasMaxLength(10);
            entity.Property(e => e.Piso).HasMaxLength(10);
            entity.Property(e => e.Puerta).HasMaxLength(10);
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Propietario).WithMany(p => p.Vivienda)
                .HasForeignKey(d => d.PropietarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // INQUILINOS VIVIENDA
        modelBuilder.Entity<InquilinosVivienda>(entity =>
        {
            entity.HasIndex(e => e.InquilinoId);
            entity.HasIndex(e => e.ViviendaId);
            entity.HasIndex(e => new { e.InquilinoId, e.ViviendaId, e.FechaInicio });

            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.Inquilino).WithMany(p => p.InquilinosVivienda)
                .HasForeignKey(d => d.InquilinoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Vivienda).WithMany(p => p.InquilinosVivienda)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        });

        // GASTO
        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasIndex(e => e.ViviendaId);

            entity.Property(e => e.Concepto).HasMaxLength(200);
            entity.Property(e => e.Categoria).HasMaxLength(50);
            entity.Property(e => e.ImporteTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            // CHECK de Categoria
            entity.ToTable(t => t.HasCheckConstraint("CK_Gastos_Categoria",
                "Categoria IN ('Luz', 'Agua', 'Gas', 'Internet', 'Comunidad', 'Alquiler', 'Otro')"));

            // CHECK de ImporteTotal
            entity.ToTable(t => t.HasCheckConstraint("CK_Gastos_ImporteTotal",
                "ImporteTotal > 0"));

            entity.HasOne(d => d.CreadoPor).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.CreadoPorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Vivienda).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // PAGO
        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasIndex(e => e.GastoId);
            entity.HasIndex(e => e.InquilinoId);

            entity.Property(e => e.Importe).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Estado).HasMaxLength(20).HasDefaultValue("Pendiente");

            // CHECK de Estado
            entity.ToTable(t => t.HasCheckConstraint("CK_Pagos_Estado",
                "Estado IN ('Pendiente', 'Pagado', 'Rechazado')"));

            // CHECK de Importe
            entity.ToTable(t => t.HasCheckConstraint("CK_Pagos_Importe",
                "Importe > 0"));

            entity.HasOne(d => d.Gasto).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.GastoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Inquilino).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.InquilinoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // INCIDENCIA
        modelBuilder.Entity<Incidencia>(entity =>
        {
            entity.HasIndex(e => e.ViviendaId);

            entity.Property(e => e.Titulo).HasMaxLength(200);
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Prioridad).HasMaxLength(20).HasDefaultValue("Media");
            entity.Property(e => e.Estado).HasMaxLength(20).HasDefaultValue("Abierta");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            // CHECK de Prioridad
            entity.ToTable(t => t.HasCheckConstraint("CK_Incidencias_Prioridad",
                "Prioridad IN ('Baja', 'Media', 'Alta', 'Urgente')"));

            // CHECK de Estado
            entity.ToTable(t => t.HasCheckConstraint("CK_Incidencias_Estado",
                "Estado IN ('Abierta', 'EnProceso', 'Resuelta', 'Cerrada')"));

            entity.HasOne(d => d.ReportadaPor).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.ReportadaPorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Vivienda).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // TAREAS CALENDARIO
        modelBuilder.Entity<TareasCalendario>(entity =>
        {
            entity.ToTable("TareasCalendario");

            entity.HasIndex(e => e.ViviendaId);

            entity.Property(e => e.AsignadaAid).HasColumnName("AsignadaAId");
            entity.Property(e => e.Titulo).HasMaxLength(200);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AsignadaA).WithMany(p => p.TareasCalendarioAsignadaAs)
                .HasForeignKey(d => d.AsignadaAid);

            entity.HasOne(d => d.CreadaPor).WithMany(p => p.TareasCalendarioCreadaPors)
                .HasForeignKey(d => d.CreadaPorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Vivienda).WithMany(p => p.TareasCalendarios)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // MENSAJE
        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasIndex(e => e.ViviendaId);
            entity.HasIndex(e => e.EmisorId);

            entity.Property(e => e.Contenido).HasMaxLength(2000);
            entity.Property(e => e.FechaEnvio).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Vivienda).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Emisor).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.EmisorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}