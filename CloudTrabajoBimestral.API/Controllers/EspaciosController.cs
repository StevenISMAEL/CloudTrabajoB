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
    public class EspaciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EspaciosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Espacios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Espacio>>> GetEspacio()
        {
            return await _context.Espacio.ToListAsync();
        }

        // GET: api/Espacios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Espacio>> GetEspacio(int id)
        {
            var espacio = await _context.Espacio.FindAsync(id);

            if (espacio == null)
            {
                return NotFound();
            }

            return espacio;
        }

        // PUT: api/Espacios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspacio(int id, Espacio espacio)
        {
            if (id != espacio.Id)
            {
                return BadRequest();
            }

            _context.Entry(espacio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspacioExists(id))
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

        // POST: api/Espacios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Espacio>> PostEspacio(Espacio espacio)
        {
            _context.Espacio.Add(espacio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEspacio", new { id = espacio.Id }, espacio);
        }

        // DELETE: api/Espacios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspacio(int id)
        {
            var espacio = await _context.Espacio.FindAsync(id);
            if (espacio == null)
            {
                return NotFound();
            }

            _context.Espacio.Remove(espacio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspacioExists(int id)
        {
            return _context.Espacio.Any(e => e.Id == id);
        }
    }
}
