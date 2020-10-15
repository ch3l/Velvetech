using System;
using System.Collections.Generic;
using System.Text;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities.StudentAggregate
{
	class Sex : Entity<int>
	{
		public string Name { get; private set; }
	}
}
