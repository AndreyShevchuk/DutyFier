using DutyFier.Client.Wpf.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.State
{
    class ResultState : IGenerationState
    {
        public UserControl CurrentStateControl { get; set; }
        public GenerateContext Context { get; set; }
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public ResultState(GenerateContext context)
        {
            IsBackwardStateAllowed = true;
            IsForwardStateAllowed = false;
            this.Context = context;
            CurrentStateControl = new ResultView(context);
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
