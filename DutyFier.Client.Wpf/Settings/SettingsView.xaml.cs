

using DutyFier.Core.Entities;

using System.Windows.Controls;
using System.Windows.Data;

namespace DutyFier.Client.Wpf.Settings
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        SettingsViewModel svw;
        public SettingsView()
        {
            svw = new SettingsViewModel();
            InitializeComponent();
            DataContext = svw;
        }

        private void AddCadetButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddPersonView apv = new AddPersonView();
            if (apv.ShowDialog()==true)
            {

            }
        }

        private void AddPositionButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddPositionView addPosition = new AddPositionView();
            if(addPosition.ShowDialog() == true)
            {

            }
        }

        private void AddTypeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddTypeView addType = new AddTypeView();
            if(addType.ShowDialog() == true)
            {

            }
        }
    }
}
