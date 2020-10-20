using System;
using System.Collections.Generic;
using System.Text;

namespace Velvetech.Domain.Common
{
	public abstract class Entity<TId> : BaseEntity, IEntity<TId>
	{
		public TId Id { get; set; }
	}
}
