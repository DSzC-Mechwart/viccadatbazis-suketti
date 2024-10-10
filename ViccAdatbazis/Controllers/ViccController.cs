using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViccController : ControllerBase
    {
        private readonly ViccDbContext _context;
        public ViccController(ViccDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Vicc>>> GetViccek()
        {
            return await _context.Viccek.Where(x => x.Aktiv).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Vicc>> GetVicc([FromBody] int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            return vicc == null ? NotFound() : Ok(vicc);
        }
        [HttpPost]
        public async Task<ActionResult> PostVicc(Vicc ujVicc)
        {
            _context.Viccek.Add(ujVicc);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutVicc(int id, Vicc modositottVicc)
        {
            if (id != modositottVicc.Id)
            {
                return BadRequest();
            }
            _context.Entry(modositottVicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVicc(int id)
        {
            var torlendo =  _context.Viccek.Find(id);
            if (torlendo == null) 
                return NotFound();
            if (torlendo.Aktiv)
            {
                torlendo.Aktiv = false;
                _context.Entry(torlendo).State = EntityState.Modified;  
            }else
            {
                _context.Viccek.Remove(torlendo);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
