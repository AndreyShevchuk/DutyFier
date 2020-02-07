using DutyFier.Core.Entities;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Settings
{
    class AddPositionViewModel
    {
        public Position  _position;
        public List<DutyType> dutyTypes;
        public AddPositionViewModel()
        {
            _position = new Position();
            AddCommand = new RelayCommand(OnAdd,CanAdd);
            //dutyTypes = new DutyTypeRepository().GetAll().ToList();
        }
        public List<DutyType> DutyTypes { get; }
        public DutyType SelectedDutyType { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public bool IsSeniorPosition { get; set; }
        public RelayCommand AddCommand { get; set; }

        private void OnAdd()
        {
            _position = new Position();
            _position.DutyType = SelectedDutyType;
            _position.Name = Name;
            _position.Weight = Weight;
            _position.IsSeniorPosition = IsSeniorPosition;
            //TODO add to DB logic
        }
        private bool CanAdd()
        {
            return _position != null;
        }
    }
}
