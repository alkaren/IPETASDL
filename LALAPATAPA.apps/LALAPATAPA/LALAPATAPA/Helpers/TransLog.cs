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
using LALAPATAPA.Models;

namespace LALAPATAPA.Helpers
{
    public static class TransLog
    {
        public static async Task<bool> SaveTransLog(TransactionLog param)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
                    var res = await client.PostAsync(Config.WebService + "/transactionlogs", stringContent);

                    if (res.IsSuccessStatusCode)
                    {
                        var newjson = res.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<TransactionLog>(newjson);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps-Translog-SaveTransLog");
            }

            return false;
        }

        public static async Task<List<TransactionLog>> GetTransactionLog(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
                    var res = await client.GetAsync(Config.WebService + $"/transactionlogs/cusid/{id}");
                    if (res.IsSuccessStatusCode)
                    {
                        var newjson = res.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<List<TransactionLog>>(newjson);
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps-Translog-GetTransactionLog");
            }
            return null;
        }
        public static async Task<List<TransactionLog>> GetAllTransactionLog()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
                    var res = await client.GetAsync(Config.WebService + $"/transactionlogs");
                    if (res.IsSuccessStatusCode)
                    {
                        var newjson = res.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<List<TransactionLog>>(newjson);
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps-Translog-GetTransactionLog");
            }
            return null;
        }
    }
}
