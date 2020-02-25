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


namespace DutyFier.Client.Wpf.Settings
{
    class AddPositionViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public Position  _position;
        public List<DutyType> _dutyTypes;
        private AddPositionModel AddPositionModel { get; set; }
        public AddPositionViewModel()
        {
            _position = new Position();
            AddCommand = new RelayCommands(OnAdd,()=>true);
            DutyTypes = new DutyTypeRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()).GetAll().ToList();
            AddPositionModel = new AddPositionModel(new PositionRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>()));
        }
        public List<DutyType> DutyTypes { get => _dutyTypes; set => _dutyTypes = value; }
        public DutyType SelectedDutyType { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public bool IsSeniorPosition { get; set; }
        public RelayCommands AddCommand { get; set; }

        private void OnAdd()
        {
            if (!Name.Equals("") && SelectedDutyType != null && Weight > 0)
            {
                _position = new Position();
                _position.DutyType = SelectedDutyType;
                SelectedDutyType = null;
                OnPropertyChanged(nameof(SelectedDutyType));
                _position.Name = Name;
                Name = "";
                OnPropertyChanged(nameof(Name));
                _position.Weight = Weight;
                Weight = 0;
                OnPropertyChanged(nameof(Weight));
                _position.IsSeniorPosition = IsSeniorPosition;
                IsSeniorPosition = false;
                OnPropertyChanged(nameof(IsSeniorPosition));
                AddPositionModel.AddPositionToDB(_position);
            }
        }
    }
}
