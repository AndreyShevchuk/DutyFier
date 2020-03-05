using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity;

namespace DutyFier.Client.Wpf.Settings.MainSettings
{
    internal class PositionsViewModel : INotifyPropertyChanged
    {
        public SettingsModel SettingsModel { get; set; }
        public PersonRepository PersonReposytory { get; }
        public ObservableCollection<Position> Allpositions { get; set; }
        public Position SelectedPositionsToRemove { get; set; }
        public RelayCommands AddPositionsCommand { get; set; }
        public RelayCommands RemovePositionsCommand { get; set; }

        public RelayCommands AddTypeCommand { get; set; }

        public PositionsViewModel()
        {
            SettingsModel = new SettingsModel(new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()), new PositionRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
            this.PersonReposytory = new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
            Allpositions = new ObservableCollection<Position>(SettingsModel.GetAllPosition());

            AddTypeCommand = new RelayCommands(addTypeCommand, Can);
            AddPositionsCommand = new RelayCommands(addPositionsCommand, Can);
            RemovePositionsCommand = new RelayCommands(removePositionsCommand, Can);
        }

        private void removePositionsCommand()
        {
            SettingsModel.RemovePosition(SelectedPositionsToRemove);
            Allpositions.Remove(SelectedPositionsToRemove);
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
                Allpositions = new ObservableCollection<Position>(SettingsModel.GetAllPosition());
                OnPropertyChanged("Allpositions");
                addPosition.Close();
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