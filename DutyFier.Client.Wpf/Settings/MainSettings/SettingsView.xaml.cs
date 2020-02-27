using System.Windows.Controls;

namespace DutyFier.Client.Wpf.Settings
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private SettingsViewModel svw;

        public SettingsView()
        {
            svw = new SettingsViewModel();
            InitializeComponent();
            DataContext = svw;
        }
    }
}