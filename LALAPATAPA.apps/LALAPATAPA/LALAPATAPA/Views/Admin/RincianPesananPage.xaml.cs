using LALAPATAPA.Models;
using LALAPATAPA.Helpers;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
    public partial class RincianPesananPage : ContentPage
    {
        DetailTransactionViewModel vmdetail;
        CustomerViewModel vmcus;
        HeaderTransaction headerTransaction;
        public string TransactionStatus { get; set; }
        public RincianPesananPage(HeaderTransaction datas)
        {
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            InitializeComponent();
            //GetStatusAsync();
            Configuration(datas);
            headerTransaction = datas;
        }
        public void Nav()
        {
            var status1 = "MENUNGGU PERSETUJUAN";
            if (lblStatus.Text.Equals(status1))
            {
                BtnKirim.IsVisible = true;
                BtnKirimUlang.IsVisible = false;
            }
            else
            {
                BtnKirim.IsVisible = false;
                BtnKirimUlang.IsVisible = false;
            }
        }
        public async void Configuration(HeaderTransaction param)
        {
            try
            {
                var detail = await vmdetail.LoadDetailTransactionByTransId(param.IdTransaction);
                var pemesan = await vmcus.LoadCustData(param.IdCustomer.Value);                
                var product = await Data.LoadProductById(detail.IdProduct.Value);
                //header
                lblId.Text = $"Pesanan {param.TransactionNumber}";
                lblStatus.Text = param.TransactionStatus;
                lblDate.Text = param.CreatedDate.Value.ToString("dd MMMM yyyy");

                //pemesan
                lblName.Text = param.CreatedBy;
                lblNohp.Text = pemesan.Contact;
                lblEmail.Text = pemesan.Email;
                lblComp.Text = pemesan.Company;

                ////detailpesanan
                lbltype.Text = product.MapType;
                lblskala.Text = product.Scale;
                lblcomodity.Text = product.Commodity;
                lblprov.Text = product.Province;
                lblcity.Text = product.City;
                lblsubd.Text = product.SubDistrict;
                lblpesan.Text = product.Description;
                txtTujuan.Text = detail.ProcurementPurpose;
                Nav();
            }
            catch (Exception er)
            {
                await DisplayAlert("Error", er.ToString(), "OK");
            }

        }
        #region try
        //public async void GetStatusAsync()
        //{
        //    try
        //    {
        //        var content = "";
        //        HttpClient client = new HttpClient();
        //        var RestURL = "http://balittanah.azurewebsites.net/Headertransactions";
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config.AccessToken);
        //        client.BaseAddress = new Uri(RestURL);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = client.GetAsync(RestURL).Result;
        //        content = await response.Content.ReadAsStringAsync();
        //        var Items = JsonConvert.DeserializeObject<List<HeaderTransaction>>(content);
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("error", ex.ToString(), "OK");
        //    }
        //}
        #endregion
        #region button
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnKirim_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpLIAPage(headerTransaction));            
        }
        private async void OnKirimUlang_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpLIAPage2(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new DaftarPesananPage()));
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