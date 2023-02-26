using LALAPATAPA.Extensions;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.Services.Authentication;
using LALAPATAPA.Services.Request;
using LALAPATAPA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LALAPATAPA.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        readonly IRequestService requestService;
        public CustomerViewModel(
            IRequestService requestService)
        {
            this.requestService = requestService;
        }

        #region Customer
        public async Task<Customer> LoadCustomerById(int id)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/customers/{id}");

                var uri = builder.ToString();

                var data = await requestService.GetAsync<Customer>(uri, Config.AccessToken);

                Config.CurrentEmailUser = data.Email;
                Config.CurrentUserName = data.Name;

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
        public async Task<Customer> LoadCustData(int id)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/customers/{id}");

                var uri = builder.ToString();

                var data = await requestService.GetAsync<Customer>(uri, Config.AccessToken);

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
        public async Task<bool> EditCustomers(Customer customer)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/customers/{customer.IdCustomer}");

                var uri = builder.ToString();

                var res = await requestService.PutAsync<Customer, object>(uri, customer, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_CustomerViewModel_EditCustomers");
                return false;
            }
            finally
            {

            }
        }
               
        public async Task<Customer> RegisterCustomers(Customer customer)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/customers");

                var uri = builder.ToString();

                var data = await requestService.PostAsync<Customer>(uri, customer, Config.AccessToken);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_CustomerViewModel_RegisterCustomer");
                return null;
            }
            finally
            {

            }
        }
        #endregion
    }
}
