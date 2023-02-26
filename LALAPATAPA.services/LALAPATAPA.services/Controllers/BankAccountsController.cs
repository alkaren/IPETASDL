using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LALAPATAPA.services.Models;

namespace LALAPATAPA.services.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public BankAccountsController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: BankAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccount()
        {
            return await _context.BankAccount.ToListAsync();
        }

        // GET: BankAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(int id)
        {
            var bankAccount = await _context.BankAccount.FindAsync(id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return bankAccount;
        }

        // PUT: BankAccounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankAccount(int id, [FromBody]BankAccount bankAccount)
        {
            if (id != bankAccount.IdBankAccount)
            {
                return BadRequest();
            }

            _context.Entry(bankAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankAccountExists(id))
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

        // POST: BankAccounts
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount([FromBody]BankAccount bankAccount)
        {
            _context.BankAccount.Add(bankAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankAccount", new { id = bankAccount.IdBankAccount }, bankAccount);
        }

        // DELETE: BankAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccount>> DeleteBankAccount(int id)
        {
            var bankAccount = await _context.BankAccount.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            _context.BankAccount.Remove(bankAccount);
            await _context.SaveChangesAsync();

            return bankAccount;
        }

        private bool BankAccountExists(int id)
        {
            return _context.BankAccount.Any(e => e.IdBankAccount == id);
        }
    }
}
