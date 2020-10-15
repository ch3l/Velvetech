using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Velvetech.Domain.Entities.GroupAggregate;
using Velvetech.Domain.Entities.StudentAggregate;

namespace Velvetech.Data.Configurations
{
	class SexConfiguration
	{
		public void Configure(EntityTypeBuilder<Sex> builder)
		{
			builder.Property(b => b.Id)
				.IsRequired();

			builder.Property(b => b.Name)
				.IsRequired()
				.HasMaxLength(10)
				.IsFixedLength();
		}
	}
}
