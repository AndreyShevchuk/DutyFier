using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Statistics
{
    class StatisticsViewModel :INotifyPropertyChanged
    {
        private StatisticModel _statisticModel;

        public StatisticsViewModel()
        {
            // TODO: use Unity as IoT
            _statisticModel = new StatisticModel(new PersonRepository(), new PersonDutyFeedbackRepository());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //moved from old project

        private void InizializeScore()
        {
            
        }

        public List<Person> Persons
        {
            get
            {
                return _statisticModel.GetPersons();
            }
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
