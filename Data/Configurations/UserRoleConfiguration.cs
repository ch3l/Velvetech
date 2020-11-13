using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities;

namespace Velvetech.Data.Configurations
{
	internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
	{
		public void Configure(EntityTypeBuilder<UserRole> entity)
		{
			entity.HasKey(e => new { e.UserId, e.RoleId });

			entity.HasOne(d => d.User)
				.WithMany(p => p.UserRole)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_UserRole_User");

			entity.HasOne(d => d.Role)
				.WithMany(p => p.UserRole)
				.HasForeignKey(d => d.RoleId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_UserRole_Role");
		}	
	}	
}
