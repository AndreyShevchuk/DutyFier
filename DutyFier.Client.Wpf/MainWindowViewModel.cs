using DutyFier.Client.Wpf.Feedback;
using DutyFier.Client.Wpf.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DutyFier.Client.Wpf.Generate;

namespace DutyFier.Client.Wpf
{
    class MainWindowViewModel
    {
        IGenerationState state;
        GenerateContext generateContext;
        SelectDatesView datesSelectionView;
        ChoseExcludedDatesAndHolydaysView choseExcludedDates;
        PreView preView;
        ResultView resultView;

        public MainWindowViewModel()
        {
            StatisticsCommand = new RelayCommands(statisticsCommand, Can);
            SettingsCommand = new RelayCommands(settingsCommand, Can);
            GenerateCommand = new RelayCommands(generateCommand, Can);
            FeedbackCommand = new RelayCommands(feedbackCommand, Can);
            ForwardCommand = new RelayCommands(forwardCommand, CanForward);
            BackwardCommand = new RelayCommands(backwardCommand, CanBacward);
            state =  new DatesSelectionState();
        }

        public RelayCommands ForwardCommand { get; set; }
        public RelayCommands BackwardCommand { get; set; }
        public RelayCommands StatisticsCommand { get; set; }
        public RelayCommands SettingsCommand { get; set; }
        public RelayCommands GenerateCommand { get; set; }
        public RelayCommands FeedbackCommand { get; set; }
        public void forwardCommand()
        {
           
           state = state.GoForward();
        }
        public void backwardCommand()
        {
          state = state.GoBackward();
        }
        public void statisticsCommand()
        {
            MessageBox.Show("shegelme");
        }
        public void settingsCommand()
        {
            MessageBox.Show("shegelme");
        }
        public void generateCommand()
        {
            MessageBox.Show("shegelme");
        }
        public void feedbackCommand()
        {
            MessageBox.Show("shegelme");
        }

        public bool Can()
        {
            return true;
        }
        public bool CanForward()
        {
            return state.IsForwardStateAllowed;
        }
        public bool CanBacward()
        {
            return state.IsBackwardStateAllowed;
        }

    }
}
