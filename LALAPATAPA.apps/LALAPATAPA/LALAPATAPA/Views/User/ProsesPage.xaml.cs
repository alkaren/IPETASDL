using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.Helpers;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;

namespace LALAPATAPA.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProsesPage : ContentPage
    {
        LoadDataViewModel datavm;       
         
        public ProsesPage()
        {
            datavm = Locator.Instance.Resolve(typeof(LoadDataViewModel)) as LoadDataViewModel;

            InitializeComponent();
            BindingContext = new IRefreshPull(this);
            Configuration();
        }

        private async void Configuration()
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            list.ItemsSource = await datavm.CusLoadAllProsesTransaction();

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }
        #region Event Handler
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnTappedProses(object sender, ItemTappedEventArgs e)
        {
            var mylist = e.Item as HeaderTransaction;
            Config.CurrentTransactionId = mylist.IdTransaction;

            await Navigation.PushModalAsync(new NavigationPage(new DetailPesananPage(mylist)));
        }
        private async void OnTappedIconHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }
        private async void OnTappedIconBaru(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PesananBaruPage()));
        }
        private async void OnTappedIconProses(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProsesPage()));
        }
        private async void OnTappedIconSelesai(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiPage()));
        }
        private async void OnTappedIconAkun(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AkunPage()));
        }
        #endregion
    }
}