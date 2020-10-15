using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Velvetech.Domain.Entities.GroupAggregate;

namespace Velvetech.Data.Configurations
{									   	
	internal class GroupStudentsConfiguration
	{
		public void Configure(EntityTypeBuilder<GroupStudent> builder)
		{
			//var navigation = builder.Metadata.FindNavigation(nameof(Group.Students));
			//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

			builder.Property(b => b.Id)
				.IsRequired();

			builder.HasOne(x => x.Group)
				.WithMany(x => x.Grouping)
				.HasForeignKey(x => x.GroupId)
				.HasConstraintName("FK_Grouping_Group");

			builder.HasOne(x => x.Student)
				.WithMany(x => x.Grouping)
				.HasForeignKey(x => x.StudentId)
				.HasConstraintName("FK_Grouping_Student");
		}	
	}
}
