using System;
using System.Collections.Generic;
using System.Text;
using LALAPATAPA.Extensions;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.Services.Authentication;
using LALAPATAPA.Services.Request;
using LALAPATAPA.ViewModels.Base;
using System.Threading.Tasks;

namespace LALAPATAPA.ViewModels
{
    class AccountViewModel : ViewModelBase
    {
        readonly IRequestService requestService;

        public AccountViewModel(
            IRequestService requestService)
        {
            this.requestService = requestService;
        }

        #region Account
        public async Task<bool> RegisterAccount(Account account)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/accounts/cus");

                var uri = builder.ToString();

                var data = await requestService.PostAsync<Account>(uri, account);
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_AccountViewModel_RegisterAccount");
                return false;
            }
            finally
            {

            }
        }
        public async Task<Account> LoadAccountById(int id)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/accounts/{id}");

                var uri = builder.ToString();

                var data = await requestService.GetAsync<Account>(uri, Config.AccessToken);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_CustomerViewModel_LoadCustomerById");
                return null;
            }
            finally
            {

            }
        }
        public async Task<bool> EditAccount(Account account)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/accounts/{account.IdAccount}");

                var uri = builder.ToString();

                var res = await requestService.PutAsync<Account, object>(uri, account, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_AccountViewModel_EditAccount");
                return false;
            }
            finally
            {

            }
        }
        public async Task<Account> FindAccountByEmail(string email)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/accounts/isexist/{email}");

                var uri = builder.ToString();

                var data = await requestService.GetAsync<Account>(uri);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_AccountViewModel_FindAccountByEmail");
                return null;
            }
            finally
            {

            }
        }
        public async Task<bool> ForgotPasswordAccount(Account account)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/accounts/forgotpass/{account.IdAccount}");

                var uri = builder.ToString();

                var res = await requestService.PutAsync<Account, object>(uri, account);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_AccountViewModel_ForgotPasswordAccount");
                return false;
            }
            finally
            {

            }
        }
        #endregion
    }
}
