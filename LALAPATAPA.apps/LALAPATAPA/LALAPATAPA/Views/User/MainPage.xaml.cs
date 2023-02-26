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
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        LoadDataViewModel datavm;
        public MainPage()
        {
            datavm = Locator.Instance.Resolve(typeof(LoadDataViewModel)) as LoadDataViewModel;
            InitializeComponent();
            Configuration();
        }
        //protected override void OnAppearing()
        //{
        //    DependencyService.Get<ILoadingPageService>().InitLoadingPage(new Views.LoadingIndicatorPage());
        //    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();
        //    Task.Delay(2000);
        //    base.OnAppearing();
        //    Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        //    {
        //        DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        //        return false;
        //    });
        //}

        private void Configuration()
        {
            // show the loading page...
            //DependencyService.Get<ILoadingPageService>().InitLoadingPage(new LoadingIndicatorPage());
            //DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            CurrentUser.Text = $"Hi, {Config.CurrentUserName}";
            LoadIcon();

            // close the loading page...
            //DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }

        private async void LoadIcon()
        {
            var procesi = await datavm.CusLoadAllProsesTransaction();
            var selesaii = await datavm.CusLoadAllSelesaiTransaction();
            icoProses.Text = procesi != null ? procesi.Count().ToString() : "0";
            icoSelesai.Text = selesaii != null ? selesaii.Count().ToString() : "0";
        }

        #region Event Handler
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnTappedFeedBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new FeedBackPage()));
        }
        private async void OnTappedBaru(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PesananBaruPage()));
        }
        private async void OnTappedSelesai(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SelesaiPage()));
        }
        private async void OnTappedProses(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProsesPage()));
        }
        private async void OnTappedAkun(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AkunPage()));
        }
        #endregion
    }
}
