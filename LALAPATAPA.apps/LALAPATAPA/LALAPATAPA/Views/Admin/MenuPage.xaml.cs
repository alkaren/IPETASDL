using LALAPATAPA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;
using LALAPATAPA.Models;

namespace LALAPATAPA.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        HeaderTransactionViewModel vm;
        HeaderTransaction dataHeader;
        public MenuPage()
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            InitializeComponent();
            UserLog();
            
        }

        private async void UserLog()
        {
            lblUser.Text = "Hi, " + Config.CurrentUserName.ToString();
            //floating icon for pesanan diproses
            var content = await vm.LoadAllHeaderTransaction();

            if(lblUser.Text == "Hi, yanjas")
            {
                DependencyService.Get<ILoadingPageService>().InitLoadingPage(new Views.LoadingIndicatorPage());
                DependencyService.Get<ILoadingPageService>().ShowLoadingPage();
                await Task.Delay(2000);
                var transpesanan = from x in content
                                  where x.TransactionStatus == Config.FlowStatus[0]
                                  select x;
                icoBaru.Text = transpesanan.Count().ToString();
                var TransProses = from x in content
                                  where x.TransactionStatus == Config.FlowStatus[1]
                                  || x.TransactionStatus == Config.FlowStatus[2]
                                  || x.TransactionStatus == Config.FlowStatus[3]
                                  || x.TransactionStatus == Config.FlowStatus[4]
                                  || x.TransactionStatus == Config.FlowStatus[5]
                                  select x;
                icoProses.Text = TransProses.Count().ToString();
                var TransSelesai = from x in content
                                   where x.TransactionStatus == Config.FlowStatus[6]
                                   || x.TransactionStatus == Config.FlowStatus[7]
                                   || x.TransactionStatus == Config.FlowStatus[8]
                                   select x;
                icoSelesai.Text = TransSelesai.Count().ToString();
                var kirims = from x in content
                                   where x.RequestDeliveryStatus == Config.FlowStatus[9]
                                   select x;
                icoKirim.Text = kirims.Count().ToString();
                var ambils = from x in content
                                   where x.RequestDeliveryStatus == Config.FlowStatus[10]
                                   select x;
                icoLokasi.Text = ambils.Count().ToString();
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
            }
            else if(lblUser.Text == "Hi, lia")
            {
                DependencyService.Get<ILoadingPageService>().InitLoadingPage(new Views.LoadingIndicatorPage());
                DependencyService.Get<ILoadingPageService>().ShowLoadingPage();
                await Task.Delay(2000);
                var transpesanan = from x in content
                                   where x.TransactionStatus == Config.FlowStatus[1]
                                   select x;
                icoBaru.Text = transpesanan.Count().ToString();
                var TransProses = from x in content
                                  where x.TransactionStatus == Config.FlowStatus[2]
                                  || x.TransactionStatus == Config.FlowStatus[3]
                                  || x.TransactionStatus == Config.FlowStatus[4]
                                  || x.TransactionStatus == Config.FlowStatus[5]
                                  select x;
                icoProses.Text = TransProses.Count().ToString();
                var TransSelesai = from x in content
                                   where x.TransactionStatus == Config.FlowStatus[6]
                                   || x.TransactionStatus == Config.FlowStatus[7]
                                   || x.TransactionStatus == Config.FlowStatus[8]
                                   select x;
                icoSelesai.Text = TransSelesai.Count().ToString();
                var kirims = from x in content
                             where x.RequestDeliveryStatus == Config.FlowStatus[9]
                             select x;
                icoKirim.Text = kirims.Count().ToString();
                var ambils = from x in content
                             where x.RequestDeliveryStatus == Config.FlowStatus[10]
                             select x;
                icoLokasi.Text = ambils.Count().ToString();
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
            }
            else if (lblUser.Text == "Hi, budi")
            {
                FrmKirim.IsVisible = false;
                FrmAmbil.IsVisible = false;
                DependencyService.Get<ILoadingPageService>().InitLoadingPage(new Views.LoadingIndicatorPage());
                DependencyService.Get<ILoadingPageService>().ShowLoadingPage();
                await Task.Delay(2000);
                var transpesanan = from x in content
                                   where x.TransactionStatus == Config.FlowStatus[3]
                                   select x;
                icoBaru.Text = transpesanan.Count().ToString();
                var TransProses = from x in content
                                  where x.TransactionStatus == Config.FlowStatus[4]
                                  || x.TransactionStatus == Config.FlowStatus[5]
                                  select x;
                icoProses.Text = TransProses.Count().ToString();
                var TransSelesai = from x in content
                                   where x.TransactionStatus == Config.FlowStatus[6]
                                   select x;
                icoSelesai.Text = TransSelesai.Count().ToString();                
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
            }
        }
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnTappedPesanan(object sender, EventArgs e)
        {
            var user1 = "Hi, yanjas";
            var user2 = "Hi, lia";
            var user3 = "Hi, budi";
            if (lblUser.Text.Equals(user2))
            {
                await Navigation.PushModalAsync(new NavigationPage(new PesananLIAPage()));
            }
            else if (lblUser.Text.Equals(user1))
            {
                await Navigation.PushModalAsync(new NavigationPage(new DaftarPesananPage()));
            }
            else if (lblUser.Text.Equals(user3))
            {
                await Navigation.PushModalAsync(new NavigationPage(new PesananBUDIPage()));
            }
            else
            {
                await DisplayAlert("Error", "Terjadi Kesalahan", "Ok");
            }
        }
        private async void OnTappedSelesai(object sender, EventArgs e)
        {
            var user1 = "Hi, yanjas";
            var user2 = "Hi, lia";
            var user3 = "Hi, budi";
            if (lblUser.Text.Equals(user2))
            {
                await Navigation.PushModalAsync(new NavigationPage(new SelesaiLIAPage()));
            }
            else if (lblUser.Text.Equals(user1))
            {
                await Navigation.PushModalAsync(new NavigationPage(new DaftarSelesaiPage()));
            }
            else if (lblUser.Text.Equals(user3))
            {
                await Navigation.PushModalAsync(new NavigationPage(new SelesaiBUDIPage()));
            }
            else
            {
                await DisplayAlert("Error", "Terjadi Kesalahan", "Ok");
            }
        }
        private async void OnTappedProses(object sender, EventArgs e)
        {
            var user1 = "Hi, yanjas";
            var user2 = "Hi, lia";
            var user3 = "Hi, budi";
            if (lblUser.Text.Equals(user2))
            {
                await Navigation.PushModalAsync(new NavigationPage(new ProsesLIAPage()));
            }
            else if (lblUser.Text.Equals(user1))
            {
                await Navigation.PushModalAsync(new NavigationPage(new DaftarProsesPage()));
            }
            else if (lblUser.Text.Equals(user3))
            {
                await Navigation.PushModalAsync(new NavigationPage(new ProsesBUDIPage()));
            }
            else
            {
                await DisplayAlert("Error", "Terjadi Kesalahan", "Ok");
            }
        }
        private async void OnTappedAkun(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AkunAdminPage()));
        }
        private async void OnTappedLokasi(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ListAmbilLokasiPage(dataHeader)));
        }
        private async void OnTappedKirim(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ListDikirimPage(dataHeader)));
        }
    }
}