using System;
using System.Collections.Generic;
using System.Text;

namespace Velvetech.Domain.Common
{
	public abstract class Entity<TKey> : BaseEntity
		//where TKey : IEquatable<TKey>
	{
		protected Entity()
		{
		}

		public TKey Id { get; private set; }
		 
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is Entity<TKey> entity
			       && Id.Equals(entity.Id);
		}
	}
}
