using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.Gms.Common;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Plugin.Media;
using Plugin.CurrentActivity;
using Refractored.XamForms.PullToRefresh.Droid;
using Plugin.Permissions;
using LALAPATAPA.Helpers;

namespace LALAPATAPA.Droid
{
    [Activity(Label = "i-peta-sdl", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //CreateNotificationChannel();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            CrossMedia.Current.Initialize();
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            PullToRefreshLayoutRenderer.Init();
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            LoadApplication(new App());
            IsPlayServicesAvailable();
#if DEBUG
            // Force refresh of the token. If we redeploy the app, no new token will be sent but the old one will
            // be invalid.
            Task.Run(() =>
            {
                // This may not be executed on the main thread.
                FirebaseInstanceId.Instance.DeleteInstanceId();
                Console.WriteLine("Forced token: " + FirebaseInstanceId.Instance.Token);
            });
#endif
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    // In a real project you can give the user a chance to fix the issue.
                    Console.WriteLine($"Error: {GoogleApiAvailability.Instance.GetErrorString(resultCode)}");
                }
                else
                {
                    Console.WriteLine("Error: Play services not supported!");
                    Finish();
                }
                return false;
            }
            else
            {
                Console.WriteLine("Play Services available.");
                return true;
            }
        }
    }

    // This service handles the device's registration with FCM.
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        public async override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Console.WriteLine($"Token received: {refreshedToken}");
            await SendRegistrationToServerAsync(refreshedToken);
        }

        async Task SendRegistrationToServerAsync(string token)
        {
            try
            {
                // Formats: https://firebase.google.com/docs/cloud-messaging/concept-options
                // The "notification" format will automatically displayed in the notification center if the 
                // app is not in the foreground.
                const string templateBodyFCM =
                    "{" +
                        "\"notification\" : {" +
                        "\"body\" : \"$(messageParam)\"," +
                          "\"title\" : \"Anda menerima notifikasi baru\"," +
                        "\"icon\" : \"logo\" }" +
                    "}";

                var templates = new JObject();
                templates["genericMessage"] = new JObject
                {
                    {"body", templateBodyFCM}
                };

                var client = new MobileServiceClient(LALAPATAPA.App.MobileServiceUrl);
                var push = client.GetPush();

                await push.RegisterAsync(token, templates);

                // Push object contains installation ID afterwards.
                Console.WriteLine(push.InstallationId.ToString());
                Helpers.Config.NotificationToken = push.InstallationId.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debugger.Break();
            }
        }
    }

    // This service is used if app is in the foreground and a message is received.
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            Console.WriteLine("Received: " + message);

            // Android supports different message payloads. To use the code below it must be something like this (you can paste this into Azure test send window):
            // {
            //   "notification" : {
            //      "body" : "The body",
            //                 "title" : "The title",
            //                 "icon" : "myicon
            //   }
            // }
            try
            {
                var msg = message.GetNotification().Body;
                MessagingCenter.Send<object, string>(this, LALAPATAPA.App.NotificationReceivedKey, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error extracting message: " + ex);
            }
        }
    }
}