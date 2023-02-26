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
    public class PaymentAttachmentsController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public PaymentAttachmentsController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: PaymentAttachments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentAttachment>>> GetPaymentAttachment()
        {
            return await _context.PaymentAttachment.ToListAsync();
        }

        // GET: PaymentAttachments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentAttachment>> GetPaymentAttachment(int id)
        {
            var paymentAttachment = await _context.PaymentAttachment.FindAsync(id);

            if (paymentAttachment == null)
            {
                return NotFound();
            }

            return paymentAttachment;
        }

        // GET: PaymentAttachments/5
        [HttpGet("transid/{id}")]
        public async Task<ActionResult<PaymentAttachment>> GetPaymentAttachmentByTransId(int id)
        {
            var paymentAttachment = await (from x in _context.PaymentAttachment
                                           join y in _context.HeaderTransaction on 
                                           x.IdAttachment equals y.IdAttachment where
                                           y.IdTransaction == id select x).SingleAsync();

            if (paymentAttachment == null)
            {
                return NotFound();
            }

            return paymentAttachment;
        }
        // PUT: PaymentAttachments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentAttachment(int id, [FromBody]PaymentAttachment paymentAttachment)
        {
            if (id != paymentAttachment.IdAttachment)
            {
                return BadRequest();
            }

            _context.Entry(paymentAttachment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentAttachmentExists(id))
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

        // POST: PaymentAttachments
        [HttpPost]
        public async Task<ActionResult<PaymentAttachment>> PostPaymentAttachment([FromBody]PaymentAttachment paymentAttachment)
        {
            _context.PaymentAttachment.Add(paymentAttachment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentAttachment", new { id = paymentAttachment.IdAttachment }, paymentAttachment);
        }

        // DELETE: PaymentAttachments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentAttachment>> DeletePaymentAttachment(int id)
        {
            var paymentAttachment = await _context.PaymentAttachment.FindAsync(id);
            if (paymentAttachment == null)
            {
                return NotFound();
            }

            _context.PaymentAttachment.Remove(paymentAttachment);
            await _context.SaveChangesAsync();

            return paymentAttachment;
        }

        private bool PaymentAttachmentExists(int id)
        {
            return _context.PaymentAttachment.Any(e => e.IdAttachment == id);
        }
    }
}
