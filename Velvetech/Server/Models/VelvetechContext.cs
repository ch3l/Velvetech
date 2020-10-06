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
					.UseSqlServer("Server=.\\sqlexpress;Database=Velvetech;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Grouping>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.GroupId });

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Grouping)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grouping_Group");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grouping)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grouping_Student");
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.Callsign)
                    .HasName("IX_Student_Unique_Callsign")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.SexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
