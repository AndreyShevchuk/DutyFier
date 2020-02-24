using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;
namespace DutyFier.Client.Wpf.Statistics
{
    class StatisticsViewModel :INotifyPropertyChanged
    {
        private StatisticModel _statisticModel;

        public StatisticsViewModel()
        {
            _statisticModel = new StatisticModel( new PersonRepository( MainWindowViewModel.Container.Resolve<DutyFierContext>()), new PersonDutyFeedbackRepository( MainWindowViewModel.Container.Resolve<DutyFierContext>()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //moved from old project

        private void InizializeScore()
        {
            
        }

        public List<PersonScoreCover> Persons
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
