using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using LALAPATAPA.services.Models;
using LALAPATAPA.services.Helpers;

namespace LALAPATAPA.services.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public LogsController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: Logs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLog()
        {
            return await _context.Log.ToListAsync();
        }

        // GET: Logs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLog(int id)
        {
            var log = await _context.Log.FindAsync(id);

            if (log == null)
            {
                return NotFound();
            }

            return log;
        }

        // PUT: Logs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLog(int id, [FromBody]Log log)
        {
            if (id != log.IdLog)
            {
                return BadRequest();
            }

            _context.Entry(log).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id))
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

        // POST: Logs
        [HttpPost]
        public async Task<ActionResult<Log>> PostLog([FromBody]Log log)
        {
            _context.Log.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLog", new { id = log.IdLog }, log);
        }

        // DELETE: Logs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Log>> DeleteLog(int id)
        {
            var log = await _context.Log.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            _context.Log.Remove(log);
            await _context.SaveChangesAsync();

            return log;
        }

        private bool LogExists(int id)
        {
            return _context.Log.Any(e => e.IdLog == id);
        }
    }
}
