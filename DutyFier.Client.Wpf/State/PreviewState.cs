using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    class PreviewState : IGenerationState
    {
        public GenerateContext Context { get; set; }
       
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public PreviewState(GenerateContext context)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = true;
            this.Context = context;
        }

        public IGenerationState GoBackward()
        {
            return new DatesExcludingState(Context);
        }

        public IGenerationState GoForward()
        {
            return new ResultState(Context);
        }
    }
}
