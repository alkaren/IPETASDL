using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;

namespace LALAPATAPA.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailSelesaiPage : ContentPage
    {
        DetailTransactionViewModel vmdetail;
        HeaderTransactionViewModel vmheader;
        NotificationsViewModel vmnotif;

        HeaderTransaction headerTransaction;

        public DetailSelesaiPage(HeaderTransaction datas)
        {
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmheader = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;
            InitializeComponent();
            Configuration(datas);
            headerTransaction = datas;
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
            txtKeterangan.Text = param.AvailableDescription == null ? "-" : param.AvailableDescription;
        }

        private void LoadCustomers()
        {
            Email.Text = Config.CurrentEmailUser;
        }

        public async void Configuration(HeaderTransaction param)
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            var status = param.TransactionStatus;
            var detail = await vmdetail.LoadDetailTransactionByTransId(param.IdTransaction);
            txtTujuan.Text = detail.ProcurementPurpose;

            var productId = detail.IdProduct.Value;
            var product = await Data.LoadProductById(productId);

            lblStatus.Text = status;
            TransNo.Text = $"Pesanan {param.TransactionNumber}";
            txtLampiran.Text = param.OrderAttachmentUrl != null ? param.OrderAttachmentUrl : "Tidak ada lampiran";
            LoadProduct(product);
            LoadCustomers();

            if (lblStatus.Text.Equals(Config.FlowStatus[8]))
            {
                frmStatus.BackgroundColor = Color.FromHex("#D13333");
                frmStatus.BorderColor = Color.FromHex("#D13333");
                lblStatus.TextColor = Color.White;
                BtnPesananSelesai.IsVisible = false;
                lblBelumAda.Text = "Pesanan ini telah anda batalkan";
                slTotal.IsVisible = false;
                slSudahDibayar.IsVisible = false;
                slRincian.IsVisible = false;
                slKeterangan.IsVisible = false;
            }
            if (lblStatus.Text.Equals(Config.FlowStatus[7]))
            {
                frmStatus.BackgroundColor = Color.FromHex("#D13333");
                frmStatus.BorderColor = Color.FromHex("#D13333");
                lblStatus.TextColor = Color.White;
                BtnPesananSelesai.IsVisible = false;
                slTotal.IsVisible = false;
                slSudahDibayar.IsVisible = false;
                slRincian.IsVisible = false;
                slKeterangan.IsVisible = false;
            }
            else if (lblStatus.Text.Equals(Config.FlowStatus[6]))
            {
                lblBelumAda.IsVisible = false;
                slSelesai.IsVisible = param.CustomerConfirmation == Config.FlowStatus[13] ? false : true;
                slSudahDibayar.IsVisible = true;
                slTotal.IsVisible = true;
            }

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }

        private async void MakeCall()
        {
            try
            {
                PhoneDialer.Open(Config.PhoneNumber);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Pemberitahuan", "Device andat tidak support untuk melakukan aksi ini", "Mengerti");
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps_DetailSelesaiPage_MakeCall");
                await DisplayAlert("Pemberitahuan", "Terjadi kesalahan", "Mengerti");
            }
        }

        #region Event Handler
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

        private async void BtnAmbil_Clicked(object sender, EventArgs e)
        {
            var isConfirm = await DisplayAlert("Pemberitahuan", "Apa anda yakin ingin mengambil peta ke kantor BBSDLP?", "Ya", "Tidak");
            if (isConfirm)
            {
                try
                {
                    // show the loading page...
                    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                    var header = new HeaderTransaction
                    {
                        IdTransaction = headerTransaction.IdTransaction,
                        IdCustomer = headerTransaction.IdCustomer,
                        IdAttachment = headerTransaction.IdAttachment,
                        TransactionNumber = headerTransaction.TransactionNumber,
                        TransactionDate = headerTransaction.TransactionDate,
                        TransactionStatus = headerTransaction.TransactionStatus,
                        PaymentMethode = headerTransaction.PaymentMethode,
                        PaymentTotal = headerTransaction.PaymentTotal,
                        CreatedDate = headerTransaction.CreatedDate,
                        CreatedBy = headerTransaction.CreatedBy,
                        UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                        UpdatedBy = Config.CurrentUserName,
                        OrderAttachmentType = headerTransaction.OrderAttachmentType,
                        OrderAttachmentUrl = headerTransaction.OrderAttachmentUrl,
                        RequestDeliveryStatus = Config.FlowStatus[10],
                        CustomerConfirmation = headerTransaction.CustomerConfirmation
                    };

                    var res = vmheader.UpdateHeaderTransaction(header);

                    if (res != null)
                    {
                        var transLog = new TransactionLog
                        {
                            IdTransaction = headerTransaction.IdTransaction,
                            Message = "Meminta pengambilan peta",
                            Source = Config.CurrentUserName,
                            Time = DatetimeHelper.GetDatetimeNow()
                        };

                        await TransLog.SaveTransLog(transLog);
                        var isCall = await DisplayAlert("Pemberitahuan", "Aksi ini akan melakukan panggilan via telpon", "Ya", "Batal");
                        if (isCall)
                        {
                            MakeCall();

                            var notif = new Notification
                            {
                                Text = $"Permohonan Pengambilan ke kantor BBSDLP. No transaksi {header.TransactionNumber}"
                            };

                            await vmnotif.PushToYanjas(notif);
                            await vmnotif.PushToLia(notif);

                        }
                    }
                    else
                    {
                        await DisplayAlert("Pemberitahuan", "Penyelesaian transaksi gagal", "Ok");
                    }

                    // close the loading page...
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                }
                catch (Exception ex)
                {
                    await Logging.Writelog(ex.Message, "MobileApps_DetailSelesaiPage_BtnAmbil_Clicked");
                    await DisplayAlert("Pemberitahuan", "Penyelesaian transaksi gagal", "Ok");

                    // close the loading page...
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                }
            }
        }

        private async void BtnKurir_Clicked(object sender, EventArgs e)
        {
            var isConfirm = await DisplayAlert("Pemberitahuan", "Apa anda yakin ingin memproses pengiriman peta?", "Ya", "Tidak");
            if (isConfirm)
            {
                try
                {
                    // show the loading page...
                    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                    var header = new HeaderTransaction
                    {
                        IdTransaction = headerTransaction.IdTransaction,
                        IdCustomer = headerTransaction.IdCustomer,
                        IdAttachment = headerTransaction.IdAttachment,
                        TransactionNumber = headerTransaction.TransactionNumber,
                        TransactionDate = headerTransaction.TransactionDate,
                        TransactionStatus = headerTransaction.TransactionStatus,
                        PaymentMethode = headerTransaction.PaymentMethode,
                        PaymentTotal = headerTransaction.PaymentTotal,
                        CreatedDate = headerTransaction.CreatedDate,
                        CreatedBy = headerTransaction.CreatedBy,
                        UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                        UpdatedBy = Config.CurrentUserName,
                        OrderAttachmentType = headerTransaction.OrderAttachmentType,
                        OrderAttachmentUrl = headerTransaction.OrderAttachmentUrl,
                        RequestDeliveryStatus = Config.FlowStatus[9],
                        CustomerConfirmation = headerTransaction.CustomerConfirmation
                    };

                    var res = vmheader.UpdateHeaderTransaction(header);

                    if (res != null)
                    {
                        var transLog = new TransactionLog
                        {
                            IdTransaction = headerTransaction.IdTransaction,
                            Message = "Meminta pengiriman peta",
                            Source = Config.CurrentUserName,
                            Time = DatetimeHelper.GetDatetimeNow()
                        };

                        await TransLog.SaveTransLog(transLog);
                        var isCall = await DisplayAlert("Pemberitahuan", "Aksi ini akan melakukan panggilan via telpon", "Ya", "Batal");
                        if (isCall)
                        {
                            MakeCall();

                            var notif = new Notification
                            {
                                Text = $"Permohonan Pengiriman ke alamat customer. No transaksi {header.TransactionNumber}"
                            };

                            await vmnotif.PushToYanjas(notif);
                            await vmnotif.PushToLia(notif);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Pemberitahuan", "Penyelesaian transaksi gagal", "Ok");
                    }

                    // close the loading page...
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                }
                catch (Exception ex)
                {
                    await Logging.Writelog(ex.Message, "MobileApps_DetailSelesaiPage_BtnKurir_Clicked");
                    await DisplayAlert("Pemberitahuan", "Penyelesaian transaksi gagal", "Ok");

                    // close the loading page...
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                }
            }
        }


        private async void BtnPesananSelesai_Clicked(object sender, EventArgs e)
        {
            var isConfirm = await DisplayAlert("Pemberitahuan", "Pastikan peta sudah diterima di email anda", "Konfirmasi", "Cek Lagi");
            if (isConfirm)
            {
                try
                {
                    // show the loading page...
                    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                    var header = new HeaderTransaction
                    {
                        IdTransaction = headerTransaction.IdTransaction,
                        IdCustomer = headerTransaction.IdCustomer,
                        IdAttachment = headerTransaction.IdAttachment,
                        TransactionNumber = headerTransaction.TransactionNumber,
                        TransactionDate = headerTransaction.TransactionDate,
                        TransactionStatus = headerTransaction.TransactionStatus,
                        PaymentMethode = headerTransaction.PaymentMethode,
                        PaymentTotal = headerTransaction.PaymentTotal,
                        CreatedDate = headerTransaction.CreatedDate,
                        CreatedBy = headerTransaction.CreatedBy,
                        UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                        UpdatedBy = Config.CurrentUserName,
                        OrderAttachmentType = headerTransaction.OrderAttachmentType,
                        OrderAttachmentUrl = headerTransaction.OrderAttachmentUrl,
                        RequestDeliveryStatus = headerTransaction.RequestDeliveryStatus,
                        CustomerConfirmation = Config.FlowStatus[13]
                    };

                    var res = vmheader.UpdateHeaderTransaction(header);

                    if (res != null)
                    {
                        var transLog = new TransactionLog
                        {
                            IdTransaction = headerTransaction.IdTransaction,
                            Message = "Penyelesaian transaksi telah dikonfirmasi, terimakasih",
                            Source = Config.CurrentUserName,
                            Time = DatetimeHelper.GetDatetimeNow()
                        };

                        await TransLog.SaveTransLog(transLog);

                        var notif = new Notification
                        {
                            Text = $"Penyelesaian transaksi telah dikonfirmasi. No transaksi {header.TransactionNumber}"
                        };

                        await vmnotif.PushToYanjas(notif);

                        await DisplayAlert("Pemberitahuan", "Anda telah mengkonfirmasi penyelesaian transaksi, terimakasih", "OK");
                        App.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Pemberitahuan", "Penyelesaian transaksi gagal", "Ok");
                    }

                    // close the loading page...
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                }
                catch (Exception ex)
                {
                    await Logging.Writelog(ex.Message, "MobileApps_DetailSelesaiPage_BtnPesananSelesai_Clicked");
                    await DisplayAlert("Pemberitahuan", "Penyelesaian transaksi gagal", "Ok");

                    // close the loading page...
                    DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                }
            }
        }
        #endregion
    }
}