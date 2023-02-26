using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LALAPATAPA.Models;
using LALAPATAPA.Helpers;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;

namespace LALAPATAPA.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpBatalPage
    {
        HeaderTransactionViewModel vm;
        HeaderTransaction dataHeader;
        public PopUpBatalPage(HeaderTransaction header)
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
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
                TransactionStatus = Config.FlowStatus[8],
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
                    Message = "transaksi dibatalkan",
                    Source = Config.CurrentUserName,
                    Time = DatetimeHelper.GetDatetimeNow()
                };

                await TransLog.SaveTransLog(transLog);
            }

            PopupNavigation.PopAsync();
            PopupNavigation.PushAsync(new PopUpDibatalkan());

            App.Current.MainPage = new NavigationPage(new MainPage());
        }
        private void OnTidak_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}