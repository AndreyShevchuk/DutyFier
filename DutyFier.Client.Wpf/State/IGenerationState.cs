using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.State
{
    interface IGenerationState
    {
        UserControl UK { get; set; } 
        GenerateContext Context { get; set; }
        bool IsBackwardStateAllowed { get; set; }
        bool IsForwardStateAllowed { get; set; }
        IGenerationState GoForward();
        IGenerationState GoBackward();
    }
}
