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
    public class DetailTransactionsController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public DetailTransactionsController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: DetailTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailTransaction>>> GetDetailTransaction()
        {
            return await _context.DetailTransaction.ToListAsync();
        }

        // GET: DetailTransactions/5
        [HttpGet("transid/{idtrans}")]
        public async Task<ActionResult<HeaderTransaction>> GetByIdTransDetailTransaction(int idtrans)
        {
            var datas = await _context.DetailTransaction.SingleAsync(x => x.IdTransaction == idtrans);

            return Ok(datas);
        }

        // GET: DetailTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailTransaction>> GetDetailTransaction(int id)
        {
            var detailTransaction = await _context.DetailTransaction.FindAsync(id);

            if (detailTransaction == null)
            {
                return NotFound();
            }

            return detailTransaction;
        }

        // PUT: DetailTransactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetailTransaction(int id, [FromBody]DetailTransaction detailTransaction)
        {
            if (id != detailTransaction.IdDetail)
            {
                return BadRequest();
            }

            _context.Entry(detailTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailTransactionExists(id))
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

        // POST: DetailTransactions
        [HttpPost]
        public async Task<ActionResult<DetailTransaction>> PostDetailTransaction([FromBody]DetailTransaction detailTransaction)
        {
            _context.DetailTransaction.Add(detailTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetailTransaction", new { id = detailTransaction.IdDetail }, detailTransaction);
        }

        // DELETE: DetailTransactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailTransaction>> DeleteDetailTransaction(int id)
        {
            var detailTransaction = await _context.DetailTransaction.FindAsync(id);
            if (detailTransaction == null)
            {
                return NotFound();
            }

            _context.DetailTransaction.Remove(detailTransaction);
            await _context.SaveChangesAsync();

            return detailTransaction;
        }

        private bool DetailTransactionExists(int id)
        {
            return _context.DetailTransaction.Any(e => e.IdDetail == id);
        }
    }
}
