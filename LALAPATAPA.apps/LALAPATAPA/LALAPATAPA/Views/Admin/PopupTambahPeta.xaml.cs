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
    public partial class PopupTambahPeta
    {
        HeaderTransactionViewModel vm;
        Product products;
        HeaderTransaction dataHeader;
        NotificationsViewModel vmnotif;

        public PopupTambahPeta(HeaderTransaction datas)
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;

            InitializeComponent();
            //Configuration(datas);
            dataHeader = datas;          
        }

        public async void Configuration(Product param)
        {
            try
            {
                var product = await Data.LoadProductById(param.IdProduct);
            }
            catch(Exception er)
            {
                await DisplayAlert("error", er.ToString(), "ok");
            }
        }
        private async void OnLanjutkan_Clicked(object sender, EventArgs e)
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
                    TransactionStatus = Config.FlowStatus[2],
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
                        Message = "Peta tersedia, dilanjutkan ke Pemesan",
                        Source = Config.CurrentUserName,
                        Time = DatetimeHelper.GetDatetimeNow()
                    };

                    await TransLog.SaveTransLog(transLog);


                    var notif = new Notification
                    {
                        Text = "Pesanan Baru Tersedia"
                    };

                    await vmnotif.PushToCus(notif, dataHeader.IdTransaction);

                }
                PopupNavigation.PopAsync();
                App.Current.MainPage = new NavigationPage(new MenuPage());
            }
            catch (Exception er)
            {
                DisplayAlert("error", er.ToString(), "ok");
            }
        }
    }
}