using System;
using System.Collections.Generic;
using System.Linq;

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Domain.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Velvetech.Presentation.Server;
using Presentation.Shared.Dtos;
using Presentation.Shared.Requests;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Domain.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Shared;

namespace Velvetech.Presentation.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GroupsController : ControllerBase
	{
		private readonly ICrudService<Group, Guid> _groupCrudService;

		public GroupsController(ICrudService<Group, Guid> groupCrudService)
		{
			_groupCrudService = groupCrudService;
		}

		// GET: api/Test/Groups
		[HttpGet]
		public async Task<ActionResult<GroupDto[]>> AllAsync()
		{
			return (await _groupCrudService.GetAllAsync())
				.Select(Extensions.ToDto)
				.ToArray();
		}		   		
	}
}
