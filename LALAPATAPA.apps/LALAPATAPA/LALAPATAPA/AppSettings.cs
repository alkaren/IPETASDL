using Xamarin.Essentials;
using LALAPATAPA.Models;
using LALAPATAPA.Helpers;

namespace LALAPATAPA
{
	public static class AppSettings
    {
        //IF YOU DEPLOY YOUR OWN ENDPOINT REPLACE THE VALUEW BELOW

        //App Center
        const string defaultAppCenterAndroid = "b3b1403c-3f9d-4c77-805e-9c002de6ddf7";
        const string defaultAppCenteriOS = "7a2a290b-07b0-47dc-9dcd-15461e894e6d";
        const string defaultAppCenterUWP = "140a8550-c309-4bc1-a05d-e5a0f7e4df1d";

        static string defaultNotificationsEndpoint;
        static string defaultSettingsFileUrl;
        
        // Maps
        const string defaultBingMapsApiKey = "AkSuJ-YtW4VDvIzErxK3ke2ILQD1muWwS2KN2QvhqHobx4YBEIYqkEVBLyx1LYby";
        public const string DefaultFallbackMapsLocation = "40.762246,-73.986943";
        
        // Fakes
        const bool defaultUseFakes = false;

        static AppSettings()
        {           
            defaultNotificationsEndpoint = "http://sh360production.2c3abf6edd44497688b2.westus.aksapp.io/notifications-api";
            defaultSettingsFileUrl = "http://sh360production.2c3abf6edd44497688b2.westus.aksapp.io/configuration-api/cfg/aks";
		}
        
        // API Endpoints
        
        public static string NotificationsEndpoint
        {
            get => Preferences.Get(nameof(NotificationsEndpoint), defaultNotificationsEndpoint);
            set => Preferences.Set(nameof(NotificationsEndpoint), value);
        }
        // Other settings
        
        public static string SettingsFileUrl
        {
            get => Preferences.Get(nameof(SettingsFileUrl), defaultSettingsFileUrl);
            set => Preferences.Set(nameof(SettingsFileUrl), value);
        }
        public static string FallbackMapsLocation
        {
            get => Preferences.Get(nameof(FallbackMapsLocation), DefaultFallbackMapsLocation);
            set => Preferences.Set(nameof(FallbackMapsLocation), value);
        }

        public static User User
        {
            get => PreferencesHelpers.Get(nameof(User), default(User));
            set => PreferencesHelpers.Set(nameof(User), value);
        }
        public static bool UseFakes
        {
            get => Preferences.Get(nameof(UseFakes), defaultUseFakes);
            set => Preferences.Set(nameof(UseFakes), value);
        }
        
        public static void RemoveUserData() => Preferences.Remove(nameof(User));
		
	
    }
}
