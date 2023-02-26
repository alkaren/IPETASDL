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
    class FeedbackViewModel : ViewModelBase
    {
        readonly IRequestService requestService;

        public FeedbackViewModel(
            IRequestService requestService)
        {
            this.requestService = requestService;
        }

        #region Feedback
        public async Task<bool> SaveComment(Feedback feedback)
        {
            try
            {
                var builder = new UriBuilder(Config.WebService);
                builder.AppendToPath("/feedbacks");

                var uri = builder.ToString();

                var res = await requestService.PostAsync<Feedback>(uri, feedback, Config.AccessToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "_" + ex.StackTrace);
                await Logging.Writelog(ex.Message, "MobileApps_FeedbackViewModel_SaveComment");
                return false;
            }
            finally
            {

            }
        }
        #endregion
    }
}
