﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

using Velvetech.Server.Models;
using Velvetech.Shared;
using Velvetech.Shared.Wrappers;

namespace Velvetech.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GroupsController : ControllerBase
	{
		private readonly VelvetechContext _context;

		public GroupsController(VelvetechContext context)
		{
			_context = context;
		}

		// GET: api/Groups
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GroupWrapper>>> GetGroup(string name = null)
		{
			if (name is null)
				return await (
					from gr in _context.Group
					select new GroupWrapper
					{
						Id = gr.Id,
						Name = gr.Name,
						Count = gr.Grouping.Count()
					})
					.ToListAsync();
			else
				return await (
					from gr in _context.Group
					where gr.Name.Contains(name)
					select new GroupWrapper
					{
						Id = gr.Id,
						Name = gr.Name,
						Count = gr.Grouping.Count()
					})
					.ToListAsync();
		}

		// GET: api/Groups/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Group>> GetGroup(Guid id)
		{
			var @group = await _context.Group.FindAsync(id);

			if (@group == null)
			{
				return NotFound();
			}

			return @group;
		}

		// PUT: api/Groups/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutGroup(Guid id, Group @group)
		{
			if (id != @group.Id)
			{
				return BadRequest();
			}

			_context.Entry(@group).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GroupExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Groups
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Group>> PostGroup(Group @group)
		{
			_context.Group.Add(@group);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetGroup", new { id = @group.Id }, @group);
		}

		// DELETE: api/Groups/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Group>> DeleteGroup(Guid id)
		{
			var @group = await _context.Group.FindAsync(id);
			if (@group == null)
			{
				return NotFound();
			}

			_context.Group.Remove(@group);
			await _context.SaveChangesAsync();

			return @group;
		}

		private bool GroupExists(Guid id)
		{
			return _context.Group.Any(e => e.Id == id);
		}
	}
}
