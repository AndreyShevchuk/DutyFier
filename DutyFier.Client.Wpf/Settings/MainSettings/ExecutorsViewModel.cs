using DutyFier.Core.Entities;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity;

namespace DutyFier.Client.Wpf.Settings.MainSettings
{
    internal class ExecutorsViewModel : INotifyPropertyChanged
    {

        private Person selectedExecutor;

        public ExecutorsViewModel()
        {
            // DutyFierContext = MainWindowViewModel.Container.Resolve<DutyFierContext>();
            SettingsModel = new SettingsModel(new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()), new PositionRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
            People = new ObservableCollection<Person>(SettingsModel.GetAllPerson());
            AddExecutorCommand = new RelayCommands(addExecutorCommand, Can);
            RemovePersonCommand = new RelayCommands(removePersonCommand, Can);
        }

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

        public SettingsModel SettingsModel { get; set; }
        public ObservableCollection<Person> People { get; set; }
        public RelayCommands AddExecutorCommand { get; set; }

        public RelayCommands RemovePersonCommand { get; set; }

        private void addExecutorCommand()
        {
            AddPersonView apv = new AddPersonView();
            if (apv.ShowDialog() == true)
            {
                People = new ObservableCollection<Person>(SettingsModel.GetAllPerson());
                OnPropertyChanged("People");
                apv.Close();
            }
        }

        private void removePersonCommand()
        {
            SettingsModel.RemovePerson(SelectedExecutor);
            People.Remove(SelectedExecutor);
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