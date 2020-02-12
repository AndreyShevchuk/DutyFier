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
            StatisticsCommand = new RelayCommand(statisticsCommand, Can);
            SettingsCommand = new RelayCommand(settingsCommand, Can);
            GenerateCommand = new RelayCommand(generateCommand, Can);
            FeedbackCommand = new RelayCommand(feedbackCommand, Can);
            ForwardCommand = new RelayCommand(forwardCommand, CanForward);
            BackwardCommand = new RelayCommand(backwardCommand, CanBacward);
            state =  new DatesSelectionState();
        }

        public RelayCommand ForwardCommand { get; set; }
        public RelayCommand BackwardCommand { get; set; }
        public RelayCommand StatisticsCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand GenerateCommand { get; set; }
        public RelayCommand FeedbackCommand { get; set; }
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
