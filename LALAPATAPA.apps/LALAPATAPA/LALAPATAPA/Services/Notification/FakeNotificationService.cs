using LALAPATAPA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LALAPATAPA.Services.Notification
{
    public class FakeNotificationService : INotificationService
    {
        public Task<IEnumerable<Models.Notification>> GetNotificationsAsync(int seq, string token)
        {
            var data = new List<Models.Notification>
            {
                new Models.Notification { Text = "Ada pembayaran masuk.", Type = NotificationType.PembayaranMasuk },
                new Models.Notification { Text = "Ada konfirmasi pembayaran.", Type = NotificationType.PembayaranKonfirm },
                new Models.Notification { Text = "Ada order baru.", Type = NotificationType.OrderMasuk },
                new Models.Notification { Text = "Notifikasi ga jelas.", Type = NotificationType.Other }
            };

            return Task.FromResult(data.AsEnumerable());
        }

        public Task DeleteNotificationAsync(Models.Notification notification)
        {
            return Task.FromResult(false);
        }
    }
}