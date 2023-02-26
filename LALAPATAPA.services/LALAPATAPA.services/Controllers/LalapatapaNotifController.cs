using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LALAPATAPA.services.Models;
using Microsoft.Azure.NotificationHubs;
using LALAPATAPA.services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace LALAPATAPA.services.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LalapatapaNotifController : ControllerBase
    {
        private readonly lalapatapadbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _config;
        public LalapatapaNotifController(lalapatapadbContext context, INotificationService notificationService, IConfiguration IConfig)
        {
            _context = context;
            _notificationService = notificationService;
            _config = IConfig;
        }

        [HttpPost("sendtoyanjas")]
        public async Task<IActionResult> SendNotifToYanjas([FromBody]Models.Notification message)
        {
            var device = await (from x in _context.Device
                        where x.IdAccount == 1
                        select x).ToListAsync();

            var res = await _notificationService.SendToYanjas(message.Text, device);

            if (res)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPost("sendtolia")]
        public async Task<IActionResult> SendNotifToLia([FromBody]Models.Notification message)
        {
            var device = await (from x in _context.Device
                                where x.IdAccount == 2
                                select x).ToListAsync();

            var res = await _notificationService.SendToLia(message.Text, device);

            if (res)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPost("sendtobudi")]
        public async Task<IActionResult> SendNotifToBudi([FromBody]Models.Notification message)
        {
            var device = await (from x in _context.Device
                                where x.IdAccount == 3
                                select x).ToListAsync();

            var res = await _notificationService.SendToBudi(message.Text, device);

            if (res)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("sendtocus/{id}")]
        public async Task<IActionResult> SendNotifToCustomer(int id, [FromBody]Models.Notification message)
        {
            var device = await (from dev in _context.Device
                                join acc in _context.Account on
                                dev.IdAccount equals acc.IdAccount
                                join cus in _context.Customer on
                                acc.IdCustomer equals cus.IdCustomer
                                join head in _context.HeaderTransaction on
                                cus.IdCustomer equals head.IdCustomer
                                where head.IdTransaction == id
                                select dev).ToListAsync();

            var res = await _notificationService.SendToCus(message.Text, device);

            if (res)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> SendNotificationAsync([FromBody]Models.Notification message)
        {
            // Get the settings for the server project.
            //HttpConfiguration config = this.Configuration;
            try
            {
                await SendNotification(message.Text, null);
            }
            catch (Exception ex)
            {
                //// Write the failure result to the logs.
                //config.Services.GetTraceWriter().Error(ex.Message, null, "Push.SendAsync Error");
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("{installationid}")]
        public async Task<IActionResult> SendNotificationAsync(string installationId, [FromBody]Models.Notification message)
        {
            // Get the settings for the server project.
            //HttpConfiguration config = this.Configuration;
            try
            {
                await SendNotification(message.Text, installationId);
            }
            catch (Exception ex)
            {
                // Write the failure result to the logs.
                //config.Services.GetTraceWriter().Error(ex.Message, null, "Push.SendAsync Error");
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        async Task<NotificationOutcome> SendNotification(string message, string installationId)
        {
            // Get the settings for the server project.
            // The name of the Notification Hub from the overview page.
            string notificationHubName = _config.GetSection("NotifHub").GetSection("NotificationHubName").Value;
            // Use "DefaultFullSharedAccessSignature" from the portal's Access Policies.
            string notificationHubConnection = _config.GetSection("NotifHub").GetSection("NotificationHubConnectionString").Value;

            // Create a new Notification Hub client.
            var hub = NotificationHubClient.CreateClientFromConnectionString(
                notificationHubConnection,
                notificationHubName,
                // Don't use this in RELEASE builds. The number of devices is limited.
                // If TRUE, the send method will return the devices a message was
                // delivered to.
                enableTestSend: true);

            // Sending the message so that all template registrations that contain "messageParam"
            // will receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            var templateParams = new Dictionary<string, string>
            {
                ["messageParam"] = message
            };

            // Send the push notification and log the results.

            NotificationOutcome result = null;
            if (string.IsNullOrWhiteSpace(installationId))
            {
                result = await hub.SendTemplateNotificationAsync(templateParams).ConfigureAwait(false);
            }
            else
            {
                result = await hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + installationId + "}").ConfigureAwait(false);
            }


            // Write the success result to the logs.
            //config.Services.GetTraceWriter().Info(result.State.ToString());
            return result;
        }
    }
}
