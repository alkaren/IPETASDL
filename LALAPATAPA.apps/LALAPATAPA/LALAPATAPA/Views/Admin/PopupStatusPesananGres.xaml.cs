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
    public partial class PopupStatusPesananGres
    {
        HeaderTransactionViewModel vm;
        HeaderTransaction dataheader;
        public PopupStatusPesananGres(HeaderTransaction datas)
        {
            InitializeComponent();
            dataheader = datas;
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
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
                    TransactionStatus = Config.FlowStatus[2],
                    PaymentMethode = dataheader.PaymentMethode,
                    PaymentTotal = dataheader.PaymentTotal,
                    CreatedDate = dataheader.CreatedDate,
                    CreatedBy = dataheader.CreatedBy,
                    UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                    UpdatedBy = Config.CurrentUserName

                };

                var res = await vm.UpdateHeaderTransaction(content);

                if (res)
                {
                    var transLog = new TransactionLog
                    {
                        IdTransaction = dataheader.IdTransaction,
                        Message = "Peta tersedia",
                        Source = Config.CurrentUserName,
                        Time = DatetimeHelper.GetDatetimeNow()
                    };

                    await TransLog.SaveTransLog(transLog);
                }
                PopupNavigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("error", "test", "ok");
            }
        }
    }
}
