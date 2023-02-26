using System.Threading.Tasks;
using LALAPATAPA.Services.Dialog;


namespace LALAPATAPA.ViewModels.Base
{
    public abstract class ViewModelBase : MvvmHelpers.BaseViewModel
    {
        protected readonly IDialogService DialogService;
       

        public ViewModelBase()
        {
            DialogService = Locator.Instance.Resolve<IDialogService>();
           
        }

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
    }
}