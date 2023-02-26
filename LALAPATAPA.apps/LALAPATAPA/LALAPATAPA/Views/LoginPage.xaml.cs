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

namespace LALAPATAPA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vm;
        CustomerViewModel vmcus;
        AccountViewModel vmacc;
        DeviceViewModel vmdevice;
        public LoginPage()
        {
            vm = Locator.Instance.Resolve(typeof(LoginViewModel)) as LoginViewModel;
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            vmacc = Locator.Instance.Resolve(typeof(AccountViewModel)) as AccountViewModel;
            vmdevice = Locator.Instance.Resolve(typeof(DeviceViewModel)) as DeviceViewModel;
            InitializeComponent();            
        }

        private bool IsInputValidate()
        {
            if(Email.Text == null)
            {
                DisplayAlert("Peringatan", "Mohon isi email / username anda", "OK");
                return false;
            }
            else if (Password.Text == null)
            {
                DisplayAlert("Peringatan", "Mohon isi password anda", "OK");
                return false;
            }

            return true;
        }

        private async void DoLogin()
        {
            try
            {
                if (IsInputValidate())
                {
                    // show the loading page...
                    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                    while (Config.NotificationToken == null || Config.NotificationToken.Length <= 0)
                    {
                        // tunggu yaa
                        if (Config.NotificationToken != null)
                            break;
                    }

                    var data = new Account
                    {
                        Username = Email.Text,
                        Password = Password.Text
                    };

                    var res = await vm.Login(data);

                    if (res != null)
                    {
                        // save notif token
                        var device = new Models.Device
                        {
                            IdAccount = res.IdAccount,
                            InstallationId = Config.NotificationToken
                        };

                        var savenotiftoken = await vmdevice.SaveDevice(device);

                        if (savenotiftoken)
                        {
                            // Set config helper
                            Config.CurrentAccountId = res.IdAccount;
                            if (res.IdGroup != 5)
                            {
                                Config.CurrentUserId = res.IdEmployee.Value;
                                Config.CurrentUserName = res.Username;

                                // close the loading page...
                                DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                                App.Current.MainPage = new NavigationPage(new Admin.MenuPage());
                            }
                            else
                            {
                                try
                                {
                                    var customer = await vmcus.LoadCustomerById(res.IdCustomer.Value);

                                    Config.CurrentUserId = res.IdCustomer.Value;
                                    Config.CurrentUserName = customer.Name;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                                }

                                // close the loading page...
                                DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                                App.Current.MainPage = new NavigationPage(new User.MainPage());
                            }
                        }
                        else
                        {
                            // close the loading page...
                            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                            await DisplayAlert("Pemberitahuan", "Terjadi kesalahan pada server", "OK");

                        }
                    }
                    else
                    {
                        // close the loading page...
                        DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                        await DisplayAlert("Pemberitahuan", "Email dan password anda tidak sesuai", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Maaf terjadi kesalahan", "Ok");
                await Logging.Writelog(ex.Message + " " + ex.StackTrace, "MobileApps_LoginPage_DoLogin");
            }
        }

        #region Event Handler
        private async void OnRegistrasi_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new User.RegisterPage());
        }
        private async void OnLupaPassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgotPasswordPage());
        }
        private void OnLogin_Clicked(object sender, EventArgs e)
        {
            DoLogin();
        }
        #endregion
    }
}