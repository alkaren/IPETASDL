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
    public class UserGroupsController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public UserGroupsController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: UserGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGroup>>> GetUserGroup()
        {
            return await _context.UserGroup.ToListAsync();
        }

        // GET: UserGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGroup>> GetUserGroup(int id)
        {
            var userGroup = await _context.UserGroup.FindAsync(id);

            if (userGroup == null)
            {
                return NotFound();
            }

            return userGroup;
        }

        // PUT: UserGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserGroup(int id, [FromBody]UserGroup userGroup)
        {
            if (id != userGroup.IdGroup)
            {
                return BadRequest();
            }

            _context.Entry(userGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGroupExists(id))
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

        // POST: UserGroups
        [HttpPost]
        public async Task<ActionResult<UserGroup>> PostUserGroup([FromBody]UserGroup userGroup)
        {
            _context.UserGroup.Add(userGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserGroup", new { id = userGroup.IdGroup }, userGroup);
        }

        // DELETE: UserGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserGroup>> DeleteUserGroup(int id)
        {
            var userGroup = await _context.UserGroup.FindAsync(id);
            if (userGroup == null)
            {
                return NotFound();
            }

            _context.UserGroup.Remove(userGroup);
            await _context.SaveChangesAsync();

            return userGroup;
        }

        private bool UserGroupExists(int id)
        {
            return _context.UserGroup.Any(e => e.IdGroup == id);
        }
    }
}
