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
    public class ParticipantesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParticipantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipante()
        {
            return await _context.Participante.ToListAsync();
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(string id)
        {
            var participante = await _context.Participante.FindAsync(id);

            if (participante == null)
            {
                return NotFound();
            }

            return participante;
        }

        // PUT: api/Participantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipante(string id, Participante participante)
        {
            if (id != participante.Cedula)
            {
                return BadRequest();
            }

            _context.Entry(participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(id))
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

        // POST: api/Participantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participante>> PostParticipante(Participante participante)
        {
            _context.Participante.Add(participante);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParticipanteExists(participante.Cedula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetParticipante", new { id = participante.Cedula }, participante);
        }

        // DELETE: api/Participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(string id)
        {
            var participante = await _context.Participante.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }

            _context.Participante.Remove(participante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipanteExists(string id)
        {
            return _context.Participante.Any(e => e.Cedula == id);
        }
    }
}
