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
    public static class Logging
    {
        public static async Task<bool> SaveLog(Log param)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
                var res = await client.PostAsync(Config.WebService + "/logs", stringContent);

                if (res.IsSuccessStatusCode)
                {
                    var newjson = res.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Log>(newjson);

                    return true;
                }
            }

            return false;
        }

        public static async Task<bool> Writelog(string message, string source)
        {
            var content = new Log
            {
                Message = message,
                Source = source,
                Time = DatetimeHelper.GetDatetimeNow().ToLocalTime()
            };

            var res = await SaveLog(content);
            if (res)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
