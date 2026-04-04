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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=GestionPisosCompartidosDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gastos__3214EC0768C2D6BD");

            entity.HasIndex(e => e.ViviendaId, "IX_Gastos_Vivienda");

            entity.Property(e => e.Categoria).HasMaxLength(50);
            entity.Property(e => e.Concepto).HasMaxLength(200);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ImporteTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CreadoPor).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.CreadoPorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gastos_Creador");

            entity.HasOne(d => d.Vivienda).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gastos_Vivienda");
        });

        modelBuilder.Entity<Incidencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Incidenc__3214EC073B8F8BC3");

            entity.HasIndex(e => e.ViviendaId, "IX_Incidencias_Vivienda");

            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Abierta");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(20)
                .HasDefaultValue("Media");
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.ReportadaPor).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.ReportadaPorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Incidencias_Reportador");

            entity.HasOne(d => d.Vivienda).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Incidencias_Vivienda");
        });

        modelBuilder.Entity<InquilinosVivienda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inquilin__3214EC0715AF8759");

            entity.HasIndex(e => e.InquilinoId, "IX_InquilinosViviendas_Inquilino");

            entity.HasIndex(e => e.ViviendaId, "IX_InquilinosViviendas_Vivienda");

            entity.HasIndex(e => new { e.InquilinoId, e.ViviendaId, e.FechaInicio }, "UQ_Inquilino_Vivienda_Activo").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.Inquilino).WithMany(p => p.InquilinosVivienda)
                .HasForeignKey(d => d.InquilinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IV_Inquilino");

            entity.HasOne(d => d.Vivienda).WithMany(p => p.InquilinosVivienda)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IV_Vivienda");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3214EC076E9951C5");

            entity.HasIndex(e => e.GastoId, "IX_Pagos_Gasto");

            entity.HasIndex(e => e.InquilinoId, "IX_Pagos_Inquilino");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.Importe).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Gasto).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.GastoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagos_Gasto");

            entity.HasOne(d => d.Inquilino).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.InquilinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagos_Inquilino");
        });

        modelBuilder.Entity<TareasCalendario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TareasCa__3214EC07C59BE493");

            entity.ToTable("TareasCalendario");

            entity.HasIndex(e => e.ViviendaId, "IX_Tareas_Vivienda");

            entity.Property(e => e.AsignadaAid).HasColumnName("AsignadaAId");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.AsignadaA).WithMany(p => p.TareasCalendarioAsignadaAs)
                .HasForeignKey(d => d.AsignadaAid)
                .HasConstraintName("FK_Tareas_Asignada");

            entity.HasOne(d => d.CreadaPor).WithMany(p => p.TareasCalendarioCreadaPors)
                .HasForeignKey(d => d.CreadaPorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tareas_Creador");

            entity.HasOne(d => d.Vivienda).WithMany(p => p.TareasCalendarios)
                .HasForeignKey(d => d.ViviendaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tareas_Vivienda");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07F04C8482");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105349597BD72").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellidos).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.Rol).HasMaxLength(20);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Vivienda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vivienda__3214EC071D05259E");

            entity.HasIndex(e => e.PropietarioId, "IX_Viviendas_Propietario");

            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Direccion).HasMaxLength(300);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NumHabitaciones).HasDefaultValue(1);

            entity.HasOne(d => d.Propietario).WithMany(p => p.Vivienda)
                .HasForeignKey(d => d.PropietarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viviendas_Propietario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
