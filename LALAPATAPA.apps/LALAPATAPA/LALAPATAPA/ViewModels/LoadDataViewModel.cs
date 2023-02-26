using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;
using Xamarin.Essentials;

namespace LALAPATAPA.ViewModels
{
    class LoadDataViewModel
    {
        public List<HeaderTransaction> headerTransactionData { get; set; }
        public List<DetailTransaction> detailTransactionData { get; set; }

        HeaderTransactionViewModel headervm;
        DetailTransactionViewModel detailvm;
        public LoadDataViewModel()
        {
            headervm = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
            detailvm = Locator.Instance.Resolve(typeof(DetailTransactionViewModel)) as DetailTransactionViewModel;

            //Configuration();
        }

        //private async void Configuration()
        //{
        //    await LoadData();
        //}

        #region CustomerApps

        //async Task LoadData()
        //{
        //    headerTransactionData = await headervm.LoadAllHeaderTransactionByIdCus(Config.CurrentUserId);
        //}
        public async Task<List<HeaderTransaction>> CusLoadAllProsesTransaction()
        {
            try
            {
                var content = await headervm.LoadAllHeaderTransactionByIdCus(Config.CurrentUserId);

                var TransProses = from x in content
                                  where x.TransactionStatus == Config.FlowStatus[0]
                                  || x.TransactionStatus == Config.FlowStatus[1]
                                  || x.TransactionStatus == Config.FlowStatus[2]
                                  || x.TransactionStatus == Config.FlowStatus[3]
                                  || x.TransactionStatus == Config.FlowStatus[4]
                                  || x.TransactionStatus == Config.FlowStatus[5]
                                  select x;

                return TransProses.ToList();
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps_LoadDataVIewModel_CusLoadAllProsesTransaction");
                return null;
            }
        }
        public async Task<List<HeaderTransaction>> CusLoadAllSelesaiTransaction()
        {
            try
            {
                var content = await headervm.LoadAllHeaderTransactionByIdCus(Config.CurrentUserId);

                var TransSelesai = from x in content
                                   where x.TransactionStatus == Config.FlowStatus[8]
                                   || x.TransactionStatus == Config.FlowStatus[7]
                                   || x.TransactionStatus == Config.FlowStatus[6]
                                   select x;

                return TransSelesai.ToList();
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps_LoadDataVIewModel_CusLoadAllSelesaiTransaction");
                return null;
            }
        }
        #endregion
    }
}
