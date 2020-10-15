using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities.StudentAggregate;

namespace Velvetech.Data.Configurations
{
	internal class StudentConfiguration
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			var navigation = builder.Metadata.FindNavigation(nameof(Student.Groups));
			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

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
		}
	}
}
