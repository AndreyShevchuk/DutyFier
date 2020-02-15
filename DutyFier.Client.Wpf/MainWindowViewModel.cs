using DutyFier.Client.Wpf.Feedback;
using DutyFier.Client.Wpf.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DutyFier.Client.Wpf.Generate;
using System.Windows.Controls;
using DutyFier.Client.Wpf.Statistics;
using DutyFier.Client.Wpf.Settings;

namespace DutyFier.Client.Wpf
{
    class MainWindowViewModel
    {
        private UIElementCollection childrenPanel;
        private IGenerationState state;
        private GenerateContext generateContext;
        public Visibility IsVisible { get; set; }
        public bool IsForwardEnable{ get; set; }
        public bool IsBackwardEnable { get; set; }
        public RelayCommands<UIElementCollection> StatisticsCommand { get; set; }
        public RelayCommands<UIElementCollection> SettingCommand { get; set; }
        public RelayCommands<UIElementCollection> SelectDatesCommand { get; set; }
        public RelayCommands<UIElementCollection> FeedbackCommand { get; set; }

        public RelayCommands<UIElementCollection> ForwardCommand { get; set; }
        public RelayCommands<UIElementCollection> BackwardCommand { get; set; }

        public MainWindowViewModel()
        {
            StatisticsCommand = new RelayCommands<UIElementCollection>(statisticCommand, CanChange);
            SettingCommand = new RelayCommands<UIElementCollection>(settingsCommand, CanChange);
            SelectDatesCommand = new RelayCommands<UIElementCollection>(generateCommand, CanChange);
            FeedbackCommand = new RelayCommands<UIElementCollection>(feedbackCommand, CanChange);
            ForwardCommand =  new RelayCommands<UIElementCollection>(forwardCommand, CanForward);
            BackwardCommand = new RelayCommands<UIElementCollection>(backwardCommand, CanBacward);
            state = new DatesSelectionState();
            IsBackwardEnable = false;
            IsForwardEnable = true;
        }

        private void statisticCommand(UIElementCollection obj)
        {
            childrenPanel = obj;
            var statistic = new StatisticsView();
            childrenPanel.Clear();
            childrenPanel.Add(statistic);

            IsVisible = Visibility.Hidden;

        }
        public void settingsCommand(UIElementCollection obj)
        {
            childrenPanel = obj;
            var settings = new SettingsView();
            childrenPanel.Clear();
            childrenPanel.Add(settings);
            
            IsVisible = Visibility.Hidden;
        }
        public void generateCommand(UIElementCollection obj)
        {

            childrenPanel = obj;
            var generate = new SelectDatesView();
            childrenPanel.Clear();
            childrenPanel.Add(generate);
            IsVisible = Visibility.Visible;
        }
        public void feedbackCommand(UIElementCollection obj)
        {
            childrenPanel = obj;
            var feedback= new FeedbackView();
            childrenPanel.Clear();
            childrenPanel.Add(feedback);

            IsVisible = Visibility.Hidden;
        }
        public bool CanChange(UIElementCollection obj)
        {
            return true;
        }
        UIElementCollection obj;
        public void forwardCommand(UIElementCollection obj)
        {

            state = state.GoForward();
            IsForwardEnable = state.IsForwardStateAllowed;
            childrenPanel = obj;
            childrenPanel.Clear();
            childrenPanel.Add(state.UK);
        }
        public void backwardCommand(UIElementCollection obj)
        {
            state = state.GoBackward();
            IsBackwardEnable = state.IsBackwardStateAllowed;
            childrenPanel = obj;
            childrenPanel.Clear();
            childrenPanel.Add(state.UK);
        }
        public bool CanForward(UIElementCollection obj)
        {
            return true;
        }
        public bool CanBacward(UIElementCollection obj)
        {
            return true;
        }

    }
}
