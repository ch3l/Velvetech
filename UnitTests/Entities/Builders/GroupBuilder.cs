using System;
using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validators;

namespace Velvetech.UnitTests.Entities.Builders
{
	static class GroupBuilder
	{
		public static async Task<Group> BuildAsync(IAsyncRepository<Group, Guid> repository, int index)
		{
			var validator = new DefaultGroupValidator();
			var group = Group.Build(validator, $"Name {index}");

			group = await repository.AddAsync(group);

			return group;
		}
	}
}
