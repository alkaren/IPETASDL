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
    public partial class PopUpLIAPage
    {
        NotificationsViewModel vmnotif;
        HeaderTransactionViewModel vmtrans;
        HeaderTransaction dataheader;
        public PopUpLIAPage(HeaderTransaction datas)
        {
            vmtrans = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;
            InitializeComponent();
            dataheader = datas;  
        }
        private async void OnLanjutkan_Clicked(object sender, EventArgs e)
        {            
            try
            {
                var content = new HeaderTransaction
                {
                    IdTransaction = dataheader.IdTransaction,
                    IdCustomer = dataheader.IdCustomer,
                    IdAttachment = dataheader.IdAttachment,
                    TransactionNumber = dataheader.TransactionNumber,
                    TransactionDate = DatetimeHelper.GetDatetimeNow(),
                    TransactionStatus = Config.FlowStatus[1],
                    PaymentMethode = dataheader.PaymentMethode,
                    PaymentTotal = dataheader.PaymentTotal,
                    CreatedDate = dataheader.CreatedDate,
                    CreatedBy = dataheader.CreatedBy,
                    UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                    UpdatedBy = Config.CurrentUserName,
                    OrderAttachmentType = dataheader.OrderAttachmentType,
                    OrderAttachmentUrl = dataheader.OrderAttachmentUrl

                };

                var res = await vmtrans.UpdateHeaderTransaction(content);
                if (res)
                {
                    var transLog = new TransactionLog
                    {
                        IdTransaction = dataheader.IdTransaction,
                        Message = "Pesanan di cek ketersediaan oleh Lia",
                        Source = Config.CurrentUserName,
                        Time = DatetimeHelper.GetDatetimeNow()
                    };

                    await TransLog.SaveTransLog(transLog);
                    var notif = new Notification
                    {
                        Text = "Pesanan Baru Di Setujui"
                    };

                    await vmnotif.PushToLia(notif);
                }
                await PopupNavigation.PopAsync();
                App.Current.MainPage = new NavigationPage(new MenuPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("error", ex.ToString(), "ok");
            }
        }
    }
}