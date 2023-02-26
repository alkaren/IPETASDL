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
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;

namespace LALAPATAPA.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPesananPage : ContentPage
    {
        DetailTransactionViewModel vm;
        HeaderTransaction headerTransaction;
        public DetailPesananPage(HeaderTransaction header)
        {
            vm = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            InitializeComponent();
            Configuration(header);
            headerTransaction = header;
        }
        private void LoadProduct(Product param)
        {
            lbTanggal.Text = param.CreatedDate.Value == null ? "-" : param.CreatedDate.Value.ToString("dd MMMM yyy");
            txtPeta.Text = param.MapType == null ? "-" : param.MapType;
            txtSkala.Text = param.Scale == null ? "-" : param.Scale;
            txtKomoditas.Text = param.Commodity == null ? "-" : param.Commodity;
            txtPropinsi.Text = param.Province == null ? "-" : param.Province;
            txtKota.Text = param.City == null ? "-" : param.City;
            txtKecamatan.Text = param.SubDistrict == null ? "-" : param.SubDistrict;
            txtPesan.Text = param.Description == null ? "-" : param.Description;
            txtTotal.Text = param.Price == null ? "-" : "Rp." + param.Price.Value.ToString("0.00");
            txtStatusPemesanan.Text = param.AvailableDescription;
        }

        private void LoadPesanan(Product param)
        {
            lbTanggal.Text = param.CreatedDate.Value == null ? "-" : param.CreatedDate.Value.ToString("dd MMMM yyy");
            txtPeta.Text = param.MapType == null ? "-" : param.MapType;
            txtSkala.Text = param.Scale == null ? "-" : param.Scale;
            txtKomoditas.Text = param.Commodity == null ? txtKomoditas.Text = "-" : param.Commodity;
            txtPropinsi.Text = param.Province == null ? "-" : param.Province;
            txtKota.Text = param.City == null ? "-" : param.City;
            txtKecamatan.Text = param.SubDistrict == null ? txtKecamatan.Text = "-" : param.SubDistrict;
            txtPesan.Text = param.Description == null ? "-" : param.Description;
        }

        private void LoadBankAccount(BankAccount param, decimal? price)
        {
            txtPembayaran.Text = $"Harap melakukan transfer ke rekening {param.BankName} {param.AccountOwner} {param.AccountNo} sebesar RP.{price.Value.ToString("0.00")}";
            txtStart.Text = param.ValidStart.Value.ToString("dd MMMM yyyy");
            txtEnd.Text = param.ValidUntil.Value.ToString("dd MMMM yyyy");
        }

        public async void Configuration(HeaderTransaction param)
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            var status = param.TransactionStatus;
            lblStatus.Text = status;
            TransNo.Text = $"Pesanan {param.TransactionNumber}";
            txtLampiran.Text = param.OrderAttachmentUrl != null ? param.OrderAttachmentUrl : "Tidak ada lampiran";

            if (lblStatus.Text.Equals(Config.FlowStatus[0]))
            {
                BtnBatal.IsVisible = false;
                BtnSetuju.IsVisible = false;
                BtnBayar.IsVisible = false;
                slTotal.IsVisible = false;
                slPembayaran.IsVisible = false;
                slRincian.IsVisible = false;
                lblBelumAda.IsVisible = false;
                slStatusPemesanan.IsVisible = false;

                var detail = await vm.LoadDetailTransactionByTransId(param.IdTransaction);
                txtTujuan.Text = detail.ProcurementPurpose;

                var productId = detail.IdProduct.Value;
                var product = await Data.LoadProductById(productId);
                LoadPesanan(product);
            }
            else if (lblStatus.Text.Equals(Config.FlowStatus[1]))
            {
                BtnBatal.IsVisible = false;
                BtnSetuju.IsVisible = false;
                BtnBayar.IsVisible = false;
                slTotal.IsVisible = false;
                slPembayaran.IsVisible = false;
                slRincian.IsVisible = false;
                lblRincian.IsVisible = false;
                lblBelumAda.IsVisible = false;
                slStatusPemesanan.IsVisible = false;

                var detail = await vm.LoadDetailTransactionByTransId(param.IdTransaction);
                txtTujuan.Text = detail.ProcurementPurpose;

                var productId = detail.IdProduct.Value;
                var product = await Data.LoadProductById(productId);
                LoadPesanan(product);
            }
            else if (lblStatus.Text.Equals(Config.FlowStatus[2]))
            {
                BtnBayar.IsVisible = false;
                BtnSetuju.IsVisible = true;
                BtnBatal.IsVisible = true;
                slTotal.IsVisible = true;
                slPembayaran.IsVisible = false;
                slStatusPemesanan.IsVisible = true;
                slRincian.IsVisible = true;
                lblBelumAda.IsVisible = false;

                var detail = await vm.LoadDetailTransactionByTransId(param.IdTransaction);
                txtTujuan.Text = detail.ProcurementPurpose;

                var productId = detail.IdProduct.Value;
                var product = await Data.LoadProductById(productId);
                LoadProduct(product);
            }
            else if (lblStatus.Text.Equals(Config.FlowStatus[3]))
            {
                BtnBatal.IsVisible = false;
                BtnSetuju.IsVisible = false;
                BtnBayar.IsVisible = false;
                slTotal.IsVisible = true;
                slPembayaran.IsVisible = false;
                slStatusPemesanan.IsVisible = true;
                slRincian.IsVisible = true;
                lblBelumAda.IsVisible = false;

                var detail = await vm.LoadDetailTransactionByTransId(param.IdTransaction);
                txtTujuan.Text = detail.ProcurementPurpose;

                var productId = detail.IdProduct.Value;
                var product = await Data.LoadProductById(productId);
                LoadProduct(product);
            }
            else if(lblStatus.Text.Equals(Config.FlowStatus[4]))
            {
                BtnBatal.IsVisible = false;
                BtnSetuju.IsVisible = false;
                BtnBayar.IsVisible = true;
                slTotal.IsVisible = true;
                slPembayaran.IsVisible = true;
                slStatusPemesanan.IsVisible = true;
                slRincian.IsVisible = true;
                lblBelumAda.IsVisible = false;

                var detail = await vm.LoadDetailTransactionByTransId(param.IdTransaction);
                txtTujuan.Text = detail.ProcurementPurpose;

                var productId = detail.IdProduct.Value;
                var product = await Data.LoadProductById(productId);
                var bankAccount = await Data.LoadBankAccountById(detail.IdBankAccount.Value);
                LoadProduct(product);
                LoadBankAccount(bankAccount, product.Price);
            }
            else if(lblStatus.Text.Equals(Config.FlowStatus[5]))
            {
                BtnBatal.IsVisible = false;
                BtnSetuju.IsVisible = false;
                BtnBayar.IsVisible = false;
                slTotal.IsVisible = true;
                slPembayaran.IsVisible = false;
                slStatusPemesanan.IsVisible = true;
                slRincian.IsVisible = true;
                lblBelumAda.IsVisible = false;

                var detail = await vm.LoadDetailTransactionByTransId(param.IdTransaction);
                txtTujuan.Text = detail.ProcurementPurpose;

                var productId = detail.IdProduct.Value;
                var product = await Data.LoadProductById(productId);
                LoadProduct(product);
            }

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }

        #region Event Handler
        private void OnBatal_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpBatalPage(headerTransaction));
        }
        private void OnSetuju_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpSetujuPage(headerTransaction));
        }
        private async void OnBayar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PembayaranPage(headerTransaction)));
        }
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
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