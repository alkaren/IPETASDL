using LALAPATAPA.Exceptions;
using LALAPATAPA.Extensions;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.Services.Authentication;
using LALAPATAPA.Services.Request;
using LALAPATAPA.Validations;
using LALAPATAPA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LALAPATAPA.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        
        readonly IAuthenticationService authenticationService;
        readonly IRequestService requestService;
      
        ValidatableObject<string> userName;
        ValidatableObject<string> password;

        public bool IsValid { get; set; }

        public LoginViewModel(
            IAuthenticationService authenticationService, IRequestService requestService)
        {
           
            this.authenticationService = authenticationService;
            this.requestService = requestService;
            userName = new ValidatableObject<string>();
            password = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public ValidatableObject<string> Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }       

        public async Task<Account> Login(Account account)
        {
            IsBusy = true;

            //IsValid = Validate();

            //if (IsValid)
            //{
                //var isAuth = await authenticationService.LoginAsync(UserName.Value, Password.Value);

                //if (isAuth)
                //{
                //    IsBusy = false;
                //}
            //}

            //MessagingCenter.Send(this, MessengerKeys.SignInRequested);

            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/accounts/authenticate");
                
                var uri = builder.ToString();

                var data = await requestService.PostAsync<Account>(uri, account);

                Config.AccessToken = data.Token;
                Config.CurrentUserGroup = data.IdGroup.Value;
                
                return data;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                //Debug.WriteLine(ex.Message + "_" + ex.StackTrace);                
                await Logging.Writelog(ex.Message, "MobileApps_LoginViewModel_Login");

                return null;
            }
            finally
            {

            }
        }     

        void AddValidations()
        {
            userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username should not be empty" });
            userName.Validations.Add(new EmailRule());
            password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password should not be empty" });
        }

        bool Validate()
        {
            var isValidUser = userName.Validate();
            var isValidPassword = password.Validate();

            return isValidUser && isValidPassword;
        }     
    }
}
