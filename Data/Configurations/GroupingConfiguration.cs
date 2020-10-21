using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities;

namespace Velvetech.Data.Configurations
{
	internal class GroupingConfiguration : IEntityTypeConfiguration<Grouping>
	{
		public void Configure(EntityTypeBuilder<Grouping> entity)
		{
			entity.HasKey(e => new { e.StudentId, e.GroupId });

			entity.HasOne(d => d.Group)
				 .WithMany(p => p.Grouping)
				 .HasForeignKey(d => d.GroupId);

			entity.HasOne(d => d.Student)
				.WithMany(p => p.Grouping)
				.HasForeignKey(d => d.StudentId);
		}	
	}	
}
