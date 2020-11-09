namespace Velvetech.Web.Services.Results
{
	public class SuccessfulEntityAction<TEntity> : EntityActionResult
	{
		public TEntity Entity { get; }

		public SuccessfulEntityAction(TEntity entity)
		{
			Entity = entity;
		}
	}
}