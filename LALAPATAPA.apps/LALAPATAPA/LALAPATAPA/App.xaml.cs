using LALAPATAPA.ViewModels.Base;
using LALAPATAPA.Views;
using LALAPATAPA.Services.Notification;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using LALAPATAPA.Helpers;
using Plugin.LocalNotifications;

namespace LALAPATAPA
{
    public partial class App : Application
    {
        static readonly int NOTIFICATION_ID = 1000;
        public const string NotificationReceivedKey = "NotificationReceived";
        public const string MobileServiceUrl = "https://lalapatapaservice.azurewebsites.net";
        static App()
        {
            BuildDependencies();
        }
        public static void BuildDependencies()
        {
            /// Do you want to use fake services that DO NOT require real backend or internet connection?
            /// Set to true the value of <see cref="AppSettings.DefaultUseFakes"/> to use fake services, false if you want to use Azure Services.

            Locator.Instance.Build();
        }

        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (Config.AccessToken is null || Config.AccessToken is "")
            {
                MainPage = new LoginPage();
            }
            else
            {
                if (Config.CurrentUserGroup == 5)
                {
                    App.Current.MainPage = new NavigationPage(new Views.User.MainPage());
                    //Application.Current.MainPage = new NavigationPage(new Views.User.MainPage());
                }
                else
                {
                    App.Current.MainPage = new NavigationPage(new Views.Admin.MenuPage());
                    //Application.Current.MainPage = new NavigationPage(new Views.Admin.MenuPage());
                }
            }

            MessagingCenter.Subscribe<object, string>(this, App.NotificationReceivedKey, OnMessageReceived);
        }

        void OnMessageReceived(object sender, string msg)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                CrossLocalNotifications.Current.Show("Anda menerima notifikasi baru", msg, NOTIFICATION_ID, DatetimeHelper.GetDatetimeNow());
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps 
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
