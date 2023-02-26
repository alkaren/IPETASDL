using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using Rg.Plugins.Popup.Services;
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
    public partial class RincianSelesaiLIAPage : ContentPage
    {
        DetailTransactionViewModel vmdetail;
        CustomerViewModel vmcus;
        HeaderTransaction headerTransaction;
        public RincianSelesaiLIAPage(HeaderTransaction datas)
        {
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            InitializeComponent();
            Configuration(datas);
            headerTransaction = datas;
        }
        public void Nav()
        {
            try
            {
                var status1 = "SELESAI";
                var status2 = "DIBATALKAN";
                var status3 = "DITOLAK";
                if (lblStatus.Text.Equals(status1))
                {
                    BtnKirimKurir.IsVisible = false;
                    BtnLokasi.IsVisible = false;
                }
                else if (lblStatus.Text.Equals(status2))
                {
                    BtnKirimKurir.IsVisible = false;
                    BtnLokasi.IsVisible = false;
                }
                else
                {
                    BtnKirimKurir.IsVisible = false;
                    BtnLokasi.IsVisible = false;
                }
            }
            catch(Exception er)
            {

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

                //////harga
                lblHarga.Text = product.Price.ToString();
                Nav();
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
        private async void OnKirimKurir_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpKirimKurir(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiLIAPage()));
        }
        private async void OnLokasi_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpLokasi(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiLIAPage()));
        }
        private async void OnSudahKirim_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpSudahKirim(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiLIAPage()));
        }
        private async void OnSudahDiambil_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpSudahDiambil(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiLIAPage()));
        }
        private async void OnKirimGres_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopupStatusPesananGresDone(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiLIAPage()));
        }
        private async void OnTappedIconHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MenuPage()));
        }
        private async void OnTappedIconBaru(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PesananLIAPage()));
        }
        private async void OnTappedIconProses(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProsesLIAPage()));
        }
        private async void OnTappedIconSelesai(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiLIAPage()));
        }
        private async void OnTappedIconAkun(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AkunAdminPage()));
        }
        #endregion
    }
}