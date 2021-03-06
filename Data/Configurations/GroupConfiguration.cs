﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities;

namespace Velvetech.Data.Configurations
{
	internal class GroupConfiguration : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.Metadata
				.FindNavigation(nameof(Group.Grouping))
				.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.Property(b => b.Id)
				.IsRequired();

			builder.Property(b => b.Name)
				.IsRequired();
		}
	}
}
