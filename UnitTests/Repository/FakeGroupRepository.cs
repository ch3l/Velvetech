using System;
using System.Collections.Generic;
using System.Text;
using Velvetech.Domain.Entities;
using Velvetech.UnitTests.Repository.Base;

namespace Velvetech.UnitTests.Repository
{
	class FakeGroupRepository : FakeRepository<Group, Guid>
	{
		protected override Guid NewKey()
		{
			return Guid.NewGuid();
		}
	}
}
