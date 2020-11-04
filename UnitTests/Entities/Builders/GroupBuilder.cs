using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.Internal.Interfaces;

namespace Velvetech.UnitTests.Entities.Builders
{
	class GroupBuilder
	{
		public async Task<Group> Build(IAsyncRepository<Group, Guid> repository, int index)
		{
			var validator = new GroupValidator();
			var group = Group.Build(validator, $"Name {index}");
			
			group = await repository.AddAsync(group);
			
			return group;
		}
	}
}
