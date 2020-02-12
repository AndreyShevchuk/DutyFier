using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.Generate
{
    class PreViewModel
    {
        public ObservableCollection<DutyRequest> DutyRequests { get; set; }
        public DutyRequest SelectDutyReqest { get; set; }


        public PreViewModel()
        {
            DutyRequests = new ObservableCollection<DutyRequest>();
        }
    }
}
