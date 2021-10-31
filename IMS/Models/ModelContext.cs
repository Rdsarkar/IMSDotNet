using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IMS.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Shelf> Shelves { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));Persist Security Info=True;User Id=shop;Password=oracle;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SHOP")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Shelf>(entity =>
            {
                //entity.HasNoKey();

                entity.HasKey(e => e.Sid);

                entity.ToTable("SHELF");

                entity.Property(e => e.Area)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("AREA");

                entity.Property(e => e.Buildingname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BUILDINGNAME");

                entity.Property(e => e.Floor)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FLOOR");

                entity.Property(e => e.Sid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SID");

                entity.Property(e => e.Zone)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ZONE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
