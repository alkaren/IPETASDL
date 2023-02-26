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
    public partial class RincianPesananLIAPage : ContentPage
    {
        DetailTransactionViewModel vmdetail;
        CustomerViewModel vmcus;
        HeaderTransactionViewModel vmtrans;
        HeaderTransaction headerTransaction;

        Product products;
        public RincianPesananLIAPage(HeaderTransaction datas)
        {
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            vmtrans = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;

            InitializeComponent();
            Configuration(datas);
            loadattch(datas);
            headerTransaction = datas;            
        }
        public void Nav()
        {            
            var status4 = "CEK KETERSEDIAAN";
            
            if (lblStatus.Text.Equals(status4))
            {
                //lblBelumAda.IsVisible = false;
                //lstRincian.ItemsSource = RincianHargaFactory.Rincians;
                BtnTersedia.IsVisible = false;
                BtnTidakTersedia.IsVisible = true;
                StkTambahPeta.IsVisible = true;
            }
            else
            {
                //lblBelumAda.IsVisible = false;
                //lstRincian.ItemsSource = RincianHargaFactory.Rincians;
                BtnTersedia.IsVisible = false;
                BtnTidakTersedia.IsVisible = false;
                BtnKirimGres.IsVisible = false;
            }
        }
        public async void loadattch(HeaderTransaction param)
        {
            try
            {                              
                    var attach = await Data.LoadTrAttachment(param.IdTransaction);
                    ///Attach
                    lblzip.Text = attach.OrderAttachmentUrl;                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
        public async void Configuration(HeaderTransaction param)
        {
            try
            {
                var detail = await vmdetail.LoadDetailTransactionByTransId(param.IdTransaction);
                var pemesan = await vmcus.LoadCustData(param.IdCustomer.Value);
                var product = await Data.LoadProductById(detail.IdProduct.Value);
                var attach = await vmtrans.LoadAllHeaderTransactionByIdCus(param.IdCustomer.Value);
                //var attach = await 
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
                //lblHarga.Text = product.Price.ToString();

                Nav();
            }
            catch (Exception er)
            {
                await DisplayAlert("error", er.ToString(), "Ok");
            }

        }
        public bool Validasi()
        {
            if(MapName.Text == "" || MapName.Text == null)
            {
                DisplayAlert("Peringatan","Harap Isi Nama Peta","Ok");
                return false;
            }
            else if(Price.Text == "" || Price.Text == null)
            {
                DisplayAlert("Peringatan", "Harap Isi Harga Peta", "Ok");
                return false;
            }
            else if(Deskripsi.Text == "" || Deskripsi.Text == null)
            {
                DisplayAlert("Peringatan", "Harap Isi Deskripsi", "Ok");
                return false;
            }
            return true;
        }
        #region button
        private void OnTappedDownload(object sender, EventArgs e)
        {
            Xamarin.Forms.Device.OpenUri(new Uri(lblzip.Text));
        }
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnTersedia_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpTersedia(headerTransaction));            
        }
        private async void OnTidakTersedia_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpTidakTersedia(headerTransaction));
            //await Navigation.PushModalAsync(new NavigationPage(new DaftarPesananPage()));
        }
        private async void OnTambahPeta_Clicked(object sender, EventArgs e)
        {
            if (Validasi())
            {
                try
                {
                    var detail = await vmdetail.LoadDetailTransactionByTransId(headerTransaction.IdTransaction);
                    var val = await Data.LoadProductById(detail.IdProduct.Value);
                    var content = new Product
                    {
                        IdProduct = detail.IdProduct.Value,
                        Name = MapName.Text,
                        Description = val.Description,
                        Price = Convert.ToDecimal(Price.Text),
                        IsAvailable = Config.FlowStatus[2],
                        Version = null,
                        ImageUrl = val.ImageUrl,
                        FileUrl = val.FileUrl,
                        CreatedDate = val.CreatedDate,
                        CreatedBy = detail.CreatedBy,
                        UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                        UpdatedBy = Config.CurrentUserName,
                        MapType = val.MapType,
                        Province = val.Province,
                        City = val.City,
                        SubDistrict = val.SubDistrict,
                        Commodity = val.Commodity,
                        Scale = val.Scale,
                        AvailableDescription = Deskripsi.Text
                    };

                    var res = await Data.TambahPeta(content);

                    PopupNavigation.PushAsync(new PopupTambahPeta(headerTransaction));
                }
                catch (Exception ex)
                {
                    await DisplayAlert("error", ex.ToString(), "ok");
                }
            }
        }
        private async void OnLanjutkan_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PesananLIAPage()));
        }
        private async void OnKirimGres_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopupStatusPesananGres(headerTransaction));
            await Navigation.PushModalAsync(new NavigationPage(new PesananLIAPage()));
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