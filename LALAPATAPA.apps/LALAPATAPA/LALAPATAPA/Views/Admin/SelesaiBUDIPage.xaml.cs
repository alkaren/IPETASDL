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
    public partial class SelesaiBUDIPage : ContentPage
    {
        HeaderTransactionViewModel vm;
        public SelesaiBUDIPage()
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            InitializeComponent();
            BindingContext = new IRefreshPull(this);
            //selesailist.ItemsSource = SelesaiBUDIFactory.Selesais;
            Configuration();
        }
        public async void Configuration()
        {
            var content = await vm.LoadAllHeaderTransaction();
            var TransSelesai = from x in content
                            where x.TransactionStatus == "SELESAI"
                            select x;
            selesailist.ItemsSource = TransSelesai;
        }
        #region button
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnTappedSelesai(object sender, ItemTappedEventArgs e)
        {
            var mylist = e.Item as HeaderTransaction;
            await Navigation.PushModalAsync(new NavigationPage(new RincianSelesaiBUDIPage(mylist)));
        }
        private async void OnTappedIconHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MenuPage()));
        }
        private async void OnTappedIconBaru(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PesananBUDIPage()));
        }
        private async void OnTappedIconProses(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProsesBUDIPage()));
        }
        private async void OnTappedIconSelesai(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiBUDIPage()));
        }
        private async void OnTappedIconAkun(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AkunAdminPage()));
        }
        #endregion
    }
}