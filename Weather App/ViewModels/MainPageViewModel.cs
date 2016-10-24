using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using SharedLibrary.SharedLibraryVM;
using SharedLibrary.Models;
using System.Collections.ObjectModel;

namespace Weather_App.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        /// <summary>
            /// The <see cref="DayList" /> property's name.
            /// </summary>
        public const string MyPropertyPropertyName = "MyProperty";

        private ObservableCollection<Day> _dayList = null;

        /// <summary>
        /// Sets and gets the MyProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Day> DayList
        {
            get
            {
                return _dayList;
            }

            set
            {
                if (_dayList == value)
                {
                    return;
                }

                _dayList = value;
                RaisePropertyChanged(MyPropertyPropertyName);
            }
        }

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
                DayList = new ObservableCollection<Day>();
                DayList.Add(new Day { temp = 20, Time = DateTime.Now });
                DayList.Add(new Day { temp = 20, Time = DateTime.Now.AddDays(1) });
                DayList.Add(new Day { temp = 20, Time = DateTime.Now.AddDays(2) });
                DayList.Add(new Day { temp = 20, Time = DateTime.Now.AddDays(3) });
                DayList.Add(new Day { temp = 20, Time = DateTime.Now.AddDays(4) });
                DayList.Add(new Day { temp = 20, Time = DateTime.Now.AddDays(5) });
                DayList.Add(new Day { temp = 20, Time = DateTime.Now.AddDays(6) });
            }
            else {

            }
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

     
        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

