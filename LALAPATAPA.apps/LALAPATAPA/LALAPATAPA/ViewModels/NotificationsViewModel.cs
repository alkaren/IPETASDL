using System.Threading.Tasks;
using LALAPATAPA.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;
using LALAPATAPA.Services.Notification;
using LALAPATAPA.Models;
using LALAPATAPA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Xaml;
using LALAPATAPA.ViewModels;
using LALAPATAPA.Extensions;
using LALAPATAPA.Services.Request;

namespace LALAPATAPA.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        readonly INotificationService notificationService;
        readonly IRequestService requestService;

        ObservableRangeCollection<Notification> notifications;
        bool hasItems;

        public NotificationsViewModel(
            INotificationService notificationService, IRequestService requestService
           )
        {
            this.notificationService = notificationService;
            this.requestService = requestService;

            HasItems = true;
        }

        public ObservableRangeCollection<Models.Notification> Notifications
        {
            get => notifications;
            set => SetProperty(ref notifications, value);
        }

        public bool HasItems
        {
            get => hasItems;
            set => SetProperty(ref hasItems, value);
        }

        public ICommand DeleteNotificationCommand => new Command<Models.Notification>(OnDelete);

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                Notifications = (ObservableRangeCollection<Models.Notification>)navigationData;
                HasItems = Notifications.Count > 0;
            }

            return base.InitializeAsync(navigationData);
        }

        async void OnDelete(Models.Notification notification)
        {
            if (notification != null)
            {
                Notifications.Remove(notification);
                await notificationService.DeleteNotificationAsync(notification);
               
                HasItems = Notifications.Count > 0;
            }
        }

        private async Task<bool> PushNotification(Notification notification, string installationId)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/lalapatapanotif/{installationId}");

                var uri = builder.ToString();

                var res = await requestService.PostAsync<Notification>(uri, notification, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_NotificationViewModel_PushNotification");
                return false;
            }
            finally
            {

            }
        }

        public async Task<bool> PushNotif(string message, string installationId)
        {
            var notif = new Notification
            {
                Text = message,
                Time = DatetimeHelper.GetDatetimeNow()
            };

            if (await PushNotification(notif, installationId))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> PushToYanjas(Notification notification)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/lalapatapanotif/sendtoyanjas");

                var uri = builder.ToString();

                var res = await requestService.PostAsync<Notification>(uri, notification, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_NotificationViewModel_PushToYanjas");
                return false;
            }
            finally
            {

            }
        }
        public async Task<bool> PushToLia(Notification notification)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/lalapatapanotif/sendtolia");

                var uri = builder.ToString();

                var res = await requestService.PostAsync<Notification>(uri, notification, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_NotificationViewModel_PushToLia");
                return false;
            }
            finally
            {

            }
        }
        public async Task<bool> PushToBudi(Notification notification)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/lalapatapanotif/sendtobudi");

                var uri = builder.ToString();

                var res = await requestService.PostAsync<Notification>(uri, notification, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_NotificationViewModel_PushToBudi");
                return false;
            }
            finally
            {

            }
        }
        public async Task<bool> PushToCus(Notification notification, int id)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/lalapatapanotif/sendtocus/{id}");

                var uri = builder.ToString();

                var res = await requestService.PostAsync<Notification>(uri, notification, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_NotificationViewModel_PushToCus");
                return false;
            }
            finally
            {

            }
        }
    }
}