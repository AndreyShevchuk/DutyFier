﻿
using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DutyFier.Client.Wpf.Settings
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public PersonRepository PersonReposytory { get; }

        private DutyFierContext DutyFierContext;
        List<Person> people;
        private List<Position> allpositions;
        private Person selectedPerson;
        private List<Position> positions;
        public SettingsModel SettingsModel { get; set; }
        public List<Person> People { get => people; set => people = value; }
        public List<Position> Positions
        {
            get { return positions; }
        }
        public Position SelectedPositionToRemove { get; set; }
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
        public RelayCommands RemovePositionCommand { get; set; }
        public RelayCommands AddExecutor { get; set; }
        public SettingsViewModel()
        {
            DutyFierContext = new DutyFierContext();
            this.PersonReposytory = new PersonRepository();
            people = DutyFierContext.Persons.ToList();
            allpositions = DutyFierContext.Positions.ToList();
            AddPositionCommand = new RelayCommands(addPositionsCommand, Can);
            RemovePositionCommand = new RelayCommands(removePositionCommand, Can);
            AddExecutor = new RelayCommands(addExecutor, Can);

            SettingsModel = new SettingsModel(new PersonRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()),new PositionRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
        }
        private void addExecutor()
        {
            AddPersonView apv = new AddPersonView();
            if (apv.ShowDialog() == true)
            {
                People = DutyFierContext.Persons.ToList();
                OnPropertyChanged("People");
                apv.Close();
            }
        }
        private void removePositionCommand()
        {
            SelectedPerson.Positions.Remove(SelectedPositionToRemove);
            positions = SelectedPerson.Positions.ToList();
            positions.ForEach(a => a.Persons.Remove(SelectedPerson));
            OnPropertyChanged("Positions");
            DutyFierContext.SaveChanges();
            SettingsModel.UpdatePersonDependencyToPosition(SelectedPerson); //TODO fix logic to remove from DB
            SettingsModel.UpdatePositionDependencyToPerson(positions);
        }
        private void addPositionsCommand()
        {
            //TODO fix logic to remove from DB
            SelectedPerson.Positions.Add(SelectedPosition);
            DutyFierContext.SaveChanges();
            positions = SelectedPerson.Positions.ToList();
            positions.ForEach(a => a.Persons.Add(SelectedPerson));
            OnPropertyChanged("Positions");
            SettingsModel.UpdatePersonDependencyToPosition(SelectedPerson);
            SettingsModel.UpdatePositionDependencyToPerson(positions);

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

        private void SaveChange(List<Person> Persons)
        {
            foreach (var item in Persons)
            {
                //DutyFierContext.Entry(item).State = EntityState.Modified;
                DutyFierContext.SaveChanges();
            }

        }
    }
}

