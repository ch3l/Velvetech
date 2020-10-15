using System;
using System.Collections.Generic;
using System.Text;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities.StudentAggregate
{
	public class Sex : Entity<int>, IAggregateRoot
	{
		public string Name { get; set; }
	}
}
