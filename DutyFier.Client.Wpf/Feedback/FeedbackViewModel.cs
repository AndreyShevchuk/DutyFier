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

namespace DutyFier.Client.Wpf.Feedback
{
    class FeedbackViewModel : INotifyPropertyChanged
    {
        private IRepository<Duty> DutyRepository { get; set; }
        public DutyModel[] DutyModels
        {
            get => DutyModel.GetDutiesWitchHasNoFeedbacks(DutyRepository);
        }
        public FeedbackViewModel()
        {
            DutyRepository = new DutyRepository(MainWindowViewModel.Container.Resolve<DutyFierContext>());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
