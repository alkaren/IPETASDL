using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LALAPATAPA.Models;
using LALAPATAPA.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;
using LALAPATAPA.Validations;

namespace LALAPATAPA.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        CustomerViewModel vmcus;
        AccountViewModel vmacc;
        public RegisterPage()
        {
            InitializeComponent();
            vmcus = Locator.Instance.Resolve(typeof(CustomerViewModel)) as CustomerViewModel;
            vmacc = Locator.Instance.Resolve(typeof(AccountViewModel)) as AccountViewModel;
        }

        private bool IsInputValidate()
        {
            if (txtName.Text == null || txtPhone.Text == null || txtPhone.Text == null || txtEmail.Text == null || txtCompany.Text == null || txtIdentity.Text == null || txtPassword.Text == null)
            {
                DisplayAlert("Peringatan", "Silahkan isi semua kolom data", "OK");
                return false;
            }
            else if (!IsEmail(txtEmail.Text))
            {
                DisplayAlert("Peringatan", "Input email salah","OK");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsEmail(string param)
        {
            var content = new EmailAddressAttribute().IsValid(param);
            return content;
        }

        private async void DoDaftar()
        {
            try
            {
                if (IsInputValidate())
                {
                    // show the loading page...
                    DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

                    string Password = txtPassword.Text;
                    string Username = txtEmail.Text;

                    var customer = new Customer
                    {
                        Name = txtName.Text,
                        Contact = txtPhone.Text,
                        Email = txtEmail.Text,
                        Company = txtCompany.Text,
                        IdentityNumber = txtIdentity.Text,
                        CreatedBy = "sistem registrasi",
                        CreatedDate = DatetimeHelper.GetDatetimeNow()
                    };

                    var content = await vmcus.RegisterCustomers(customer);

                    if (content != null)
                    {
                        var account = new Account
                        {
                            IdGroup = 5,
                            Username = Username,
                            Password = Password,
                            CreatedDate = DatetimeHelper.GetDatetimeNow(),
                            CreatedBy = "sistem registrasi",
                            IdCustomer = content.IdCustomer
                        };

                        var res = await vmacc.RegisterAccount(account);

                        if (!res)
                        {
                            // close the loading page...
                            DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                            await DisplayAlert("Pemberitahuan", "Registrasi gagal. silahkan coba lagi atau gunakan email lain", "OK");
                        }
                        else
                        {
                            // close the loading page...
                            DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                            await DisplayAlert("Pemberitahuan", "Registrasi berhasil", "OK");
                            await Navigation.PushModalAsync(new LoginPage());
                        }
                    }
                    else
                    {
                        // close the loading page...
                        DependencyService.Get<ILoadingPageService>().HideLoadingPage();

                        await DisplayAlert("Pemberitahuan", "Registrasi gagal, silahkan coba lagi", "OK");
                    }
                }
                BtnDaftar.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps_RegistrasiPage_DaftarClicked");
                BtnDaftar.IsEnabled = true;
            }
        }

        #region Event Handler
        private async void OnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Daftar_Clicked(Object sender, EventArgs e)
        {
            BtnDaftar.IsEnabled = false;
            DoDaftar();
        }
        #endregion
    }
}