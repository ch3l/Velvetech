using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Shared.Dtos;

namespace Velvetech.Api.Controllers
{
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
		[HttpGet]
		public async Task<ActionResult<SexDto[]>> ListAsync() =>
			await _sexList.ListAsync()
				.Select(DtoExtensions.ToDto)
				.ToArrayAsync();
	}
}