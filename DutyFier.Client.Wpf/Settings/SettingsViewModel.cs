
using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Settings
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        List<Person> people;
        private List<Position> allpositions;
        private Person selectedPerson;
        private List<Position> positions;
        
        public List<Person> People { get => people; set => people = value; }
        public List<Position> Positions
        {
            get { return positions; }
        }

        public Position SelectedPosition { get; set; }
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                positions = value.Positions.ToList();
                OnPropertyChanged("Positions");
                OnPropertyChanged("SelectedPerson");
                
            }
        }

        public List<Position> Allpositions { get => allpositions; set => allpositions = value; }
        public RelayCommands AddPositionCommand { get; set; }
        public SettingsViewModel()
        {
            
            people = new PersonRepository().GetAll().ToList();
            allpositions = new PositionRepository().GetAll().ToList();
            AddPositionCommand = new RelayCommands(addPositionsCommand, Can);
        }
        private void addPositionsCommand()
        {
            SelectedPerson.Positions.Add(SelectedPosition);
            positions = SelectedPerson.Positions.ToList();
            OnPropertyChanged("Positions");
        }
        public bool Can()
        {
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }



    }
}

