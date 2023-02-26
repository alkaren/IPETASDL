using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace LALAPATAPA.Helpers
{
    public static class Config
    {
        static string DefaultWebService = "https://lalapatapaservice.azurewebsites.net";
        static string DefaultBlobsService = "DefaultEndpointsProtocol=https;AccountName=balittanahstorage;AccountKey=0ZatHPCCk5NmdYtZjGx7Jm5z1ULOba3PvuOcggcoYczqIrP8C+wFmQjHcS9aiO0u5wK4tMSGSDNvqO1ofyJmcA==;EndpointSuffix=core.windows.net";
        static string DefaultRootBlobUrl = "https://balittanahstorage.blob.core.windows.net/lalapatapa/";
        static string DefaultBlobsContainer = "lalapatapa";
        static string DefaultPhoneNumber = "+6281219344133";

        public static string WebService
        {
            get => Preferences.Get(nameof(WebService), DefaultWebService);
            set => Preferences.Set(nameof(WebService), value);
        }
        public static string BlobsService
        {
            get => Preferences.Get(nameof(BlobsService), DefaultBlobsService);
            set => Preferences.Set(nameof(BlobsService), value);
        }
        public static string BlobsContainer
        {
            get => Preferences.Get(nameof(BlobsContainer), DefaultBlobsContainer);
            set => Preferences.Set(nameof(BlobsContainer), value);
        }
        public static string BlobUrl
        {
            get => Preferences.Get(nameof(BlobUrl), DefaultRootBlobUrl);
            set => Preferences.Set(nameof(BlobUrl), value);
        }
        public static string PhoneNumber
        {
            get => Preferences.Get(nameof(PhoneNumber), DefaultPhoneNumber);
            set => Preferences.Set(nameof(PhoneNumber), value);
        }
        public static string AccessToken
        {
            get => Preferences.Get(nameof(AccessToken), "");
            set => Preferences.Set(nameof(AccessToken), value);
        }
        public static string CurrentUserName
        {
            get => Preferences.Get(nameof(CurrentUserName), "");
            set => Preferences.Set(nameof(CurrentUserName), value);
        }
        public static int CurrentUserId
        {
            get => Preferences.Get(nameof(CurrentUserId), -1);
            set => Preferences.Set(nameof(CurrentUserId), value);
        }
        public static int CurrentTransactionId
        {
            get => Preferences.Get(nameof(CurrentTransactionId), -1);
            set => Preferences.Set(nameof(CurrentTransactionId), value);
        }
        public static string CurrentEmailUser
        {
            get => Preferences.Get(nameof(CurrentEmailUser), "");
            set => Preferences.Set(nameof(CurrentEmailUser), value);
        }
        public static int CurrentUserGroup
        {
            get => Preferences.Get(nameof(CurrentUserGroup), -1);
            set => Preferences.Set(nameof(CurrentUserGroup), value);
        }
        public static int CurrentAccountId
        {
            get => Preferences.Get(nameof(CurrentAccountId), -1);
            set => Preferences.Set(nameof(CurrentAccountId), value);
        }
        public static string NotificationToken
        {
            get => Preferences.Get(nameof(NotificationToken), "");
            set => Preferences.Set(nameof(NotificationToken), value);
        }
        public static string[] FlowStatus = {
            "MENUNGGU PERSETUJUAN",
            "CEK KETERSEDIAAN",
            "TERSEDIA",                 
            "PROSES PEMBAYARAN",
            "MENUNGGU PEMBAYARAN",
            "VALIDASI PEMBAYARAN",
            "SELESAI",
            "DITOLAK",
            "DIBATALKAN",
            "DIKIRIM VIA KURIR",
            "DIAMBIL KE LOKASI",
            "TELAH DIAMBIL",
            "TELAH DIKIRIM",
            "TRANSAKSI TELAH SELESAI"
        };
    }
}
