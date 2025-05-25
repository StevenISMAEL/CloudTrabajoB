using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudTrabajoBimestral.Models;

namespace CloudTrabajoBimestral.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SesionesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sesiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sesion>>> GetSesion()
        {
            return await _context.Sesion.ToListAsync();
        }

        // GET: api/Sesiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sesion>> GetSesion(int id)
        {
            var sesion = await _context.Sesion.FindAsync(id);

            if (sesion == null)
            {
                return NotFound();
            }

            return sesion;
        }

        // PUT: api/Sesiones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSesion(int id, Sesion sesion)
        {
            if (id != sesion.Id)
            {
                return BadRequest();
            }

            _context.Entry(sesion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SesionExists(id))
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

        // POST: api/Sesiones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sesion>> PostSesion(Sesion sesion)
        {
            _context.Sesion.Add(sesion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSesion", new { id = sesion.Id }, sesion);
        }

        // DELETE: api/Sesiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSesion(int id)
        {
            var sesion = await _context.Sesion.FindAsync(id);
            if (sesion == null)
            {
                return NotFound();
            }

            _context.Sesion.Remove(sesion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SesionExists(int id)
        {
            return _context.Sesion.Any(e => e.Id == id);
        }
    }
}
