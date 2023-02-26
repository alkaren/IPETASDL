using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;
using Rg.Plugins.Popup.Services;

namespace LALAPATAPA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        AccountViewModel accvm;
        public ForgotPasswordPage()
        {
            accvm = Locator.Instance.Resolve(typeof(AccountViewModel)) as AccountViewModel;
            InitializeComponent();
        }
        private async void OnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void OnPulihkan_Clicked(object sender, EventArgs e)
        {
            try
            {
                // show the loading page...
                DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                var data = await accvm.FindAccountByEmail(txtEmailUser.Text);

                if(data != null)
                {
                    //var res = await accvm.ForgotPasswordAccount(data);
                    var res = await Data.TryForgotPassword(data);

                    if (res)
                    {
                        // close the loading page...
                        DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                        PopupNavigation.PushAsync(new PopUpView());
                    }
                    else
                    {
                        await  DisplayAlert("Pemberitahuan", "Terjadi kesalahan pada server", "Mengerti");
                    }
                }
                else
                {
                    await DisplayAlert("Pemberitahuan", "Email tidak terdaftar", "Mengerti");
                }

                // close the loading page...
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
            }
            catch (Exception ex)
            {
                // close the loading page...
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
            }
        }
    }
}