using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity;

namespace DutyFier.Client.Wpf.Settings.MainSettings
{
    internal class EditPositionsViewModel : INotifyPropertyChanged
    {
        public PersonRepository PersonReposytory { get; }
        private Person selectedPerson;

        public SettingsModel SettingsModel { get; set; }

        public EditPositionsViewModel()
        {
            SettingsModel = new SettingsModel(new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()), new PositionRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
            this.PersonReposytory = new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            Allpositions = new ObservableCollection<Position>(SettingsModel.GetAllPosition());
            People = new ObservableCollection<Person>(SettingsModel.GetAllPerson());

            AddPositionCommand = new RelayCommands(addPositionCommand, Can);
            RemovePositionCommand = new RelayCommands(removePositionCommand, Can);
            AddPositionToAllCommand = new RelayCommands(addPositionToAllCommand, Can);
            RemovePositionFromAllCommand = new RelayCommands(removePostionFromAllCommand, Can);
        }

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                PersonPositions = new ObservableCollection<Position>(value.Positions.ToList());
                OnPropertyChanged("PersonPositions");
                OnPropertyChanged("SelectedPerson");
            }
        }

        public Position SelectedPositionToRemove { get; set; }
        public RelayCommands AddPositionCommand { get; set; }
        public RelayCommands RemovePositionCommand { get; set; }
        public RelayCommands AddPositionToAllCommand { get; set; }
        public RelayCommands RemovePositionFromAllCommand { get; set; }
        public ObservableCollection<Position> Allpositions { get; set; }
        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<Position> PersonPositions { get; private set; }
        public Position SelectedPosition { get; set; }

        private void removePositionCommand()
        {
            SelectedPerson.Positions.Remove(SelectedPositionToRemove);
            PersonPositions.Remove(SelectedPositionToRemove);
            SettingsModel.UpdatePersonDependencyToPosition(SelectedPerson);
        }

        private void addPositionCommand()
        {
            SelectedPerson.Positions.Add(SelectedPosition);
            if (!(PersonPositions.Contains(SelectedPosition)))
            {
                PersonPositions.Add(SelectedPosition);
            }
            SettingsModel.UpdatePersonDependencyToPosition(SelectedPerson);
        }
        private void removePostionFromAllCommand()
        {
            foreach(var person in People)
            {
                person.Positions.Remove(SelectedPosition);
                SettingsModel.UpdatePersonDependencyToPosition(person);
            }
            PersonPositions.Remove(SelectedPosition);
        }
        private void addPositionToAllCommand()
        {
            foreach(var person in People)
            {
                person.Positions.Add(SelectedPosition);
                SettingsModel.UpdatePersonDependencyToPosition(person);
            }
            if (!(PersonPositions.Contains(SelectedPosition)))
            {
                PersonPositions.Add(SelectedPosition);
            }

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