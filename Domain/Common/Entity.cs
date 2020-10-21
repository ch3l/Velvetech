using System;
using System.Collections.Generic;
using System.Text;

namespace Velvetech.Domain.Common
{
	public abstract class Entity<TId> : BaseEntity, IEntity<TId>
	{
		protected Entity()
		{
		}

		protected Entity(TId id)
		{
			Id = id;
		}		

		public TId Id { get; set; }
	}
}
