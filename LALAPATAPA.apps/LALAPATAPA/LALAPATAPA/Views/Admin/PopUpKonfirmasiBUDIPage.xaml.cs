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
    public partial class PopUpKonfirmasiBUDIPage
    {
        NotificationsViewModel vmnotif;
        HeaderTransactionViewModel vmtrans;
        DetailTransactionViewModel vmdetail;
        HeaderTransaction dataHeader;
        public PopUpKonfirmasiBUDIPage(HeaderTransaction header)
        {
            InitializeComponent();
            dataHeader = header;
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;
            vmtrans = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            vmdetail = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;
        }
        private async void OnIya_Clicked(object sender, EventArgs e)
        {
            var content = new HeaderTransaction
            {
                IdTransaction = dataHeader.IdTransaction,
                IdCustomer = dataHeader.IdCustomer,
                IdAttachment = dataHeader.IdAttachment,
                TransactionNumber = dataHeader.TransactionNumber,
                TransactionDate = dataHeader.TransactionDate,
                TransactionStatus = Config.FlowStatus[6],
                PaymentMethode = "transfer via bank",
                PaymentTotal = dataHeader.PaymentTotal,
                CreatedDate = dataHeader.CreatedDate,
                CreatedBy = dataHeader.CreatedBy,
                UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                UpdatedBy = Config.CurrentUserName,
                OrderAttachmentType = dataHeader.OrderAttachmentType,
                OrderAttachmentUrl = dataHeader.OrderAttachmentUrl
            };

            var res = await vmtrans.UpdateHeaderTransaction(content);
            if (res)
            {
                var transLog = new TransactionLog
                {
                    IdTransaction = dataHeader.IdTransaction,
                    Message = "Transaksi Selesai",
                    Source = Config.CurrentUserName,
                    Time = DatetimeHelper.GetDatetimeNow()
                };

                await TransLog.SaveTransLog(transLog);
            }

            var detail = await vmdetail.LoadDetailTransactionByTransId(dataHeader.IdTransaction);
            var con = new DetailTransaction
            {
                IdDetail = detail.IdDetail,
                IdTransaction = detail.IdTransaction,
                IdProduct = detail.IdProduct,
                Quantity = detail.Quantity,
                TotalPrice = detail.TotalPrice,
                Discount = detail.Discount,
                TransactionDescription = "sudah dibayar",
                CreatedDate = detail.CreatedDate,
                CreatedBy = detail.CreatedBy,
                UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                UpdatedBy = Config.CurrentUserName,
                IdBankAccount = detail.IdBankAccount,
                ProcurementPurpose = detail.ProcurementPurpose
            };
            var res2 = await vmdetail.UpdateDetailTransaction(con);
            if (res2)
            {
                var DetailLog = new Log
                {
                    Message = "Update Detail, sudah dibayar",
                    Source = Config.CurrentUserName
                };
                await Logging.Writelog(DetailLog.Message, DetailLog.Source);
                var notif = new Notification
                {
                    Text = "Pembayaran Telah Di Validasi"
                };
                var notif2 = new Notification
                {
                    Text = "Pesanan Sudah Selesai"
                };
                var notif3 = new Notification
                {
                    Text = "Pesanan Sudah Selesai"
                };

                await vmnotif.PushToCus(notif, dataHeader.IdTransaction);
                await vmnotif.PushToYanjas(notif2);
                await vmnotif.PushToLia(notif3);
            }

            PopupNavigation.PopAsync();
            PopupNavigation.PushAsync(new PopUpSudahBayarBUDIPage());
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiBUDIPage()));
        }
        private void OnTidak_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}