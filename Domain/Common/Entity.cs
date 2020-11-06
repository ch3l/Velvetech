using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Velvetech.Domain.Common
{
	public abstract class Entity<TKey> : BaseEntity
	{
		private TKey _id;

		public TKey Id
		{
			get => _id.Equals(default(TKey)) 
				? throw new Exception($"Not acceptable default value \"{_id}\" of property " +
				                      $"\"{nameof(Id)}\" in Entity<{typeof(TKey).Name}> " +
				                      $"\"{GetType().Name}\"") 
				: _id;
			private set => _id = value;
		}

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
