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
    public partial class RincianPesananBUDIPage : ContentPage
    {
        NotificationsViewModel vmnotif;
        TimeZoneInfo timeZoneInfo;
        DateTime dateTime;
        DetailTransactionViewModel vmdetail;
        CustomerViewModel vmcus;
        HeaderTransactionViewModel vmtrans;
        HeaderTransaction headerTransaction;
        BankAccount bank;
        public RincianPesananBUDIPage(HeaderTransaction datas)
        {
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            vmtrans = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            InitializeComponent();
            frmHarga.HeightRequest = 180;
            GetRekening();
            Configuration(datas);
            headerTransaction = datas;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT+07:00");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            txtStart.Text = dateTime.ToString("dd MMM yyyy");
            DateTime nextMonday = dateTime.AddDays(7);
            txtExpired.Text = nextMonday.ToString("dd MMM yyyy");
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
        public bool Validasi()
        {
            if(txtNoRek.Text == "" || txtNoRek.Text == null)
            {
                DisplayAlert("Peringatan", "Harap Isi No. E-Billing", "Ok");
                return false;
            }
            else if (txtAtasNama.Text == "" || txtAtasNama.Text == null)
            {
                DisplayAlert("Peringatan", "Harap Isi Atas Nama", "Ok");
                return false;
            }
            return true;
        }
        //private void OnIsi_Clicked(object sender, EventArgs e)
        //{
        //    lblBank.Text = "BCA";
        //    lblBank.TextColor = Color.FromHex("#23A761");
        //    lblNoRek.Text = "98237489374";
        //    lblNoRek.TextColor = Color.FromHex("#23A761");
        //    lblAtasNama.Text = "LALAPATAPA";
        //    lblAtasNama.TextColor = Color.FromHex("#23A761");
        //    BtnLanjut.IsVisible = true;
        //    BtnIsi.IsVisible = false;
        //    txtBank.Placeholder = "BCA";
        //    txtNoRek.Placeholder = "98237489374";
        //    txtAtasNama.Placeholder = "LALAPATAPA";
        //    txtBank.IsEnabled = false;
        //    txtNoRek.IsEnabled = false;
        //    txtAtasNama.IsEnabled = false;
        //}        
        #region saveREKlocal
        public void GetRekening()
        {
            if (Application.Current.Properties.ContainsKey("AN"))
            {
                txtBank.Text = Application.Current.Properties["Bank"].ToString();
                txtNoRek.Text = Application.Current.Properties["NoRek"].ToString();
                txtAtasNama.Text = Application.Current.Properties["AN"].ToString();
            }
        }
        public void StoreRekening()
        {
            Application.Current.Properties["Bank"] = txtBank.Text;
            Application.Current.Properties["NoRek"] = txtNoRek.Text;
            Application.Current.Properties["AN"] = txtAtasNama.Text;
            txtBank.Text = string.Empty;
            txtNoRek.Text = string.Empty;
            txtAtasNama.Text = string.Empty;
            DisplayAlert("Success", "Nomor Rekening Telah Tersimpan", "OK");
        }
        #endregion
        //private async Task<bool> Input(BankAccount param)
        //{
        //    //var content = await Data.LoadEmployeeById(Config.CurrentUserId);
        //    var newdata = new BankAccount
        //    {
        //        //IdBankAccount = content
        //        BankName = param.BankName,
        //        AccountOwner = param.AccountOwner,
        //        AccountNo = param.AccountNo                                 
        //    };

        //    var res = await Data.InputRekening(newdata);

        //    if (res != null)
        //        return true;            

        //    return false;
        //}
        #region button
        private async void OnLanjutkan_Clicked(object sender, EventArgs e)
        {
            if (Validasi())
            {
                try
                {
                    var input = new BankAccount
                    {
                        BankName = txtBank.Text,
                        AccountNo = txtNoRek.Text,
                        AccountOwner = txtAtasNama.Text,
                        ValidStart = DateTime.Parse(txtStart.Text),
                        ValidUntil = DateTime.Parse(txtExpired.Text)
                    };

                    var DoEdit = await Data.InputRekening(input);

                    if (DoEdit != null)
                    {
                        var detail = await vmdetail.LoadDetailTransactionByTransId(headerTransaction.IdTransaction);
                        var val = await Data.LoadProductById(detail.IdDetail);
                        var data = new DetailTransaction
                        {
                            IdDetail = detail.IdDetail,
                            IdTransaction = detail.IdTransaction,
                            IdProduct = detail.IdProduct,
                            Quantity = detail.Quantity,
                            TotalPrice = detail.TotalPrice,
                            Discount = detail.Discount,
                            TransactionDescription = "SUDAH DIBAYAR",
                            CreatedDate = detail.CreatedDate,
                            CreatedBy = detail.CreatedBy,
                            UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                            UpdatedBy = Config.CurrentUserName,
                            IdBankAccount = DoEdit.IdBankAccount,
                            ProcurementPurpose = detail.ProcurementPurpose
                        };
                        var res2 = await vmdetail.UpdateDetailTransaction(data);

                        //var ban = await Data.InputRekening(input);
                        if (res2)
                        {
                            var content = new HeaderTransaction
                            {
                                IdTransaction = headerTransaction.IdTransaction,
                                IdCustomer = headerTransaction.IdCustomer,
                                IdAttachment = headerTransaction.IdAttachment,
                                TransactionNumber = headerTransaction.TransactionNumber,
                                TransactionDate = DatetimeHelper.GetDatetimeNow(),
                                TransactionStatus = Config.FlowStatus[4],
                                PaymentMethode = "TRANSFER VIA BANK",
                                PaymentTotal = headerTransaction.PaymentTotal,
                                CreatedDate = headerTransaction.CreatedDate,
                                CreatedBy = headerTransaction.CreatedBy,
                                UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                                UpdatedBy = Config.CurrentUserName,
                                OrderAttachmentType = headerTransaction.OrderAttachmentType,
                                OrderAttachmentUrl = headerTransaction.OrderAttachmentUrl

                            };
                            var res = await vmtrans.UpdateHeaderTransaction(content);
                            if (res)
                            {
                                var transLog = new TransactionLog
                                {
                                    IdTransaction = headerTransaction.IdTransaction,
                                    Message = "E-Billing telah dikirim ke pemesan, menunggu pembayaran",
                                    Source = Config.CurrentUserName,
                                    Time = DatetimeHelper.GetDatetimeNow()
                                };

                                await TransLog.SaveTransLog(transLog);
                                var notif = new Notification
                                {
                                    Text = "Segera melakukan pembayaran"
                                };

                                await vmnotif.PushToCus(notif, headerTransaction.IdTransaction);
                            }
                            await DisplayAlert("Pemberitahuan", "Data berhasil disimpan", "OK");
                        }
                        App.Current.MainPage = new NavigationPage(new MenuPage());
                    }
                    else
                    {
                        await DisplayAlert("Pemberitahuan", "Data gagal disimpan", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("error", ex.ToString(), "ok");
                }
            }
        }
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