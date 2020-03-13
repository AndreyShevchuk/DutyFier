using DutyFier.Client.Wpf.Feedback;
using DutyFier.Client.Wpf.Generate;
using DutyFier.Client.Wpf.Settings;
using DutyFier.Client.Wpf.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DutyFier.Client.Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
        private void MoveSelecter(int index)
        {
            TransitionEff.OnApplyTemplate();
            GridSelecter.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        private void ListVievButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoveSelecter(ListVievButtons.SelectedIndex);
        }

        private void Window1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

       
    }
}

