using Velvetech.Web.HttpClients.Results.Base;

namespace Velvetech.Web.HttpClients.Results
{
	public class SuccessfulEntityAction<TEntity> : ApiActionResult
	{
		public TEntity Entity { get; }

		public SuccessfulEntityAction(TEntity entity)
		{
			Entity = entity;
		}
	}
}