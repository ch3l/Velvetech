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
			var group = new Group();
			var validator = new GroupValidator();
			
			group = await repository.AddAsync(group);
			group.SelectValidator(validator);
			group.SetName($"Name {index}");

			return group;
		}
	}
}
