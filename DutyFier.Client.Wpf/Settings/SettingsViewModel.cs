using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity;

namespace DutyFier.Client.Wpf.Settings
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        public PersonRepository PersonReposytory { get; }

        private DutyFierContext DutyFierContext;
        private Person selectedPerson;
        private Person selectedExecutor;

        public Person SelectedExecutor
        {
            get
            {
                return selectedExecutor;
            }
            set
            {
                selectedExecutor = value;
                OnPropertyChanged("SelectedExecutor");
            }
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

        public ObservableCollection<Position> PersonPositions { get; private set; }
        public SettingsModel SettingsModel { get; set; }
        public ObservableCollection<Person> People { get; set; }
        public Position SelectedPositionsToRemove { get; set; }
        public Position SelectedPositionToRemove { get; set; }
        public Position SelectedPosition { get; set; }
        public ObservableCollection<Position> Allpositions { get; set; }
        public RelayCommands AddPositionCommand { get; set; }
        public RelayCommands RemovePositionCommand { get; set; }
        public RelayCommands AddExecutorCommand { get; set; }
        public RelayCommands AddPositionsCommand { get; set; }
        public RelayCommands AddTypeCommand { get; set; }
        public RelayCommands RemovePersonCommand { get; set; }
        public RelayCommands RemovePositionsCommand { get; set; }

        public SettingsViewModel()
        {
            DutyFierContext = MainWindowViewModel.Container.Resolve<DutyFierContext>();
            SettingsModel = new SettingsModel(new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()), new PositionRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
            this.PersonReposytory = new PersonRepository();
            People = new ObservableCollection<Person>(DutyFierContext.Persons.ToList());
            Allpositions = new ObservableCollection<Position>(SettingsModel.GetAllPosition());
            AddPositionCommand = new RelayCommands(addPositionCommand, Can);
            RemovePositionCommand = new RelayCommands(removePositionCommand, Can);
            AddExecutorCommand = new RelayCommands(addExecutorCommand, Can);
            AddPositionsCommand = new RelayCommands(addPositionsCommand, Can);
            AddTypeCommand = new RelayCommands(addTypeCommand, Can);
            RemovePersonCommand = new RelayCommands(removePersonCommand, Can);
            RemovePositionsCommand = new RelayCommands(removePositionsCommand, Can);
        }
        private void removePositionsCommand()
        {
            SettingsModel.RemovePosition(SelectedPositionsToRemove);
            Allpositions.Remove(SelectedPositionsToRemove);

        }

        private void removePersonCommand()
        {
            SettingsModel.RemovePerson(SelectedExecutor);
            People.Remove(SelectedExecutor);
        }

        private void addTypeCommand()
        {
            AddTypeView addType = new AddTypeView();
            if (addType.ShowDialog() == true)
            {
                addType.Close();
            }
        }

        private void addPositionsCommand()
        {
            AddPositionView addPosition = new AddPositionView();
            if (addPosition.ShowDialog() == true)
            {
                //Allpositions = DutyFierContext.Positions.ToList();
                OnPropertyChanged("Allpositions");
                addPosition.Close();
            }
        }

        private void addExecutorCommand()
        {
            AddPersonView apv = new AddPersonView();
            if (apv.ShowDialog() == true)
            {
                People = new ObservableCollection<Person>(DutyFierContext.Persons.ToList());
                OnPropertyChanged("People");
                apv.Close();
            }
        }

        private void removePositionCommand()
        {
            SelectedPerson.Positions.Remove(SelectedPositionToRemove);
            PersonPositions.Remove(SelectedPositionToRemove);
            SettingsModel.UpdatePersonDependencyToPosition(SelectedPerson);
        }

        private void addPositionCommand()
        {
            SelectedPerson.Positions.Add(SelectedPosition);
            PersonPositions.Add(SelectedPosition);
            SettingsModel.UpdatePersonDependencyToPosition(SelectedPerson);
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