using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LALAPATAPA.services.Models;
using LALAPATAPA.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace LALAPATAPA.services.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly lalapatapadbContext _context;
        private readonly IAccountService _accountService;

        public AccountsController(lalapatapadbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        // this for login
        // POST: Accounts
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]Account userParam)
        {
            var user = await _accountService.Authenticate(userParam.Username, userParam.Password);

            // return null if not found
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // this for forgot password
        [AllowAnonymous]
        [HttpGet("isexist/{email}")]
        public async Task<ActionResult<Account>> IsAccountExist(string email)
        {
            var account = await _context.Account.SingleAsync(x => x.Username == email);
            account.Password = null;

            if (account != null)
            {
                return account;
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpPut("forgotpass/{id}")]
        public async Task<IActionResult> ForgotPassword(int id, [FromBody]Account account)
        {
            try
            {
                if (id != account.IdAccount)
                {
                    return BadRequest();
                }

                var newPassword = await _accountService.RecoveryPassword(account.Username);

                if (newPassword != null)
                {
                    // save new password      

                    var newaccount = new Account
                    {
                        IdAccount = account.IdAccount,
                        CreatedBy = account.CreatedBy,
                        CreatedDate = account.CreatedDate,
                        IdCustomer = account.IdCustomer,
                        IdEmployee = account.IdEmployee,
                        IdGroup = account.IdGroup,
                        Token = account.Token,
                        Username = account.Username,                        
                        Password = newPassword,
                        UpdatedBy = "system forgot password",
                        UpdatedDate = DateTime.Now,
                    };

                    _context.Entry(account).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AccountExists(id))
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
                return BadRequest();
            }
            catch (Exception ex)
            {
                // this is exception
                return BadRequest();
            }
        }

        // GET: Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccount()
        {
            return await _context.Account.ToListAsync();
        }

        // GET: Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody]Account account)
        {
            if (id != account.IdAccount)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: Accounts
        [AllowAnonymous]
        [HttpPost("cus")]
        public async Task<ActionResult<Account>> PostCustomerAccount([FromBody]Account account)
        {
            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.IdAccount }, account);
        }
        
        // POST: Accounts
        [HttpPost("emp")]
        public async Task<ActionResult<Account>> PostEmployeeAccount([FromBody]Account account)
        {
            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.IdAccount }, account);
        }

        // DELETE: Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.IdAccount == id);
        }
    }
}
