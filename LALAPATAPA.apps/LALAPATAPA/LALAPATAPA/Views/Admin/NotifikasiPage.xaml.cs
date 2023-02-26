using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALAPATAPA.Views.Admin
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
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();
            var translog = await TransLog.GetAllTransactionLog();

            if (translog != null)
            {
                var order = from x in translog
                            orderby x.Time descending
                            select x;
                notificationlist.ItemsSource = order;
            }
            else
            {
                await DisplayAlert("Pemberitahuan", "Tidak ada notifikasi", "Ok");
            }
            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }
        
        private async void OnTutup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}