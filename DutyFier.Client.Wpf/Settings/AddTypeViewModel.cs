using DutyFier.Core.Entities;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DutyFier.Client.Wpf.Settings
{
    class AddTypeViewModel : INotifyPropertyChanged
    {
        public DutyType _dutyType;
        private AdTypeModel AddTypeModel;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public AddTypeViewModel()
        {
            _dutyType = new DutyType();
            AddCommand = new RelayCommands(OnAdd, ()=>true);
            AddTypeModel = new AdTypeModel(MainWindowViewModel.Container.Resolve<IRepository<DutyType>>());
        }
        public string Name { get; set; }
        public RelayCommands AddCommand { get; set; }

        public void OnAdd()
        {
            if (Name != null && !Name.Equals(""))
            {
                _dutyType = new DutyType();
                _dutyType.Name = Name;
                Name = "";
                OnPropertyChanged(nameof(Name));
                AddTypeModel.AddDutyTypeToDB(_dutyType);
            }
        }
        public bool CanAdd()
        {
            return true;
        }
    }
}
