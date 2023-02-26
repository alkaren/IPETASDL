using LALAPATAPA.Models;
using LALAPATAPA.Helpers;
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
    public partial class AkunAdminPage : ContentPage
    {
        AccountViewModel vm;
        DeviceViewModel vmdevice;

        Account thisaccount;
        public AkunAdminPage()
        {
            vm = Locator.Instance.Resolve(typeof(AccountViewModel)) as AccountViewModel;
            vmdevice = Locator.Instance.Resolve(typeof(DeviceViewModel)) as DeviceViewModel;
            InitializeComponent();
            Configuration();
        }
        private async void Configuration()
        {
            var content = await vm.LoadAccountById(Config.CurrentAccountId);
            LoadDisplay(content);
            thisaccount = content;
        }
        private void LoadDisplay(Account param)
        {
            try
            {
                Username.Text = param.Username;
                Password.Text = param.Password;
            }
            catch (Exception ez)
            {
                DisplayAlert("ERROR", ez.ToString(), "OK");
            }
        }

        private async void DestroyLogin()
        {
            var deletenotiftoken = await vmdevice.DeleteDevice(Config.NotificationToken);

            Config.CurrentAccountId = -1;
            Config.CurrentEmailUser = "";
            Config.CurrentTransactionId = -1;
            Config.CurrentUserGroup = -1;
            Config.CurrentUserId = -1;
            Config.CurrentUserName = "";
            Config.AccessToken = "";

            App.Current.MainPage = new LoginPage();
        }

        private async Task<bool> Edit(Account param)
        {
            var content = await vm.LoadAccountById(Config.CurrentAccountId);
            var newdata = new Account
            {
                IdAccount = param.IdAccount,
                IdGroup = param.IdGroup,
                Username = param.Username,
                Password = param.Password,
                CreatedDate = content.CreatedDate,
                CreatedBy = content.CreatedBy,
                UpdatedBy = Config.CurrentUserName,
                UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                IdCustomer = param.IdCustomer,
                IdEmployee = param.IdEmployee
            };

            var res = await vm.EditAccount(newdata);

            if (res)
                return true;

            return false;
        }

        #region Event Handler
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var input = new Account
            {
                Username = Username.Text,                
                Password = Password.Text
            };

            var DoEdit = await Edit(input);

            if (DoEdit)
            {
                await DisplayAlert("Pemberitahuan", "Edit account berhasil", "OK");
                await Navigation.PushAsync(new AkunAdminPage());
            }
            else
            {
                await DisplayAlert("Pemberitahuan", "Edit account gagal", "OK");
            }

        }        
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifikasiPage());
        }
        private async void OnLogout_Clicked(object sender, EventArgs e)
        {
            DestroyLogin();
            await Navigation.PushModalAsync(new LoginPage());
        }
        private async void OnTappedIconHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MenuPage()));
        }
        private async void OnTappedIconBaru(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new DaftarPesananPage()));
        }
        private async void OnTappedIconProses(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new DaftarProsesPage()));
        }
        private async void OnTappedIconSelesai(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new DaftarSelesaiPage()));
        }
        private async void OnTappedIconAkun(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AkunAdminPage()));
        }
        #endregion
    }
}
