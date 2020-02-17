using DutyFier.Client.Wpf.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.State
{
    class DatesExcludingState : IGenerationState
    {
        public UserControl UK { get; set; }
        public GenerateContext Context { get; set; }
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        
        public DatesExcludingState( GenerateContext context)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = true;
            this.Context = context;
            UK = new ChoseExcludedDatesAndHolydaysView(context);
        }

        public IGenerationState GoBackward()
        {
            return new DatesSelectionState(Context);
        }

        public IGenerationState GoForward()
        {
            return new PreviewState(Context);
        }
    }
}
