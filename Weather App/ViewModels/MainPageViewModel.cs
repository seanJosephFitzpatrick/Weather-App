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
using System.Collections;
using Windows.Data.Json;


namespace Weather_App.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
 
        /// <summary>
        /// The <see cref="CurrentDay" /> property's name.
        /// </summary>
        public const string CurrentDayPropertyName = "CurrentDay";

        private Day _currentDay = null;

        /// <summary>
        /// Sets and gets the CurrentDay property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Day CurrentDay
        {
            get
            {
                return _currentDay;
            }

            set
            {
                if (_currentDay == value)
                {
                    return;
                }

                _currentDay = value;
                RaisePropertyChanged(CurrentDayPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TempList" /> property's name.
        /// </summary>
        public const string TempListPropertyName = "TempList";

        private ObservableCollection<Day> _tempList = null;

        /// <summary>
        /// Sets and gets the TempList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Day> TempList
        {
            get
            {
                return _tempList;
            }

            set
            {
                if (_tempList == value)
                {
                    return;
                }

                _tempList = value;
                RaisePropertyChanged(TempListPropertyName);
            }
        }

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
              
            }

            try
            {
                loadData();
                DayList = new ObservableCollection<Day>();
                
                
                CurrentDay = DayList[0];
             


            }
            catch (ArgumentOutOfRangeException)
            {

                return;
            }

        }
        
        public async void loadData()
        {
            await PopulateWeatherDataAsync();

        }

      

        public async Task PopulateWeatherDataAsync()
        {

            try
            {
                var position = await GeolocationVM.GetPosition();


                var lat = position.Coordinate.Latitude;
                var lon = position.Coordinate.Longitude;


                //RootObject result = await APIDataVM.GetWeather(lat, lon);
                // var RootObject = await APIDataVM.GetWeatherDays(lat, lon);
                
                RootObjectDays weather = await APIDataVM.GetWeatherDays(lat, lon);


                var Days = new ObservableCollection<Day>(weather.list);

              
                

                //var Days = RootObject.list;




                foreach (var list in Days)
                {
                    //Covert Fahreheit to celsius
                    list.temp.min = (((int)list.temp.min) - 32);
                    list.temp.max = (((int)list.temp.max) - 32);

                    DayList.Add(list);


                }



            }
            catch (Exception)
            {
                // LocationText.Text = "Unable to access Weather";
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

