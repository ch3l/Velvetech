using Velvetech.Web.HttpClients.Results.Base;

namespace Velvetech.Web.HttpClients.Results
{
	public class SuccessfullEntityAction<TEntity> : ClientActionResult
	{
		public TEntity Entity { get; }

		public SuccessfullEntityAction(TEntity entity)
		{
			Entity = entity;
		}
	}
}