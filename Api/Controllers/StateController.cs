using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.External.Interfaces;
using Velvetech.Domain.Services.Internal.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Shared;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;

namespace Velvetech.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StateController : ControllerBase
	{
		public static bool State = false;

	// GET: api/Test/State/IsActive
		[HttpGet]
		public async Task<ActionResult<bool>> IsActiveAsync()
		{
			return await Task.FromResult(State);
		}
	}
}
