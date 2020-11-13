﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities;

namespace Velvetech.Data.Configurations
{
	internal class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> entity)
		{
			entity.Metadata
				.FindNavigation(nameof(User.UserRole))
				.SetPropertyAccessMode(PropertyAccessMode.Field);

			entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(30);
		}
	}
}
