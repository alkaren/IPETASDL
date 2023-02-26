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

namespace LALAPATAPA.Helpers
{
    public static class Data
    {       
        #region Product
        public static async Task<Product> LoadProductById(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
                    var res = await client.GetAsync(Config.WebService + $"/products/{id}");
                    if (res.IsSuccessStatusCode)
                    {
                        var content = res.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<Product>(content);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps-Data-LoadProductById");
            }

            return null;
        }

        #endregion

        #region BankAccount
        public static async Task<BankAccount> LoadBankAccountById(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
                    var res = await client.GetAsync(Config.WebService + $"/bankaccounts/{id}");
                    if (res.IsSuccessStatusCode)
                    {
                        var content = res.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<BankAccount>(content);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps-Data-LoadBankAccountById");
            }
            return null;
        }
        public static async Task<BankAccount> InputRekening(BankAccount bank)
        {
            var client = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(bank), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
            var res = await client.PostAsync(Config.WebService + $"/BankAccounts", stringContent);
            if (res.IsSuccessStatusCode)
            {
                var newjson = res.Content.ReadAsStringAsync().Result;
                var content = JsonConvert.DeserializeObject<BankAccount>(newjson);
                return content;
            }

            return null;
        }
        #endregion

        #region Payment Attachment
        public static async Task<PaymentAttachment> SaveAttachment(PaymentAttachment data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
                    var res = await client.PostAsync(Config.WebService + $"/paymentattachments", stringContent);
                    if (res.IsSuccessStatusCode)
                    {
                        var content = res.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<PaymentAttachment>(content);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                await Logging.Writelog(ex.Message, "MobileApps-Data-SaveAttachment");
            }
            return null;
        }
        #endregion
        
        #region attachment
        public static async Task<PaymentAttachment> LoadAttachment(int id)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
            var res = await client.GetAsync(Config.WebService + $"/paymentattachments/transid/{id}");
            if (res.IsSuccessStatusCode)
            {
                var content = res.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<PaymentAttachment>(content);

                return result;
            }

            return null;
        }
        public static async Task<HeaderTransaction> LoadTrAttachment(int id)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
            var res = await client.GetAsync(Config.WebService + $"/headertransactions/{id}");
            if (res.IsSuccessStatusCode)
            {
                var content = res.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<HeaderTransaction>(content);

                return result;
            }

            return null;
        }
        #endregion
        public static async Task<Notification> InputMessage(Notification notif)
        {
            var client = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(notif), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
            var res = await client.PostAsync(Config.WebService + $"/Notifications", stringContent);
            if (res.IsSuccessStatusCode)
            {
                var newjson = res.Content.ReadAsStringAsync().Result;
                var content = JsonConvert.DeserializeObject<Notification>(newjson);
                return content;
            }

            return null;
        }

        public static async Task<bool> TambahPeta(Product maps)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(maps), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Config.AccessToken);
                var res = await client.PutAsync(Config.WebService + $"/products/{maps.IdProduct}", stringContent);
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }

        public static async Task<bool> TryForgotPassword(Account account)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
                var res = await client.PutAsync(Config.WebService + $"/accounts/forgotpass/{account.IdAccount}", stringContent);
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
