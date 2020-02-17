using DutyFier.Client.Wpf.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.State
{
    class PreviewState : IGenerationState
    {
        public UserControl CurrentStateControl { get; set; }
        public GenerateContext Context { get; set; }
       
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public PreviewState(GenerateContext context)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = true;
            this.Context = context;
            CurrentStateControl = new PreView(Context);
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
