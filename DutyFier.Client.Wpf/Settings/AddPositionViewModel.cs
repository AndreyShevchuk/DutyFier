using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Settings
{
    class AddPositionViewModel
    {
        Position _position;
        public AddPositionViewModel()
        {
            _position = new Position();
        }
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
