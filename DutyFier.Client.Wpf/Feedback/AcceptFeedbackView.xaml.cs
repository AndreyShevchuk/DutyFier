using DutyFier.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DutyFier.Client.Wpf.Feedback
{
    /// <summary>
    /// Логика взаимодействия для AcceptFeedbackView.xaml
    /// </summary>
    public partial class AcceptFeedbackView : Window
    {
        FeedbackViewModel.AcceptFeedbackViewClosingTrigger reedFeedbackContext;
        public FeedbacksContext FeedbacksContext { get; set; }
        public AcceptFeedbackView(Core.Entities.Duty duty, 
            FeedbacksContext feedbacksContext, 
            FeedbackViewModel.AcceptFeedbackViewClosingTrigger reedFeedbackContext)
        {
            InitializeComponent();
            FeedbacksContext = FeedbacksContext;
            DataContext = new AcceptFeedbackViewModel(duty, feedbacksContext);
            this.reedFeedbackContext = reedFeedbackContext;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            reedFeedbackContext?.Invoke();
        }
    }
}
