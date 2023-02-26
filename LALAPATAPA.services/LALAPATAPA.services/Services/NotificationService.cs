using LALAPATAPA.services.Helpers;
using LALAPATAPA.services.Models;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LALAPATAPA.services.Services
{
    public interface INotificationService
    {
        Task<bool> SendToYanjas(string message, List<Device> device);
        Task<bool> SendToLia(string message, List<Device> device);
        Task<bool> SendToBudi(string message, List<Device> device);
        Task<bool> SendToCus(string message, List<Device> device);
    }
    public class NotificationService : INotificationService
    {
        private readonly lalapatapadbContext _context;
        private readonly AppSetting _appSetting;
        private readonly IConfiguration _config;

        public NotificationService(lalapatapadbContext context, IOptions<AppSetting> appSetting, IConfiguration iConfig)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _config = iConfig;
        }

        public async Task<bool> SendToYanjas(string message, List<Device> device)
        {
            try
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

                foreach (var notif in device)
                {
                    NotificationOutcome result = null;
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        //result = await hub.SendTemplateNotificationAsync(templateParams).ConfigureAwait(false);
                    }
                    else
                    {
                        result = await hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + notif.InstallationId + "}").ConfigureAwait(false);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SendToLia(string message, List<Device> device)
        {
            try
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

                foreach (var notif in device)
                {
                    NotificationOutcome result = null;
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        //result = await hub.SendTemplateNotificationAsync(templateParams).ConfigureAwait(false);
                    }
                    else
                    {
                        result = await hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + notif.InstallationId + "}").ConfigureAwait(false);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SendToBudi(string message, List<Device> device)
        {
            try
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

                foreach (var notif in device)
                {
                    NotificationOutcome result = null;
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        //result = await hub.SendTemplateNotificationAsync(templateParams).ConfigureAwait(false);
                    }
                    else
                    {
                        result = await hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + notif.InstallationId + "}").ConfigureAwait(false);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SendToCus(string message, List<Device> device)
        {
            try
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

                foreach (var notif in device)
                {
                    NotificationOutcome result = null;
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        //result = await hub.SendTemplateNotificationAsync(templateParams).ConfigureAwait(false);
                    }
                    else
                    {
                        result = await hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + notif.InstallationId + "}").ConfigureAwait(false);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
