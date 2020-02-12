using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    class DatesExcludingState : IGenerationState
    {
        public GenerateContext Context { get; set; }
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public DatesExcludingState()
        {
                
        }
        public DatesExcludingState( GenerateContext context)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = true;
            this.Context = context;
        }

        public IGenerationState GoBackward()
        {
            return new DatesSelectionState();
        }

        public IGenerationState GoForward()
        {
            return new PreviewState(Context);
        }
    }
}
