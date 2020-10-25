using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Velvetech.Domain.Common
{
	public interface IFilter<T>
	{
		Func<IQueryable<T>, IQueryable<T>> Filter { get; }
	}

	public class FilterBase<T> : IFilter<T>
	{
		public Func<IQueryable<T>, IQueryable<T>> Filter { get; private set; }

		public FilterBase(Func<IQueryable<T>, IQueryable<T>> filter)
		{
			Filter = filter;	
		}
	}
}
