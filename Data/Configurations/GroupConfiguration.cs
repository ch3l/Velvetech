using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities.GroupAggregate;

namespace Velvetech.Data.Configurations
{									   	
	internal class GroupConfiguration
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			var navigation = builder.Metadata.FindNavigation(nameof(Group.Students));
			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.Property(b => b.Id)
				.IsRequired();

			builder.Property(b => b.Name)
				.IsRequired();
		}
	}
}
