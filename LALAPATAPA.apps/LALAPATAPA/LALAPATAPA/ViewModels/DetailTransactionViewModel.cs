using LALAPATAPA.Extensions;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.Services.Request;
using LALAPATAPA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LALAPATAPA.ViewModels
{
    public class DetailTransactionViewModel : ViewModelBase
    {
        readonly IRequestService requestService;
        public DetailTransactionViewModel(
            IRequestService requestService)
        {
            this.requestService = requestService;
        }

        #region Detail Transaction
        public async Task<DetailTransaction> LoadDetailTransactionByTransId(int id)
        {

            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/detailtransactions/transid/{id}");

                var uri = builder.ToString();

                var data = await requestService.GetAsync<DetailTransaction>(uri, Config.AccessToken);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_DetailTransactionViewModel_LoadDetailTransactionByTransId");
                return null;
            }
            finally
            {

            }

        }

        public async Task<bool> UpdateDetailTransaction(DetailTransaction detailTransaction)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/detailtransactions/{detailTransaction.IdDetail}");

                var uri = builder.ToString();

                var res = await requestService.PutAsync<DetailTransaction, object>(uri, detailTransaction, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_DetailTransactionViewModel_UpdateDetailTransaction");
                return false;
            }
        }

        public async Task<DetailTransaction> SaveDetail(DetailTransaction detailTransaction)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/detailtransactions");

                var uri = builder.ToString();

                var data = await requestService.PostAsync<DetailTransaction>(uri, detailTransaction, Config.AccessToken);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_DetailTransactionViewModel_SaveDetail");
                return null;
            }
            finally
            {

            }
        }
        #endregion
    }
}
