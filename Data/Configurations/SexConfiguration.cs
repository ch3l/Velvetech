using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Velvetech.Domain.Entities;

namespace Velvetech.Data.Configurations
{
	internal class SexConfiguration : IEntityTypeConfiguration<Sex>
	{
		public void Configure(EntityTypeBuilder<Sex> builder)
		{
			builder.Metadata
				.FindNavigation(nameof(Sex.Student))
				.SetPropertyAccessMode(PropertyAccessMode.Field); 

			builder.Property(b => b.Name)
				.IsRequired()
				.HasMaxLength(10)
				.IsFixedLength();
		}
	}
}
