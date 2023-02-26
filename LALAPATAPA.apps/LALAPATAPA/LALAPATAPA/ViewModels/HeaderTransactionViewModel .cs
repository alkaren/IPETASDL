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
    public class HeaderTransactionViewModel : ViewModelBase
    {
        readonly IRequestService requestService;
        public HeaderTransactionViewModel(
            IRequestService requestService)
        {
            this.requestService = requestService;
        }

        #region Header Transaction
        public async Task<List<HeaderTransaction>> LoadAllHeaderTransactionByIdCus(int id)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/headertransactions/idcus/{id}");

                var uri = builder.ToString();

                var data = await requestService.GetAsync<List<HeaderTransaction>>(uri, Config.AccessToken);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_HeaderTransactionViewModel_LoadAllHeaderTransactionByIdCus");
                return null;
            }
            finally
            {
               
            }
        }

        public async Task<bool> UpdateHeaderTransaction(HeaderTransaction header)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/headertransactions/{header.IdTransaction}");

                var uri = builder.ToString();

                var res = await requestService.PutAsync<HeaderTransaction,object>(uri, header, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_HeaderTransactionViewModel_UpdateHeaderTransaction");
                return false;
            }
            finally
            {

            }
        }

        public async Task<List<HeaderTransaction>> LoadAllHeaderTransaction()
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/headertransactions");

                var uri = builder.ToString();

                var data = await requestService.GetAsync<List<HeaderTransaction>>(uri, Config.AccessToken);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_HeaderTransactionViewModel_LoadAllHeaderTransaction");
                return null;
            }
            finally
            {

            }
        }

        public async Task<HeaderTransaction> SavePesanan(HeaderTransaction headerTransaction)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/headertransactions");

                var uri = builder.ToString();

                var data = await requestService.PostAsync<HeaderTransaction>(uri, headerTransaction, Config.AccessToken);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_HeaderTransactionViewModel_SavePesanan");
                return null;
            }
            finally
            {

            }
        }
        #endregion
    }
}
