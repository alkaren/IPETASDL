﻿using LALAPATAPA.Models;
using LALAPATAPA.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LALAPATAPA.ViewModels;
using LALAPATAPA.ViewModels.Base;

namespace LALAPATAPA.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpBudiPage
    {
        HeaderTransactionViewModel vm;
        HeaderTransaction dataheader;
        public PopUpBudiPage(HeaderTransaction datas)
        {
            InitializeComponent();
            dataheader = datas;
        }
        private async void OnLanjutkan_Clicked(object sender, EventArgs e)
        {            
            try
            {
                var content = new HeaderTransaction
                {
                    IdTransaction = dataheader.IdTransaction,
                    IdCustomer = dataheader.IdCustomer,
                    IdAttachment = dataheader.IdAttachment,
                    TransactionNumber = dataheader.TransactionNumber,
                    TransactionDate = DatetimeHelper.GetDatetimeNow(),
                    TransactionStatus = Config.FlowStatus[10],
                    PaymentMethode = dataheader.PaymentMethode,
                    PaymentTotal = dataheader.PaymentTotal,
                    CreatedDate = dataheader.CreatedDate,
                    CreatedBy = dataheader.CreatedBy,
                    UpdatedDate = DatetimeHelper.GetDatetimeNow(),
                    UpdatedBy = Config.CurrentUserName

                };

                var res = await vm.UpdateHeaderTransaction(content);

                if (res)
                {
                    var transLog = new TransactionLog
                    {
                        IdTransaction = dataheader.IdTransaction,
                        Message = "Menunggu no rekening dari Budi",
                        Source = Config.CurrentUserName,
                        Time = DatetimeHelper.GetDatetimeNow()
                    };

                    await TransLog.SaveTransLog(transLog);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("error", ex.ToString(), "ok");
            }
            await PopupNavigation.PopAsync();
        }
    }
}