using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Velvetech.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StateController : ControllerBase
	{
		public static bool State = false;

		// GET: api/Test/State/IsReady
		[HttpGet]
		public async Task<ActionResult<bool>> IsReadyAsync()
		{
			return await Task.FromResult(State);
		}
	}
}
