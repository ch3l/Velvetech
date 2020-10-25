using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using LinqKit;

namespace Velvetech.Domain.Common
{
	public interface IFilter<T>
	{
		Func<IQueryable<T>, IQueryable<T>> Filter { get; }
	}

	public abstract class FilterBase<TEntity, TRequest> : IFilter<TEntity>, IEnumerable<Expression<Func<TEntity, bool>>>
	{
		public Func<IQueryable<TEntity>, IQueryable<TEntity>> Filter =>
			(source) => source.Where(StackExpressions());

		protected readonly TRequest Request;

		protected FilterBase(TRequest request)
		{
			Request = request;
		}

		public abstract IEnumerator<Expression<Func<TEntity, bool>>> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();

		public Expression<Func<TEntity, bool>> StackExpressions()
		{
			if (this.Any())
				return this.Aggregate((current, next) => current.And(next));

			return (source) => true;
		}
	}
}
