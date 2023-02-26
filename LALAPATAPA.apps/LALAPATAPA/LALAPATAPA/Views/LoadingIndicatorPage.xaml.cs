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
    public partial class LoadingIndicatorPage : ContentPage
    {
        public LoadingIndicatorPage()
        {
            InitializeComponent();
        }
    }

    public interface ILoadingPageService
    {
        void InitLoadingPage
            (ContentPage loadingIndicatorPage = null);

        void ShowLoadingPage();
        void HideLoadingPage();
    }
}