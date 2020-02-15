using DutyFier.Client.Wpf.Feedback;
using DutyFier.Client.Wpf.Generate;
using DutyFier.Client.Wpf.Settings;
using DutyFier.Client.Wpf.Statistics;
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
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void MoveSelecter(int index)
        {
            TransitionEff.OnApplyTemplate();
            GridSelecter.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }
        private void DarkMode_OnChecked(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/materialdesigntheme.dark.xaml");
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary() { Source = uri });
            Uri uri2 = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/materialdesigncolor.teal.xaml");
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(2);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(2, new ResourceDictionary() { Source = uri2 });
        }
        private void DarkMode_OnUnchecked(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/materialdesigntheme.light.xaml");
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary() { Source = uri });
            Uri uri2 = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/materialdesigncolor.blue.xaml");
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(2);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(2, new ResourceDictionary() { Source = uri2 });
        }
        
        

        private void MaximixeWindow_OnClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void ButtonPower_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

