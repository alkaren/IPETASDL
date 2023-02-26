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
    public partial class RincianProsesPage : ContentPage
    {
        DetailTransactionViewModel vmdetail;
        CustomerViewModel vmcus;
        HeaderTransactionViewModel vmtrans;
        HeaderTransaction headerTransaction;
        public RincianProsesPage(HeaderTransaction datas)
        {
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            vmtrans = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            InitializeComponent();
            Configuration(datas);
            headerTransaction = datas;
        }
        public void Nav()
        {
            try
            {
                var status1 = "TERSEDIA";
                var status2 = "CEK KETERSEDIAAN";
                var status3 = "PROSES PEMBAYARAN";
                var status4 = "MENUNGGU PEMBAYARAN";
                var status5 = "VALIDASI PEMBAYARAN";
                if (lblStatus.Text.Equals(status1))
                {
                    lblBelumAda.IsVisible = false;                    
                    BtnKirimPemesan.IsVisible = false;
                    BtnKirimBudi.IsVisible = false;
                }
                else if (lblStatus.Text.Equals(status2))
                {
                    lblBelumAda.IsVisible = false;
                    BtnKirimPemesan.IsVisible = false;
                    BtnKirimBudi.IsVisible = false;
                }
                else if (lblStatus.Text.Equals(status3))
                {
                    lblBelumAda.IsVisible = false;
                    BtnKirimPemesan.IsVisible = false;
                    BtnKirimBudi.IsVisible = false;
                }
                else
                {
                    lblBelumAda.IsVisible = false;
                    BtnKirimPemesan.IsVisible = false;
                    BtnKirimBudi.IsVisible = false;
                }
            }
            catch (Exception ec)
            {
                DisplayAlert("error", ec.ToString(), "ok");
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

                ////harga
                lblHarga.Text = product.Price.ToString();
                Nav();
            }
            catch (Exception er)
            {
                await DisplayAlert("Error", er.ToString(), "OK");
            }
        }
        private async Task<bool> Edit(HeaderTransaction param)
        {
            var content = await vmtrans.LoadAllHeaderTransaction();
            var newdata = new HeaderTransaction
            {                
                IdTransaction = param.IdTransaction,
                IdCustomer = param.IdCustomer,
                IdAttachment = param.IdAttachment,
                TransactionNumber = param.TransactionNumber,
                TransactionDate = param.TransactionDate,
                TransactionStatus = param.TransactionStatus,     
                PaymentMethode = param.PaymentMethode,
                PaymentTotal = param.PaymentTotal,
                CreatedDate = param.CreatedDate,
                CreatedBy = param.UpdatedBy,
                UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                UpdatedBy = Config.CurrentUserName,
                IdAttachmentNavigation = param.IdAttachmentNavigation,
                IdCustomerNavigation = param.IdCustomerNavigation            
            };

            var res = await vmtrans.UpdateHeaderTransaction(newdata);

            if (res)
                return true;

            return false;
        }

        #region button
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnKirimPemesan_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpPage(headerTransaction));
            await Navigation.PushAsync(new DaftarPesananPage());
            #region try
            //var input = new HeaderTransaction
            //{
            //    IdTransaction = Convert.ToInt32(lblId.Text),                
            //    TransactionStatus = "DISETUJUI",
            //    CreatedDate = Convert.ToDateTime(lblDate.Text),
            //    CreatedBy = lblName.Text       
            //};

            //var DoEdit = await Edit(input);

            //if (DoEdit)
            //{
            //    await DisplayAlert("Pemberitahuan", "Status Transaksi telah diubah", "OK");
            //    PopupNavigation.PushAsync(new PopUpPage());
            //    await Navigation.PushAsync(new DaftarPesananPage());
            //}
            //else
            //{
            //    await DisplayAlert("Pemberitahuan", "Terjadi Kesalahan", "OK");
            //}
            #endregion
        }
        private async void OnKirimBudi_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpBudiPage(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new DaftarSelesaiPage()));
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