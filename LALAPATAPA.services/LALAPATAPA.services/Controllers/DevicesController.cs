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
    public class DevicesController : ControllerBase
    {
        private readonly lalapatapadbContext _context;

        public DevicesController(lalapatapadbContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevice()
        {
            return await _context.Device.ToListAsync();
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Device.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, Device device)
        {
            if (id != device.IdDevice)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        [HttpGet("getallbyaccount/{id}")]
        public async Task<ActionResult<List<Device>>> GetdeviceByAccountId(int id)
        {
            var device = await (from x in _context.Device
                          where x.IdAccount == id
                          select x).ToListAsync();

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // POST: api/Devices
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice([FromBody]Device device)
        {
            _context.Device.Add(device);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeviceExists(device.IdDevice))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDevice", new { id = device.IdDevice }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{installationid}")]
        public async Task<ActionResult<Device>> DeleteDevice(string installationid)
        {
            var device = (from x in _context.Device where
                          x.InstallationId == installationid
                          select x).FirstOrDefault();

            if (device == null)
            {
                return NotFound();
            }

            _context.Device.Remove(device);
            await _context.SaveChangesAsync();

            return device;
        }

        private bool DeviceExists(int id)
        {
            return _context.Device.Any(e => e.IdDevice == id);
        }
    }
}
