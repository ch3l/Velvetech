using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
	abstract class Entity<TId>
	{
		public TId Id { get; private set; }		
	}
}
