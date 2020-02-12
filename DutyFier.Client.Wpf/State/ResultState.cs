using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    class ResultState : IGenerationState
    {
        public GenerateContext Context { get; set; }
       
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public ResultState(GenerateContext context)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = false;
            this.Context = context;
        }

        public IGenerationState GoBackward()
        {
            return new PreviewState(Context);
        }

        public IGenerationState GoForward()
        {
            return null;
        }
    }
}
