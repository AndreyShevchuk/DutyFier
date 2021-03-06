﻿using DutyFier.Client.Wpf.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.State
{
    class DatesSelectionState : IGenerationState
    {
        public UserControl CurrentStateControl { get; set; }
        public GenerateContext Context { get; set; }
        public bool IsBackwardStateAllowed { get; set; }
        public bool IsForwardStateAllowed { get; set; }
        public DatesSelectionState()
        {
            Context = new GenerateContext();
            IsBackwardStateAllowed = false;
            IsForwardStateAllowed = true;
            CurrentStateControl = new SelectDatesView(Context);
        }
        public DatesSelectionState(GenerateContext context)
        {
            Context = context;
            IsBackwardStateAllowed = false;
            IsForwardStateAllowed = true;
            CurrentStateControl = new SelectDatesView(Context);
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
