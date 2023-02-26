using LALAPATAPA.Helpers;
using LALAPATAPA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LALAPATAPA.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNotifPage : ContentPage
    {
        public TestNotifPage()
        {
            InitializeComponent();
            _client.BaseAddress = new Uri(App.MobileServiceUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.Timeout = TimeSpan.FromSeconds(120);
        }
        HttpClient _client = new HttpClient();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object, string>(this, App.NotificationReceivedKey, OnMessageReceived);
            btnSend.Clicked += OnBtnSendClicked;
        }

        void OnMessageReceived(object sender, string msg)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                lblMsg.Text = msg;
            });
        }

        async void OnBtnSendClicked(object sender, EventArgs e)
        {
            Debug.WriteLine($"Sending message: " + txtMsg.Text);

            var content = new StringContent("\"" + txtMsg.Text + "\"", Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("xamunotifications", content);
            Debug.WriteLine("Send result: " + result.IsSuccessStatusCode);
            try
            {
                var input = new Notification
                {
                    Text = txtMsg.Text                    
                };

                var DoEdit = await Data.InputMessage(input);
            }
            catch(Exception ex)
            {
                await DisplayAlert("error", ex.ToString(), "ok");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, App.NotificationReceivedKey);
        }
    }
}