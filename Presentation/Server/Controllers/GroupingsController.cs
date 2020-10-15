using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Velvetech.Presentation.Server.Models;
using Velvetech.Presentation.Shared;

namespace Velvetech.Presentation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupingsController : ControllerBase
    {
        private readonly VelvetechContext _context;

        public GroupingsController(VelvetechContext context)
        {
            _context = context;
        }

        // GET: api/Groupings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grouping>>> GetGrouping()
        {
            return await _context.Grouping.ToListAsync();
        }

        // GET: api/Groupings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grouping>> GetGrouping(Guid id)
        {
            var grouping = await _context.Grouping.FindAsync(id);

            if (grouping == null)
            {
                return NotFound();
            }

            return grouping;
        }

        // PUT: api/Groupings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrouping(Guid id, Grouping grouping)
        {
            if (id != grouping.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(grouping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupingExists(id))
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

        // POST: api/Groupings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Grouping>> PostGrouping(Grouping grouping)
        {
            _context.Grouping.Add(grouping);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GroupingExists(grouping.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGrouping", new { id = grouping.StudentId }, grouping);
        }

        // DELETE: api/Groupings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Grouping>> DeleteGrouping(Guid id)
        {
            var grouping = await _context.Grouping.FindAsync(id);
            if (grouping == null)
            {
                return NotFound();
            }

            _context.Grouping.Remove(grouping);
            await _context.SaveChangesAsync();

            return grouping;
        }

        private bool GroupingExists(Guid id)
        {
            return _context.Grouping.Any(e => e.StudentId == id);
        }
    }
}
