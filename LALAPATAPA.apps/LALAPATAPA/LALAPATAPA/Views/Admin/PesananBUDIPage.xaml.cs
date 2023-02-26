using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;

namespace LALAPATAPA.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PesananBUDIPage : ContentPage
    {
        HeaderTransactionViewModel vm;
        public string TransactionNumber { get; set; }
        public string TranscasTransactionStatus { get; set; }
        public DateTime? createdDate { get; set; }
        public PesananBUDIPage()
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            InitializeComponent();
            BindingContext = new IRefreshPull(this);
            //Device.BeginInvokeOnMainThread(LoadData);
            Configuration();
            
        }
        #region try
        public async void LoadData()
        {
            try
            {
                var content = "";
                HttpClient client = new HttpClient();
                var RestURL = "http://balittanah.azurewebsites.net/Headertransactions";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config.AccessToken);
                client.BaseAddress = new Uri(RestURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(RestURL).Result;
                content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<HeaderTransaction>>(content);
                selesailist.ItemsSource = Items;
            }
            catch (Exception ex)
            {
                await DisplayAlert("error", ex.ToString(), "OK");
            }
        }
        #endregion
        public async void Configuration()
        {
            var content = await vm.LoadAllHeaderTransaction();
            var TransBaru = from x in content
                            where x.TransactionStatus == "PROSES PEMBAYARAN"
                            select x;
            selesailist.ItemsSource = TransBaru;
        }
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnTappedPesanan(object sender, ItemTappedEventArgs e)
        {
            var mylist = e.Item as HeaderTransaction;
            await Navigation.PushModalAsync(new NavigationPage(new RincianPesananBUDIPage(mylist)));
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
    }
}