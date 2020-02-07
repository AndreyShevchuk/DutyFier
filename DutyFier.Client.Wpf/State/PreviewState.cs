using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    class PreviewState : IGenerationState
    {
        public object Context { get; set; }
        public object SelectedDatesContex { get; set; }
        public object DatesExcludingContext { get; set; }
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public PreviewState(object selectedDatesContext, object datesExcludingContext)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = true;
            SelectedDatesContex =selectedDatesContext;
            DatesExcludingContext = datesExcludingContext;
        }

        public IGenerationState GoBackward()
        {
            return new DatesExcludingState(DatesExcludingContext);
        }

        public IGenerationState GoForward()
        {
            return new ResultState(SelectedDatesContex,DatesExcludingContext,Context);
        }
    }
}
