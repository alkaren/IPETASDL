using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;

namespace LALAPATAPA.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotifikasiPage : ContentPage
    {
        public NotifikasiPage()
        {
            InitializeComponent();
            Configuration();
        }

        private async void Configuration()
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            var transLog = await TransLog.GetTransactionLog(Config.CurrentUserId);

            if (transLog != null)
                notificationlist.ItemsSource = transLog;
            else
                await DisplayAlert("Pemberitahuan", "Tidak ada notifikasi", "Ok");

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }

        private async void OnTutup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}