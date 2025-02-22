using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfApp2.Database;

public partial class BdtestTaskServerstalContext : DbContext
{
    public BdtestTaskServerstalContext()
    {
    }

    public BdtestTaskServerstalContext(DbContextOptions<BdtestTaskServerstalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<Tare> Tares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Ilias\\ILYASQL;Database=BDTestTask_Serverstal;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Cars");
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Tare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Tares");

            entity.Property(e => e.DateGross).HasColumnType("datetime");
            entity.Property(e => e.IdCar).HasColumnName("idCar");
            entity.Property(e => e.TareDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.Tares)
                .HasForeignKey(d => d.IdCar)
                .HasConstraintName("FK_Tares_Cars");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
