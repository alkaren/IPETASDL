using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LALAPATAPA.Helpers
{
    public class IRefreshPull : INotifyPropertyChanged
    {
        public ObservableCollection<string> Items { get; set; }
        Page page;
        public IRefreshPull(Page page)
        {
            this.page = page;
            Items = new ObservableCollection<string>();
        }

        bool canRefresh = true;

        public bool CanRefresh
        {
            get { return canRefresh; }
            set
            {
                if (canRefresh == value)
                    return;

                canRefresh = value;
                OnPropertyChanged("CanRefresh");
            }
        }


        bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                    return;

                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        ICommand refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            Items.Clear();

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {

                for (int i = 0; i < 100; i++)
                    Items.Add(DatetimeHelper.GetDatetimeNow().AddMinutes(i).ToString("F"));

                IsBusy = false;

                //page.DisplayAlert("Refreshed", "You just refreshed the page! Nice job! Pull to refresh is now disabled", "OK");
                this.CanRefresh = false;

                return false;
            });
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
