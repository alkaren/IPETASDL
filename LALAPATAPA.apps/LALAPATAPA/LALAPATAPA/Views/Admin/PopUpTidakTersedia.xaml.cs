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
    public partial class PopUpTidakTersedia
    {
        HeaderTransactionViewModel vm;
        HeaderTransaction dataHeader;
        public PopUpTidakTersedia(HeaderTransaction datas)
        {
            InitializeComponent();
            dataHeader = datas;
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
        }
        private async void OnIya_Clicked(object sender, EventArgs e)
        {
            try
            {
                var content = new HeaderTransaction
                {
                    IdTransaction = dataHeader.IdTransaction,
                    IdCustomer = dataHeader.IdCustomer,
                    IdAttachment = dataHeader.IdAttachment,
                    TransactionNumber = dataHeader.TransactionNumber,
                    TransactionDate = DatetimeHelper.GetDatetimeNow(),
                    TransactionStatus = Config.FlowStatus[7],
                    PaymentMethode = dataHeader.PaymentMethode,
                    PaymentTotal = dataHeader.PaymentTotal,
                    CreatedDate = dataHeader.CreatedDate,
                    CreatedBy = dataHeader.CreatedBy,
                    UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                    UpdatedBy = Config.CurrentUserName,
                    OrderAttachmentType = dataHeader.OrderAttachmentType,
                    OrderAttachmentUrl = dataHeader.OrderAttachmentUrl
                };
                var res = await vm.UpdateHeaderTransaction(content);

                if (res)
                {
                    var transLog = new TransactionLog
                    {
                        IdTransaction = dataHeader.IdTransaction,
                        Message = "Peta tidak tersedia",
                        Source = Config.CurrentUserName,
                        Time = DatetimeHelper.GetDatetimeNow()
                    };

                    await TransLog.SaveTransLog(transLog);                    
                }
                PopupNavigation.PopAsync();
                App.Current.MainPage = new NavigationPage(new MenuPage());
            }
            catch (Exception er)
            {
                await DisplayAlert("error", er.ToString(), "ok");
            }
            PopupNavigation.PopAsync();
            PopupNavigation.PushAsync(new PopUpStatusTidakTersedia());
            await Navigation.PushModalAsync(new NavigationPage(new PesananLIAPage()));
        }
        private void OnTidak_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}