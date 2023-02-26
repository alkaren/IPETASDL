using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALAPATAPA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpView
    {
        public PopUpView()
        {
            InitializeComponent();
        }
        private async void OnKembali_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}