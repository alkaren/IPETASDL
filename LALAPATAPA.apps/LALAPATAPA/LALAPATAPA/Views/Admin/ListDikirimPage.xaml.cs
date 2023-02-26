using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALAPATAPA.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListDikirimPage : ContentPage
    {
        HeaderTransactionViewModel vm;
        NotificationsViewModel vmnotif;
        public string TransactionNumber { get; set; }
        public string TranscasTransactionStatus { get; set; }
        public DateTime? createdDate { get; set; }
        HeaderTransaction dataHeader;
        public ListDikirimPage(HeaderTransaction header)
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;
            InitializeComponent();
            BindingContext = new IRefreshPull(this);
            dataHeader = header;
            //Device.BeginInvokeOnMainThread(LoadData);
            Configuration();
        }
        public async void Configuration()
        {
            if(Config.CurrentUserName == "yanjas")
            {
                var content = await vm.LoadAllHeaderTransaction();
                var TransBaru = from x in content
                                where x.RequestDeliveryStatus == "DIKIRIM VIA KURIR" || x.TransactionStatus == "TELAH DIKIRIM"
                                select x;
                selesailist.ItemsSource = TransBaru;
            }
            else
            {
                var content = await vm.LoadAllHeaderTransaction();
                var TransBaru = from x in content
                                where x.RequestDeliveryStatus == "DIKIRIM VIA KURIR" || x.TransactionStatus == "TELAH DIKIRIM"
                                select x;
                selesailist.ItemsSource = TransBaru;
                selesailist.IsEnabled = false;
            }
        }
        public async void UpdateKirim(HeaderTransaction mylist)
        {
            try
            {
                var trans = await Data.LoadTrAttachment(mylist.IdTransaction);
                var content = new HeaderTransaction
                {
                    IdTransaction = trans.IdTransaction,
                    IdCustomer = trans.IdCustomer,
                    IdAttachment = trans.IdAttachment,
                    TransactionNumber = trans.TransactionNumber,
                    TransactionDate = trans.TransactionDate,
                    TransactionStatus = trans.TransactionStatus,
                    PaymentMethode = trans.PaymentMethode,
                    PaymentTotal = trans.PaymentTotal,
                    CreatedDate = trans.CreatedDate,
                    CreatedBy = trans.CreatedBy,
                    UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                    UpdatedBy = Config.CurrentUserName,
                    OrderAttachmentType = trans.OrderAttachmentType,
                    OrderAttachmentUrl = trans.OrderAttachmentUrl,
                    RequestDeliveryStatus = Config.FlowStatus[12],
                    CustomerConfirmation = trans.CustomerConfirmation
                };
                var res = await vm.UpdateHeaderTransaction(content);
                if (res)
                {
                    var transLog = new TransactionLog
                    {
                        IdTransaction = trans.IdTransaction,
                        Message = "Pesanan telah dikirim via kurir",
                        Source = Config.CurrentUserName,
                        Time = DatetimeHelper.GetDatetimeNow()
                    };

                    await TransLog.SaveTransLog(transLog);
                    var notif = new Notification
                    {
                        Text = "Pesanan telah dikirim via kurir"
                    };

                    await vmnotif.PushToCus(notif, trans.IdTransaction);
                    var notif2 = new Notification
                    {
                        Text = "Pesanan telah diambil di lokasi"
                    };

                    await vmnotif.PushToLia(notif2);
                }
                App.Current.MainPage = new NavigationPage(new MenuPage());
            }
            catch(Exception er)
            {
                await DisplayAlert("Peringatan", "Terjadi Kesalahan", "Ok");
            }            
        }
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnTappedPesanan(object sender, ItemTappedEventArgs e)
        {
            if (Config.CurrentUserName == "yanjas")
            {
                var mylist = e.Item as HeaderTransaction;
                var resp = await DisplayAlert("Pemberitahuan", "Apakah Pesanan sudah dikirim via kurir?", "Ya", "Tidak");
                if (resp)
                {
                    UpdateKirim(mylist);
                }
            }
            else
            {
                return;
            }
        }
        private async void OnTappedIconHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MenuPage()));
        }
        private async void OnTappedIconBaru(object sender, EventArgs e)
        {
            var user1 = "Hi, yanjas";
            var user2 = "Hi, lia";
            var user3 = "Hi, budi";
            if (Config.CurrentUserName.Equals(user2))
            {
                await Navigation.PushModalAsync(new NavigationPage(new PesananLIAPage()));
            }
            else if (Config.CurrentUserName.Equals(user1))
            {
                await Navigation.PushModalAsync(new NavigationPage(new DaftarPesananPage()));
            }
            else if (Config.CurrentUserName.Equals(user3))
            {
                await Navigation.PushModalAsync(new NavigationPage(new PesananBUDIPage()));
            }
            else
            {
                await DisplayAlert("Error", "Terjadi Kesalahan", "Ok");
            }
        }
        private async void OnTappedIconSelesai(object sender, EventArgs e)
        {
            var user1 = "Hi, yanjas";
            var user2 = "Hi, lia";
            var user3 = "Hi, budi";
            if (Config.CurrentUserName.Equals(user2))
            {
                await Navigation.PushModalAsync(new NavigationPage(new SelesaiLIAPage()));
            }
            else if (Config.CurrentUserName.Equals(user1))
            {
                await Navigation.PushModalAsync(new NavigationPage(new DaftarSelesaiPage()));
            }
            else if (Config.CurrentUserName.Equals(user3))
            {
                await Navigation.PushModalAsync(new NavigationPage(new SelesaiBUDIPage()));
            }
            else
            {
                await DisplayAlert("Error", "Terjadi Kesalahan", "Ok");
            }
        }
        private async void OnTappedIconProses(object sender, EventArgs e)
        {
            var user1 = "Hi, yanjas";
            var user2 = "Hi, lia";
            var user3 = "Hi, budi";
            if (Config.CurrentUserName.Equals(user2))
            {
                await Navigation.PushModalAsync(new NavigationPage(new ProsesLIAPage()));
            }
            else if (Config.CurrentUserName.Equals(user1))
            {
                await Navigation.PushModalAsync(new NavigationPage(new DaftarProsesPage()));
            }
            else if (Config.CurrentUserName.Equals(user3))
            {
                await Navigation.PushModalAsync(new NavigationPage(new ProsesBUDIPage()));
            }
            else
            {
                await DisplayAlert("Error", "Terjadi Kesalahan", "Ok");
            }
        }
        private async void OnTappedIconAkun(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AkunAdminPage()));
        }
    }
}