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
    public partial class AkunPage : ContentPage
    {
        CustomerViewModel vmcus;
        AccountViewModel vmacc;
        DeviceViewModel vmdevice;

        Account thisaccount;
        public AkunPage()
        {
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            vmacc = Locator.Instance.Resolve(typeof(AccountViewModel)) as AccountViewModel;
            vmdevice = Locator.Instance.Resolve(typeof(DeviceViewModel)) as DeviceViewModel;
            InitializeComponent();
            Configuration();
        }

        private async void Configuration()
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            var cuscontent = await vmcus.LoadCustomerById(Config.CurrentUserId);
            var acccontent = await vmacc.LoadAccountById(Config.CurrentAccountId);
            LoadDisplay(cuscontent, acccontent);

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }
        private void LoadDisplay(Customer customer, Account account)
        {
            thisaccount = account;

            txtName.Text = customer.Name;
            txtPhone.Text = customer.Contact;
            txtEmail.Text = customer.Email;
            txtCompany.Text = customer.Company;
            txtIdentityNumber.Text = customer.IdentityNumber;
            txtPassword.Text = account.Password;
        }

        private async Task<bool> Edit()
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            var cuscontent = await vmcus.LoadCustomerById(Config.CurrentUserId);
            var acccontent = await vmacc.LoadAccountById(Config.CurrentAccountId);

            var Username = txtEmail.Text;
            var Password = txtPassword.Text;

            var newcus = new Customer
            {
                IdCustomer = cuscontent.IdCustomer,
                Name = txtName.Text,
                Contact = txtPhone.Text,
                Email = txtEmail.Text,
                Company = txtCompany.Text,
                IdentityNumber = txtIdentityNumber.Text,
                CreatedDate = cuscontent.CreatedDate,
                CreatedBy = cuscontent.CreatedBy,
                UpdatedBy = Config.CurrentUserName,
                UpdatedDate = DatetimeHelper.GetDatetimeNow()
            };

            var newacc = new Account
            {
                IdAccount = acccontent.IdAccount,
                IdGroup = acccontent.IdGroup,
                Username = Username,
                Password = Password,
                CreatedDate = acccontent.CreatedDate,
                CreatedBy = acccontent.CreatedBy,
                UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                UpdatedBy = Config.CurrentUserName,
                IdCustomer = acccontent.IdCustomer,
                IdEmployee = acccontent.IdEmployee
            };

            var updatecus = await vmcus.EditCustomers(newcus);
            var updateacc = await vmacc.EditAccount(newacc);

            if (updatecus && updateacc)
            {
                // close the loading page...
                DependencyService.Get<ILoadingPageService>().HideLoadingPage();
                return true;
            }

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
            return false;
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

        #region Event Handler
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var DoEdit = await Edit();

            if (DoEdit)
            {
                await DisplayAlert("Pemberitahuan", "Edit account berhasil", "OK");
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                await DisplayAlert("Pemberitahuan", "Edit account gagal", "OK");
            }

        }
        private async void OnNotify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NotifikasiPage()));
        }
        private async void OnLogout_Clicked(object sender, EventArgs e)
        {
            var response =  await DisplayAlert("Peringatan", "Apakah anda yakin ingin logout", "Ya", "Tidak");
            if (response)
            {
                DestroyLogin();
                await Navigation.PopToRootAsync();
            }
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
        #endregion
    }
}