using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities.GroupAggregate;

namespace Velvetech.Data.Configurations
{									   	
	public class GroupConfiguration : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			/*var x = builder.Metadata.FindNavigation(nameof(Group.Student));
			x.SetPropertyAccessMode(PropertyAccessMode.Field);*/

			builder.Property(b => b.Id)
				.IsRequired();

			builder.Property(b => b.Name)
				.IsRequired();
		}
	}
}
