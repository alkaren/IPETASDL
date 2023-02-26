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
    public class TransactionLogsController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public TransactionLogsController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: TransactionLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionLog>>> GetTransactionLog()
        {
            return await _context.TransactionLog.ToListAsync();
        }

        // GET: TransactionLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionLog>> GetTransactionLog(int id)
        {
            var transactionLog = await _context.TransactionLog.FindAsync(id);

            if (transactionLog == null)
            {
                return NotFound();
            }

            return transactionLog;
        }

        // GET: TransactionLogs/5
        [HttpGet("cusid/{id}")]
        public async Task<ActionResult<TransactionLog>> GetTransactionLogByCusId(int id)
        {
            var transactionLog = from x in _context.TransactionLog join
                                 y in _context.HeaderTransaction on 
                                 x.IdTransaction equals y.IdTransaction
                                 where y.IdCustomer == id
                                 select x;

            var result = await transactionLog.ToListAsync(); 

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }


        // PUT: TransactionLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionLog(int id, [FromBody]TransactionLog transactionLog)
        {
            if (id != transactionLog.IdTransactioLog)
            {
                return BadRequest();
            }

            _context.Entry(transactionLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionLogExists(id))
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

        // POST: TransactionLogs
        [HttpPost]
        public async Task<ActionResult<TransactionLog>> PostTransactionLog([FromBody]TransactionLog transactionLog)
        {
            _context.TransactionLog.Add(transactionLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactionLog", new { id = transactionLog.IdTransactioLog }, transactionLog);
        }

        // DELETE: TransactionLogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionLog>> DeleteTransactionLog(int id)
        {
            var transactionLog = await _context.TransactionLog.FindAsync(id);
            if (transactionLog == null)
            {
                return NotFound();
            }

            _context.TransactionLog.Remove(transactionLog);
            await _context.SaveChangesAsync();

            return transactionLog;
        }

        private bool TransactionLogExists(int id)
        {
            return _context.TransactionLog.Any(e => e.IdTransactioLog == id);
        }
    }
}
