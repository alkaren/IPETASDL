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
    class EmployeeViewModel : ViewModelBase
    {
        readonly IRequestService requestService;
        public EmployeeViewModel(
            IRequestService requestService)
        {
            this.requestService = requestService;
        }

        #region Employee
        public async Task<Employee> LoadEmployeeById(int id)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath($"/employees/{id}");

                var uri = builder.ToString();

                var data = await requestService.GetAsync<Employee>(uri, Config.AccessToken);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_EmployeeViewModel_LoadEmployeeById");
                return null;
            }
        }
        #endregion
    }
}
