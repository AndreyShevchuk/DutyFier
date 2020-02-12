using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyFier.Client.Wpf.State
{
    interface IGenerationState
    {
        GenerateContext Context { get; set; }
        bool IsBackwardStateAllowed { get; set; }
        bool IsForwardStateAllowed { get; set; }
        IGenerationState GoForward();
        IGenerationState GoBackward();
    }
}
