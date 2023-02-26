using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LALAPATAPA.services.Models;
using Microsoft.AspNetCore.Authorization;

namespace LALAPATAPA.services.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public FeedbacksController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback()
        {
            return await _context.Feedback.ToListAsync();
        }

        // GET: Feedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await _context.Feedback.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // PUT: Feedbacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, [FromBody]Feedback feedback)
        {
            if (id != feedback.IdFeedback)
            {
                return BadRequest();
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: Feedbacks
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback([FromBody]Feedback feedback)
        {
            _context.Feedback.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = feedback.IdFeedback }, feedback);
        }

        // DELETE: Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Feedback>> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedback.Remove(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedback.Any(e => e.IdFeedback == id);
        }
    }
}
