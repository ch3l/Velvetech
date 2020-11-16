using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Velvetech.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StateController : ControllerBase
	{
		public static bool State = false;

		// GET: api/Test/State/IsReady
		[HttpGet]
		[SwaggerOperation(
			Summary = "Checks MSSQL Server active state",
			Description = "Checks MSSQL Server active state",
			OperationId = "AuthorizationController.AuthorizeAsync",
			Tags = new[]
			{
				"Controller: State"
			})]
		public async Task<ActionResult<bool>> IsReadyAsync() => await Task.FromResult(State);
	}
}
