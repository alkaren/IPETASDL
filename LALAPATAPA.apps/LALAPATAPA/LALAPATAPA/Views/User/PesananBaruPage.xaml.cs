using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Plugin.FilePicker;

namespace LALAPATAPA.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PesananBaruPage : ContentPage
    {
        HeaderTransactionViewModel vmtrans;
        DetailTransactionViewModel vmdetail;
        ProductViewModel vmproduct;
        NotificationsViewModel vmnotif;
        
        //single use variable
        private string _propinsi;
        private string _kabupaten;
        private string _komoditas;
        private string _kecamatan;

        private byte[] dataAttachment;
        private string filename;
        private List<string> Peta = new List<string>();
        private List<string> SkalaPeta = new List<string>();

        public PesananBaruPage()
        {
            vmtrans = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmproduct = Locator.Instance.Resolve(typeof(ProductViewModel)) as ProductViewModel;
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;

            InitializeComponent();
            Configuration();
        }

        public void Configuration()
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            // populate propisi
            cbPropinsi.ItemsSource = LocationHelper.GetPropinsi().ToList();

            // populate jenispeta
            var rawJeniPeta = PetaFactory.peta;
            Peta.Clear();
            foreach (var item in rawJeniPeta)
            {
                Peta.Add(item.JenisPeta);
            }
            cbJenisPeta.ItemsSource = Peta;

            // populate skala
            var rawskala = SkalaFactory.skalaPeta;
            SkalaPeta.Clear();
            foreach (var item in rawskala)
            {
                SkalaPeta.Add(item.Skala);
            }
            cbSkala.ItemsSource = SkalaPeta;

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }

        private bool IsInputValidate()
        {
            bool ret = false;

            _propinsi = cbPropinsi.SelectedItem == null ? _propinsi = "-" : cbPropinsi.SelectedItem.ToString();
            _kabupaten = cbKabupaten.SelectedItem == null ? _kabupaten = "-" : cbKabupaten.SelectedItem.ToString();
            _komoditas = txtKomoditas.Text == null ? _komoditas = "-" : txtKomoditas.Text;
            _kecamatan = txtKecamatan.Text == null ? _kecamatan = "-" : txtKecamatan.Text;

            if (cbJenisPeta.SelectedItem == null)
            {
                DisplayAlert("Peringatan", "Silahkan isi jenis peta", "OK");
                ret = false;
            }
            else if (cbSkala.SelectedItem == null)
            {
                DisplayAlert("Peringatan", "Silahkan isi skala peta", "OK");
                ret = false;
            }
            else if(txtPesanan.Text == null)
            {
                DisplayAlert("Peringatan", "Silahkan isi detail pesanan", "OK");
                ret = false;
            }
            else if(txtTujuan.Text == null)
            {
                DisplayAlert("Peringatan", "SIlahkan isi tujuan penggunaan peta", "OK");
                ret = false;
            }
            else
            {
                ret = true;
            }

            return ret;
        }

        #region try
        //public async Task<HeaderTransaction> SavePesanan(HeaderTransaction transaction)
        //{
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
        //    var res = await client.PostAsync(Config.WebService + "/headertransactions", stringContent);
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var newjson = res.Content.ReadAsStringAsync().Result;
        //        var data = JsonConvert.DeserializeObject<HeaderTransaction>(newjson);
        //        _idTransaction = data.IdTransaction;

        //        return data;
        //    }

        //    return null;
        //}

        //public async Task<bool> SaveDetail(DetailTransaction detail)
        //{
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(detail), Encoding.UTF8, "application/json");
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
        //    var res = await client.PostAsync(Config.WebService + "/detailtransactions", stringContent);
        //    if (res.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public async Task<bool> SaveProduct(Product product)
        //{
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
        //    var res = await client.PostAsync(Config.WebService + "/products", stringContent);
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var newjson = res.Content.ReadAsStringAsync().Result;
        //        var data = JsonConvert.DeserializeObject<Product>(newjson);
        //        _idProduct = data.IdProduct;

        //        return true;
        //    }

        //    return false;
        //}
        #endregion

        private async void DoPesan()
        {
            try
            {
                if (IsInputValidate())
                {
                    // show the loading page...
                    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                    var product = new Product
                    {
                        MapType = cbJenisPeta.SelectedItem.ToString(),
                        Scale = cbSkala.SelectedItem.ToString(),
                        Commodity = _komoditas,
                        Province = _propinsi,
                        City = _kabupaten,
                        SubDistrict = _kecamatan,
                        Description = txtPesanan.Text,
                        CreatedBy = Config.CurrentUserName,
                        CreatedDate = DatetimeHelper.GetDatetimeNow()
                        
                    };

                    var saveproduct = await vmproduct.SaveProduct(product);

                    if (saveproduct != null)
                    {
                        var header = new HeaderTransaction
                        {
                            IdCustomer = Config.CurrentUserId,
                            TransactionStatus = Config.FlowStatus[0],
                            TransactionNumber = DatetimeHelper.GetDatetimeNow().ToString("ddMMyyyyhhmmss"),
                            CreatedDate = DatetimeHelper.GetDatetimeNow(),
                            CreatedBy = Config.CurrentUserName,
                            OrderAttachmentUrl = filename != null ? Config.BlobUrl + filename.Trim() : null
                        };

                        var saveheader = await vmtrans.SavePesanan(header);

                        if (saveheader != null)
                        {
                            var detail = new DetailTransaction
                            {
                                IdTransaction = saveheader.IdTransaction,
                                IdProduct = saveproduct.IdProduct,
                                Quantity = 1,
                                CreatedDate = DatetimeHelper.GetDatetimeNow(),
                                CreatedBy = Config.CurrentUserName,
                                ProcurementPurpose = txtTujuan.Text
                            };

                            var savedetail = await vmdetail.SaveDetail(detail);

                            if (savedetail != null)
                            {

                                var notif = new Notification
                                {
                                    Text = $"Anda menerima pesanan baru {saveheader.TransactionNumber}"
                                };

                                await vmnotif.PushToYanjas(notif);

                                // close the loading page...
                                DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                                await DisplayAlert("Pemberitahuan", "Pesanan berhasil disimpan", "OK");

                                App.Current.MainPage = new NavigationPage(new MainPage());

                                var translog = new TransactionLog
                                {
                                    IdTransaction = saveheader.IdTransaction,
                                    Message = $"mengirim pesanan baru. dengan nomor transaksi {saveheader.TransactionNumber}",
                                    Source = Config.CurrentUserName,
                                    Time = DatetimeHelper.GetDatetimeNow()
                                };

                                await TransLog.SaveTransLog(translog);

                            }
                            else
                            {
                                // close the loading page...
                                DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                                await DisplayAlert("Pemberitahuan", "Gagal membuat pesanan kode error xErrorDetail", "OK");
                            }
                        }
                        else
                        {
                            // close the loading page...
                            DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                            await DisplayAlert("Pemberitahuan", "Gagal membuat pesanan kode error xErrorHeader", "OK");
                        }
                    }
                    else
                    {
                        // close the loading page...
                        DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                        await DisplayAlert("Pemberitahuan", "Gagal menyimpan pesanan kode error xErrorProduct", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps_PesananBaruPage_Pesan_Clicked");

            }
        }
        private async Task PickAndShowFile(string[] fileTypes)
        {
            try
            {
                var pickedFile = await CrossFilePicker.Current.PickFile(fileTypes);

                if (pickedFile != null)
                {
                    FileNameLabel.Text = pickedFile.FileName;
                    btnAmbil.IsVisible = false;
                    btnUpload.IsVisible = true;
                    dataAttachment = pickedFile.DataArray;
                }
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps_PesananBaruPage_PickAndShowFile");
                await DisplayAlert("Pemberitahuan", "Terjadi kesalahan saat membuka file", "Mengerti");
            }
        }

        #region Event Handler
        private async void Ambil_Clicked(object sender, EventArgs args)
        {
            string[] fileType = null;
            
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                fileType = new string[] { "application/zip", "application/pdf" };
            }
            await PickAndShowFile(fileType);
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

        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }

        private async void Pesan_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Pemberitahuan", "Kirim Pesanan?", "Ya", "Batal");
            if (response)
                DoPesan();
        }

        #region tryradiobutton

        //private void RbKota_Checked(object sender, EventArgs e)
        //{
        //    if (rbKota.IsChecked)
        //    {
        //        // populate kota
        //        if (cbPropinsi.SelectedItem != null)
        //        {
        //            string param = cbPropinsi.SelectedItem.ToString();
        //            cbKabupaten.ItemsSource = LocationHelper.GetPemerintahan(param).ToList();
        //        }
        //    }
        //}

        //private void RbKab_Checked(object sender, EventArgs e)
        //{
        //    if (rbKab.IsChecked)
        //    {
        //        // populate kabupaten
        //        if (cbPropinsi.SelectedItem != null)
        //        {
        //            string param = cbPropinsi.SelectedItem.ToString();
        //            cbKabupaten.ItemsSource = LocationHelper.GetKabupaten(param).ToList();
        //        }
        //    }
        //}
        #endregion

        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {
            try
            {
                // show the loading page...
                DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                string date = DatetimeHelper.GetDatetimeNow().ToString("ddMMyyyyhhmmss");
                filename = $"attachment{date}{FileNameLabel.Text}";
                var res = await BlobsService.ProcessAsync(dataAttachment, filename);

                if (res)
                {
                    // close the loading page...
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                    await DisplayAlert("Pemberitahuan", "Upload file SAP berhasil", "Ok");
                }
                else
                {
                    // close the loading page...
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                    await DisplayAlert("Pemberitahuan", "Upload file SAP Gagal", "Ok");
                }
            }
            catch
            {
                // close the loading page...
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                await DisplayAlert("Pemberitahuan", "Gagal upload file SAP", "OK");
            }
        }

        private void CbPropinsi_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            if (cbPropinsi.SelectedItem != null)
            {
                var dataKabupaten = LocationHelper.GetKabupaten(cbPropinsi.SelectedItem.ToString());
                cbKabupaten.ItemsSource = dataKabupaten.ToList();
            }
        }
        #endregion
    }
}