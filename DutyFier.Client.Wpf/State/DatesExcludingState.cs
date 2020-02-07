using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    class DatesExcludingState : IGenerationState
    {
        public object Context { get; set; }
        public object SelectedDatesContex { get; set; }
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public DatesExcludingState(object contextDates)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = true;
            SelectedDatesContex = contextDates;
        }

        public IGenerationState GoBackward()
        {
            return new DatesSelectionState();
        }

        public IGenerationState GoForward()
        {
            return new PreviewState();
        }
    }
}
