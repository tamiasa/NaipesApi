using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaipesApi.Data;
using NaipesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaipesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarajasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BarajasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Naipes>> GetNaipes()
        {

            var list = await _context.Naipes.ToListAsync();

            var max = list.Count;
            int index = new Random().Next(0, max);

            var naipes = list[index];

            if (naipes == null)
            {
                return NoContent();
            }

            return naipes;
        }
    }
}
