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
    public class ProductViewModel : ViewModelBase
    {
        readonly IRequestService requestService;

        public ProductViewModel(
            IRequestService requestService)
        {
            this.requestService = requestService;
        }

        #region Product
        public async Task<Product> SaveProduct(Product product)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/products");

                var uri = builder.ToString();

                var data = await requestService.PostAsync<Product>(uri, product, Config.AccessToken);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_ProductViewModel_SaveProduct");
                return null;
            }
            finally
            {

            }
        }
        #endregion
    }
}
