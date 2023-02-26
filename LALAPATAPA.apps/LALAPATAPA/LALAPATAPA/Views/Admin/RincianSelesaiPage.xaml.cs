using LALAPATAPA.Models;
using LALAPATAPA.Helpers;
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
    public partial class RincianSelesaiPage : ContentPage
    {
        DetailTransactionViewModel vmdetail;
        CustomerViewModel vmcus;

        HeaderTransaction headerTransaction;
        public RincianSelesaiPage(HeaderTransaction datas)
        {
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            InitializeComponent();
            Configuration(datas);            
        }
        public void nav()
        {
            try
            {               
                var status1 = "SELESAI";
                var status2 = "DIBATALKAN";
                var status3 = "DITOLAK";
                if (lblStatus.Text.Equals(status1))
                {
                    lblBelumAda.IsVisible = false;
                    BtnKirimLia.IsVisible = false;
                }
                else
                {
                    lblBelumAda.IsVisible = false;
                    BtnKirimLia.IsVisible = false;
                }
            }
            catch (Exception ec)
            {
                DisplayAlert("error", ec.ToString(), "OK");
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
                lblId.Text = param.TransactionNumber;
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
                nav();
            }
            catch (Exception er)
            {
                await DisplayAlert("Error", er.ToString(), "OK");
            }
        }
            private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnKirimLia_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpLIAPage2(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new DaftarPesananPage()));
        }
        private async void OnKirimPemesan_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpPage(headerTransaction));
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
    }
}