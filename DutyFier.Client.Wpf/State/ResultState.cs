using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    class ResultState : IGenerationState
    {
        public object Context { get; set; }
        public object SelectedDatesContex { get; set; }
        public object DatesExcludingContext { get; set; }
        public object PreviewContext { get; set; }
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public ResultState(object selectedDatesContext, object datesExcludingContext, object previewContext)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = false;
            SelectedDatesContex = selectedDatesContext;
            DatesExcludingContext = datesExcludingContext;
            PreviewContext = previewContext;
        }

        public IGenerationState GoBackward()
        {
            return new PreviewState(SelectedDatesContex, DatesExcludingContext);
        }

        public IGenerationState GoForward()
        {
            return null;
        }
    }
}
