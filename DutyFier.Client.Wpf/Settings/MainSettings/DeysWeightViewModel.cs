using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Settings.MainSettings
{
    class DeysWeightViewModel : INotifyPropertyChanged
    {
        public SettingsModel SettingsModel { get; set; }

        private DaysOfWeekWeight _daysOfWeek;

        public ObservableCollection<DaysOfWeekWeight> Days { get; set; } 
        public DaysOfWeekWeight SelectedDays
        {
            get
            {
                return _daysOfWeek;
            }
            set
            {
                OnPropertyChanged("SelectedDays");
                _daysOfWeek = value;
            }
        }
        public DeysWeightViewModel(SettingsModel settingsModel)
        {
            SettingsModel = settingsModel;
            Days = new ObservableCollection<DaysOfWeekWeight>(SettingsModel.GetAllDaysOfWeek());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
