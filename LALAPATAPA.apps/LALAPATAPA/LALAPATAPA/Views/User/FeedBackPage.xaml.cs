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
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;

namespace LALAPATAPA.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedBackPage : ContentPage
    {
        FeedbackViewModel vmfed;
        public FeedBackPage()
        {
            vmfed = Locator.Instance.Resolve(typeof(FeedbackViewModel)) as FeedbackViewModel;
            InitializeComponent();
        }
        private async void Kirim_Clicked(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Pemberitahuan", "Kirim komentar?", "Ya", "Batal");

            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            if (response)
            {
                var feedback = new Feedback
                {
                    IdAccount = Config.CurrentAccountId,
                    Message = txtPesan.Text,
                    Source = "Customer",
                    Time = DatetimeHelper.GetDatetimeNow()
                };

                await vmfed.SaveComment(feedback);

                txtPesan.Text = "";
                // close the loading page...
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                await DisplayAlert("Pemberitahuan", "Feedback terkirim", "Ok");
            }
            else
            {
                // close the loading page...
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                await DisplayAlert("Pemberitahuan", "Terjadi kesalahan", "Mengerti");
            }
        }
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
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