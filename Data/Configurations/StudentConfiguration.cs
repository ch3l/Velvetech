using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities;

namespace Velvetech.Data.Configurations
{
	internal class StudentConfiguration : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.Metadata
				.FindNavigation(nameof(Student.Grouping))
				.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.HasIndex(e => e.Callsign)
				.HasName("IX_Student_Unique_Callsign")
				.IsUnique();

			builder.Property(e => e.Id)
				.HasDefaultValueSql("(newid())");

			builder.Property(e => e.Callsign)
				.HasMaxLength(16);

			builder.Property(e => e.FirstName)
				.IsRequired()
				.HasMaxLength(40);

			builder.Property(e => e.LastName)
				.IsRequired()
				.HasMaxLength(40);

			builder.Property(e => e.MiddleName)
				.HasMaxLength(60);

			builder.HasOne(d => d.Sex)
				.WithMany(p => p.Student)
				.HasForeignKey(d => d.SexId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Student_Student");
		}
	}
}
