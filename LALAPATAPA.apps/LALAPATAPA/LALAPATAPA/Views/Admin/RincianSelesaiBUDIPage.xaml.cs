using LALAPATAPA.Models;
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

namespace LALAPATAPA.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RincianSelesaiBUDIPage : ContentPage
    {
        DetailTransactionViewModel vmdetail;
        CustomerViewModel vmcus;
        public RincianSelesaiBUDIPage(HeaderTransaction datas)
        {
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            InitializeComponent();
            Configuration(datas);
            //lstRincian.ItemsSource = RincianHargaFactory.Rincians;
            //frmRincianHarga.HeightRequest = 300;
            //frmHarga.HeightRequest = 180;
            //lblBank.Text = "BCA";
            //lblBank.TextColor = Color.FromHex("#23A761");
            //lblNoRek.Text = "98237489374";
            //lblNoRek.TextColor = Color.FromHex("#23A761");
            //lblAtasNama.Text = "LALAPATAPA";
            //lblAtasNama.TextColor = Color.FromHex("#23A761");
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
                lblDate.Text = param.CreatedDate.Value.ToString("dd MMM yyyy");

                ////detailpesanan
                lbltype.Text = product.MapType;
                lblskala.Text = product.Scale;
                lblcomodity.Text = product.Commodity;
                lblprov.Text = product.Province;
                lblcity.Text = product.City;
                lblsubd.Text = product.SubDistrict;
                lblpesan.Text = product.Description;
                txtTujuan.Text = detail.ProcurementPurpose;

                //////harga
                lblHarga.Text = product.Price.ToString();  
            }
            catch (Exception er)
            {
                await DisplayAlert("Error", er.ToString(), "OK");
            }
        }
        #region button
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
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