using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    class DatesSelectionState : IGenerationState
    {
        public GenerateContext Context { get; set; }
        public bool IsBackwardStateAllowed { get;  set; }
        public bool IsForwardStateAllowed { get; set; }
        public DatesSelectionState( )
        {
            IsBackwardStateAllowed = false;
            IsForwardStateAllowed = true;
            
        }

        public IGenerationState GoBackward()
        {
            return null;
        }

        public IGenerationState GoForward()
        {
            return new DatesExcludingState(Context);
        }
    }
}
