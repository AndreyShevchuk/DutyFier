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
            dutyTypes = new DutyTypeRepository().GetAll().ToList();
        }
        public List<DutyType> DutyTypes { get; }
        public DutyType SelectedDutyType { get { return _position.DutyType; } set { _position.DutyType = value; } }
        public string Name { get { return _position.Name; } set { _position.Name = value; } }
        public double Weight { get { return _position.Weight; } set { _position.Weight = value; } }
        public bool IsSeniorPosition { get { return _position.IsSeniorPosition; } set { _position.IsSeniorPosition = value; } }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        //логика добавления в БД
                    }));
            }
        }
    }
}
