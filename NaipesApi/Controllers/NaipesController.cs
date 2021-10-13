using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaipesApi.Data;
using NaipesApi.Models;

namespace NaipesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NaipesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NaipesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Naipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Naipes>>> GetNaipes()
        {
            return await _context.Naipes.ToListAsync();
        }

        // GET: api/Naipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Naipes>> GetNaipes(string id)
        {
            var naipes = await _context.Naipes.FindAsync(id);

            if (naipes == null)
            {
                return NotFound();
            }

            return naipes;
        }

        // PUT: api/Naipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNaipes(string id, Naipes naipes)
        {
            if (id != naipes.NaipeID)
            {
                return BadRequest();
            }

            _context.Entry(naipes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NaipesExists(id))
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

        // POST: api/Naipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Naipes>> PostNaipes(Naipes naipes)
        {
            _context.Naipes.Add(naipes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NaipesExists(naipes.NaipeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNaipes", new { id = naipes.NaipeID }, naipes);
        }

        // DELETE: api/Naipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNaipes(string id)
        {
            var naipes = await _context.Naipes.FindAsync(id);
            if (naipes == null)
            {
                return NotFound();
            }

            _context.Naipes.Remove(naipes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NaipesExists(string id)
        {
            return _context.Naipes.Any(e => e.NaipeID == id);
        }
    }
}
