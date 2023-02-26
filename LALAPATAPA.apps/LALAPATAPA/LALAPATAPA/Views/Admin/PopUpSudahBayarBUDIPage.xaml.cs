using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALAPATAPA.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpSudahBayarBUDIPage
    {
        public PopUpSudahBayarBUDIPage()
        {
            InitializeComponent();
        }
        private async void OnLanjutkan_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}