using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;

namespace LALAPATAPA.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DaftarProsesPage : ContentPage
    {
        HeaderTransactionViewModel vm;
        public DaftarProsesPage()
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            InitializeComponent();
            BindingContext = new IRefreshPull(this);
            //selesailist.ItemsSource = AdminProsesFactory.Proses;
            Configuration();

            
        }
        public async void Configuration()
        {
            var content = await vm.LoadAllHeaderTransaction();
            var TransProses = from x in content
                              where x.TransactionStatus == "CEK KETERSEDIAAN"
                              || x.TransactionStatus == "TERSEDIA"
                              || x.TransactionStatus == "PROSES PEMBAYARAN"
                              || x.TransactionStatus == "MENUNGGU PEMBAYARAN"
                              || x.TransactionStatus == "VALIDASI PEMBAYARAN"                              
                              select x;
            selesailist.ItemsSource = TransProses;
        }
        
        #region button
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnTappedProses(object sender, ItemTappedEventArgs e)
        {
            var mylist = e.Item as HeaderTransaction;            

            await Navigation.PushModalAsync(new NavigationPage(new RincianProsesPage(mylist)));
        }
        private async void OnTappedIconHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MenuPage()));
        }
        private async void OnTappedIconBaru(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new DaftarPesananPage()));
        }
        private async void OnTappedIconProses(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new DaftarProsesPage()));
        }
        private async void OnTappedIconSelesai(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new DaftarSelesaiPage()));
        }
        private async void OnTappedIconAkun(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AkunAdminPage()));
        }
        #endregion
    }
}