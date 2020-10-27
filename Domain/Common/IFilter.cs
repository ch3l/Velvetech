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
		public abstract IEnumerator<Expression<Func<TEntity, bool>>> GetEnumerator();

		protected readonly TRequest Request;
		
		public Func<IQueryable<TEntity>, IQueryable<TEntity>> Filter =>
			(source) => source.Where(StackExpressions());

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();

		protected FilterBase(TRequest request)
		{
			Request = request;
		}

		public Expression<Func<TEntity, bool>> StackExpressions()
		{
			if (this.Any())
				return this.Aggregate((current, next) => current.And(next));

			return (source) => true;
		}
	}

	public abstract class Expressions<TEntity, TRequest> : IEnumerable<Expression<Func<TEntity, bool>>>
	{
		public abstract IEnumerator<Expression<Func<TEntity, bool>>> GetEnumerator();

		protected readonly TRequest Request;

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();

		protected Expressions(TRequest request)
		{
			Request = request;
		}

		public Expression<Func<TEntity, bool>> StackExpressions()
		{
			if (this.Any())
				return this.Aggregate((current, next) => current.And(next));

			return (source) => true;
		}  
	}	   
}
