using LALAPATAPA.Models;
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
	public partial class PopupStatusPesananGresDone
	{
		HeaderTransactionViewModel vmtrans;
		HeaderTransaction dataheader;
		public PopupStatusPesananGresDone(HeaderTransaction datas)
		{
			vmtrans = Locator.Instance.Resolve(typeof(HeaderTransactionViewModel)) as HeaderTransactionViewModel;
			InitializeComponent();
			dataheader = datas;
		}
		private async void OnLanjutkan_Clicked(object sender, EventArgs e)
		{
			PopupNavigation.PopAsync();
			var content = new HeaderTransaction
			{
				IdTransaction = dataheader.IdTransaction,
				IdCustomer = dataheader.IdCustomer,
				IdAttachment = dataheader.IdAttachment,
				TransactionNumber = dataheader.TransactionNumber,
				TransactionDate = dataheader.TransactionDate,
				TransactionStatus = Config.FlowStatus[13],
				PaymentMethode = dataheader.PaymentMethode,
				PaymentTotal = dataheader.PaymentTotal,
				CreatedDate = dataheader.CreatedDate,
				CreatedBy = dataheader.CreatedBy,
				UpdatedDate = DatetimeHelper.GetDatetimeNow(),
				UpdatedBy = Config.CurrentUserName

			};

			var res = await vmtrans.UpdateHeaderTransaction(content);

			if (res)
			{
				var transLog = new TransactionLog
				{
					IdTransaction = dataheader.IdTransaction,
					Message = "Peta tidak tersedia",
					Source = Config.CurrentUserName,
					Time = DatetimeHelper.GetDatetimeNow()
				};

				await TransLog.SaveTransLog(transLog);
			}
		}
	}
}