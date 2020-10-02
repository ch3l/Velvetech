using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Velvetech.Server.Models
{
    public partial class VelvetechContext : DbContext
    {
        public VelvetechContext()
        {
        }

        public VelvetechContext(DbContextOptions<VelvetechContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Grouping> Grouping { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder
					.UseLazyLoadingProxies()
					.UseSqlServer("Server=.\\SQLExpress;Database=Velvetech;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Grouping>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.GroupId });
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Callsign)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Patronymic).HasMaxLength(60);

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.SexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Sex");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
