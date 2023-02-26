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
    public partial class PopUpSetujuPage
    {
        HeaderTransactionViewModel vm;
        NotificationsViewModel vmnotif;
        HeaderTransaction dataHeader;
        public PopUpSetujuPage(HeaderTransaction header)
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;
            InitializeComponent();
            dataHeader = header;
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
                TransactionStatus = Config.FlowStatus[3],
                PaymentMethode = dataHeader.PaymentMethode,
                PaymentTotal = dataHeader.PaymentTotal,
                CreatedDate = dataHeader.CreatedDate,
                CreatedBy = dataHeader.CreatedBy,
                UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                UpdatedBy = Config.CurrentUserName

            };

            var res = await vm.UpdateHeaderTransaction(content);

            if (res)
            {
                var transLog = new TransactionLog
                {
                    IdTransaction = dataHeader.IdTransaction,
                    Message = $"harga disetujui, melanjutkan ke pembayaran",
                    Source = Config.CurrentUserName,
                    Time = DatetimeHelper.GetDatetimeNow()
                };

                await TransLog.SaveTransLog(transLog);

                var notif = new Notification
                {
                    Text = $"Pesanan disetujui, harus transfer kemana?. No transaksi {content.TransactionNumber}"
                };

                await vmnotif.PushToBudi(notif);
            }

            PopupNavigation.PopAsync();

            App.Current.MainPage = new NavigationPage(new MainPage());
        }
        private void OnTidak_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}