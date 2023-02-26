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
    public class HeaderTransactionsController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public HeaderTransactionsController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: HeaderTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeaderTransaction>>> GetHeaderTransaction()
        {
            return await _context.HeaderTransaction.ToListAsync();
        }

        // GET: HeaderTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeaderTransaction>> GetHeaderTransaction(int id)
        {
            var headerTransaction = await _context.HeaderTransaction.FindAsync(id);

            if (headerTransaction == null)
            {
                return NotFound();
            }

            return headerTransaction;
        }

        // GET: HeaderTransactions/5
        [HttpGet("idcus/{idcus}")]
        public async Task<ActionResult<HeaderTransaction>> GetByIdCusTransaction(int idcus)
        {
            var datas = from x in _context.HeaderTransaction
                        where x.IdCustomer == idcus select x;

            var hasil = await datas.ToListAsync();

            return Ok(hasil);
        }

        // GET: HeaderTransactions/5
        [HttpGet("{idcus}/isstatus")]
        public async Task<ActionResult<HeaderTransaction>> GetByStatusTransaction(int idcus, string status)
        {
            var datas = from x in _context.HeaderTransaction
                        where x.IdCustomer == idcus && x.TransactionStatus == status
                        select x;
            var hasil = await datas.ToListAsync();

            return Ok(hasil);
        }

        // PUT: HeaderTransactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeaderTransaction(int id, [FromBody]HeaderTransaction headerTransaction)
        {
            if (id != headerTransaction.IdTransaction)
            {
                return BadRequest();
            }

            _context.Entry(headerTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeaderTransactionExists(id))
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

        // POST: HeaderTransactions
        [HttpPost]
        public async Task<ActionResult<HeaderTransaction>> PostHeaderTransaction([FromBody]HeaderTransaction headerTransaction)
        {
            _context.HeaderTransaction.Add(headerTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHeaderTransaction", new { id = headerTransaction.IdTransaction }, headerTransaction);
        }

        // DELETE: HeaderTransactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HeaderTransaction>> DeleteHeaderTransaction(int id)
        {
            var headerTransaction = await _context.HeaderTransaction.FindAsync(id);
            if (headerTransaction == null)
            {
                return NotFound();
            }

            _context.HeaderTransaction.Remove(headerTransaction);
            await _context.SaveChangesAsync();

            return headerTransaction;
        }

        private bool HeaderTransactionExists(int id)
        {
            return _context.HeaderTransaction.Any(e => e.IdTransaction == id);
        }

        // GET: HeaderTransaction/5
        [HttpGet("getdevicebytrans/{id}")]
        public async Task<ActionResult<List<Device>>> GetdeviceByTransId(int id)
        {
            var device = await (from x in _context.Device
                                           join y in _context.Account on
                                           x.IdAccount equals y.IdAccount
                                           join z in _context.Customer on
                                           y.IdCustomer equals z.IdCustomer
                                           join v in _context.HeaderTransaction on
                                           z.IdCustomer equals v.IdCustomer
                                           where v.IdTransaction == id
                                           select x).ToListAsync();

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }
    }
}
