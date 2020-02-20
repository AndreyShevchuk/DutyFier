using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;


namespace DutyFier.Client.Wpf.Settings
{
    class AddPositionViewModel
    {
        public Position  _position;
        public List<DutyType> dutyTypes;
        private AddPositionModel AddPositionModel { get; set; }
        public AddPositionViewModel()
        {
            _position = new Position();
            AddCommand = new RelayCommands(OnAdd,CanAdd);
            dutyTypes = MainWindowViewModel.Container.Resolve<IRepository<DutyType>>().GetAll().ToList();
            AddPositionModel = new AddPositionModel(MainWindowViewModel.Container.Resolve<IRepository<Position>>());
        }
        public List<DutyType> DutyTypes { get; }
        public DutyType SelectedDutyType { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public bool IsSeniorPosition { get; set; }
        public RelayCommands AddCommand { get; set; }

        private void OnAdd()
        {
            _position = new Position();
            _position.DutyType = SelectedDutyType;
            _position.Name = Name;
            _position.Weight = Weight;
            _position.IsSeniorPosition = IsSeniorPosition;
            AddPositionModel.AddPositionToDB(_position);
        }
        private bool CanAdd()
        {
            return _position != null;
        }
    }
}
