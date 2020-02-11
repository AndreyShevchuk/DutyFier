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
        private Dictionary<int, UserControl> userControls = new Dictionary<int, UserControl>();
        private StatisticsView statisticWindow;
        private SettingsView settingsViev;
        private FeedbackView feedbackViev;
        private int generateWindowIndex = 1;
        // private GenerateStepTwo generate2;  /// Нада буде поправити код в generate2
        // private GenerateStepFour generate4;  /// Нада буде поправити код в generate2

        public MainWindow()
        {
            //generate2 = new GenerateStepTwo();
            //generate4 = new GenerateStepFour();
            InitializeComponent();
            //userControls.Add(1, new GenerateStepOne());
            //userControls.Add(2, generate2);
            //userControls.Add(3, new GenerateStepThree());
            var ass = new ChoseExcludedDatesAndHolydaysView();
            ass.DataContext = new ChoseExcludedDatesAndHolydaysViewModel();
            //var sv = new SelectDatesView();
            //sv.DataContext = new SelectDatesViewModel();
            //userControls.Add(4, generate4);
            userControls.Add(1, ass); 
            statisticWindow = new StatisticsView();
            settingsViev = new SettingsView();
            feedbackViev = new FeedbackView();
            GridForWindow.Children.Add(statisticWindow);
            ArrowsStackPanel.Visibility = Visibility.Hidden;
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
        private void ListVievButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListVievButtons.SelectedIndex;
            MoveSelecter(index);
            switch (index)
            {
                case 0:
                    DisplayGenerateWindow(statisticWindow);
                    break;
                case 1:
                    DisplayGenerateWindow(settingsViev);
                    break;
                case 2:
                    DisplayGenerateWindow(userControls[1]);
                    ArrowsStackPanel.Visibility = Visibility.Visible;
                    break;
                case 3:
                    DisplayGenerateWindow(feedbackViev);
                    break;
            }
        }
        private void DisplayGenerateWindow(UserControl userControl)
        {
            ArrowsStackPanel.Visibility = Visibility.Hidden;
            GridForWindow.Children.Clear();
            GridForWindow.Children.Add(userControl);
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
        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (generateWindowIndex > +1)
            {
                generateWindowIndex--;
                GridForWindow.Children.Clear();
                GridForWindow.Children.Add(userControls[generateWindowIndex]);
            }
            else
            {

            }
        }
        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            generateWindowIndex++;
            GridForWindow.Children.Clear();

            if (generateWindowIndex == 3)   // Треба буде видалити коли добавим стани оскільки зараз працює на статичних полях і погано написаному Generate2
            {

            }
            else if (generateWindowIndex == 4)
            {
            }

            GridForWindow.Children.Add(userControls[generateWindowIndex]);


        }
    }
}

