using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Base.Interfaces;
using Velvetech.Shared.Dtos;

namespace Velvetech.Api.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SexController : ControllerBase
	{
		private readonly IReadService<Sex, int> _sexList;

		public SexController(IReadService<Sex, int> sexList)
		{
			_sexList = sexList;
		}

		// GET: api/Sex/List						
		[Authorize(Roles = Shared.Authentication.Constants.Roles.SexRead)]
		[HttpGet]
		[SwaggerOperation(
			Summary = "Returns Groups list",
			Description = @"Returns Groups list, support search by 'Group' query tag",
			OperationId = "GroupController.ListAsync",
			Tags = new[]
			{
				"Controller: Sex", 
				//"Action: List"
			})]
		public async Task<ActionResult<SexDto[]>> ListAsync() =>
			await _sexList.ListAsync()
				.Select(DtoExtensions.ToDto)
				.ToArrayAsync();
	}
}