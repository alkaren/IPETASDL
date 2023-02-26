using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class PembayaranPage : ContentPage
    {
        HeaderTransactionViewModel vm;
        NotificationsViewModel vmnotif;

        private byte[] _image;
        private string filename;
        HeaderTransaction headerTransaction;
        int idAttachment;
        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        private MediaFile _foto;
        public PembayaranPage(HeaderTransaction param)
        {
            vm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            vmnotif = Locator.Instance.Resolve(typeof(NotificationsViewModel)) as NotificationsViewModel;
            headerTransaction = param;
            InitializeComponent();
            TransNoPembayaranPage.Text = headerTransaction.TransactionNumber;
            txtknfrm.Text = $"transaksi {headerTransaction.TransactionNumber} telah kami terima.";
        }
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnAmbil_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            var foto = await CrossMedia.Current
                .PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions());

            _foto = foto;
            ImgPayment.Source = FileImageSource.FromFile(foto.Path);
            ///////Compress/////
            var resizeFile = DependencyService.Get<IMediaService>().ResizeImage(foto.Path, 500, 500);
            if (resizeFile.Length > 0)
            {
                ImgPayment.Source = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(resizeFile);
                });
            }
            else
            {
                ImgPayment.Source = foto.Path;
            }
            BtnUpload.IsVisible = true;
            _image = File.ReadAllBytes(foto.Path);
        }

        private async Task<PaymentAttachment> SaveAttachment()
        {
            string url = $"{Config.BlobUrl}{filename}";

            var content = new PaymentAttachment
            {
                Type = "image",
                Url = url,
                CreatedDate = DatetimeHelper.GetDatetimeNow(),
                CreatedBy = Config.CurrentUserName,
                UpdatedBy = Config.CurrentUserName,
                UpdatedDate = DatetimeHelper.GetDatetimeNow()
            };

            var res = await Data.SaveAttachment(content);

            if (res != null)
            {
                idAttachment = res.IdAttachment;
                return res;
            }

            return null;
        }

        private async void OnUpload_Clicked(object sender, EventArgs e)
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            string date = DatetimeHelper.GetDatetimeNow().ToString("ddMMyyyyhhmmss");
            filename = $"payment{date}{headerTransaction.TransactionNumber}.jpg";

            var res = await BlobsService.ProcessAsync(_image, filename);
            if (res)
            {
                await DisplayAlert("pemberitahuan", "berhasil upload gambar", "OK");
                await SaveAttachment();
                BtnKonfirmasi.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("pemberitahuan", "gagal upload gambar", "Coba lagi");
            }

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }
        private async void OnKonfirmasi_Clicked(object sender, EventArgs e)
        {
            var content = new HeaderTransaction
            {
                IdTransaction = headerTransaction.IdTransaction,
                IdCustomer = headerTransaction.IdCustomer,
                IdAttachment = idAttachment,
                TransactionNumber = headerTransaction.TransactionNumber,
                TransactionDate = DatetimeHelper.GetDatetimeNow(),
                TransactionStatus = Config.FlowStatus[5],
                PaymentMethode = headerTransaction.PaymentMethode,
                PaymentTotal = headerTransaction.PaymentTotal,
                CreatedDate = headerTransaction.CreatedDate,
                CreatedBy = headerTransaction.CreatedBy,
                UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                UpdatedBy = Config.CurrentUserName

            };

            var res = await vm.UpdateHeaderTransaction(content);

            if (res)
            {
                StkPembayaran.IsVisible = false;
                StkLanjutkan.IsVisible = true;

                var transLog = new TransactionLog
                {
                    IdTransaction = headerTransaction.IdTransaction,
                    Message = "transaksi telah dibayar, menungugu konfirmasi",
                    Source = Config.CurrentUserName,
                    Time = DatetimeHelper.GetDatetimeNow()
                };

                await TransLog.SaveTransLog(transLog);                
            }
        }
        private async void OnLanjutkan_Clicked(object sender, EventArgs e)
        {
            var notif = new Notification
            {
                Text = $"Pembayaran dilakukan, mohon untuk dicek"
            };

            await vmnotif.PushToBudi(notif);

            App.Current.MainPage = new NavigationPage(new MainPage());
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
    }
}