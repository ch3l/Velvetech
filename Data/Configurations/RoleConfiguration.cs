
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities;

namespace Velvetech.Data.Configurations
{
	internal class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> entity)
		{
			entity.Metadata
				.FindNavigation(nameof(Role.UserRole))
				.SetPropertyAccessMode(PropertyAccessMode.Field);

			entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(40);
		}
	}
}
