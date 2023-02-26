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
    public class DeviceViewModel : ViewModelBase
    {
        readonly IRequestService requestService;
        public DeviceViewModel(
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

        public async Task<bool> SaveDevice(Device device)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/devices");

                var uri = builder.ToString();

                var data = await requestService.PostAsync<Device>(uri, device, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_DeviceViewModel_SaveDevice");
                return false;
            }
            finally
            {

            }
        }

        public async Task<bool> DeleteDevice(string installationId)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/devices/{installationId}");

                var uri = builder.ToString();

                var data = await requestService.DeleteAsync<Device>(uri, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_DeviceViewModel_DeleteDevice");
                return false;
            }
            finally
            {

            }
        }
        #endregion
    }
}
