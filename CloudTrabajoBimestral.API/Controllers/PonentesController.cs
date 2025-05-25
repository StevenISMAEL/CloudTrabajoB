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
    public class PonentesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PonentesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ponentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ponente>>> GetPonente()
        {
            return await _context.Ponente.ToListAsync();
        }

        // GET: api/Ponentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ponente>> GetPonente(int id)
        {
            var ponente = await _context.Ponente.FindAsync(id);

            if (ponente == null)
            {
                return NotFound();
            }

            return ponente;
        }

        // PUT: api/Ponentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPonente(int id, Ponente ponente)
        {
            if (id != ponente.Id)
            {
                return BadRequest();
            }

            _context.Entry(ponente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PonenteExists(id))
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

        // POST: api/Ponentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ponente>> PostPonente(Ponente ponente)
        {
            _context.Ponente.Add(ponente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPonente", new { id = ponente.Id }, ponente);
        }

        // DELETE: api/Ponentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePonente(int id)
        {
            var ponente = await _context.Ponente.FindAsync(id);
            if (ponente == null)
            {
                return NotFound();
            }

            _context.Ponente.Remove(ponente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PonenteExists(int id)
        {
            return _context.Ponente.Any(e => e.Id == id);
        }
    }
}
