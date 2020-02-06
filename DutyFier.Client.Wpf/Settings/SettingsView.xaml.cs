
using DutyFier.Client.Wpf.Settings.Infrastructure;
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
        private SettingsViewModel personDutyViewModel;
        public SettingsView()
        {
            personDutyViewModel = new SettingsViewModel();
            InitializeComponent();
            this.DataContext = personDutyViewModel;
        }

        private void CreateDataGrid()
        {
            this.dataGrid.AutoGenerateColumns = false;
            this.dataGrid.CanUserAddRows = false;

            /* Add a column for the displaying the full name of the user */
            this.dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Person",
                Binding = new Binding("LastName")
                {
                    Mode = BindingMode.OneWay
                }
            });

            /* Add a column for each group */
            foreach (Position position in personDutyViewModel.Positions)
            {
                //DataGridCheckBoxColumn chkBoxColumn = new DataGridCheckBoxColumn();

                GroupDataGridCheckBoxColumn chkBoxColumn =
                    new GroupDataGridCheckBoxColumn(position, personDutyViewModel);
                chkBoxColumn.Header = position.Name;

                Binding binding = new Binding("Positions");
                PositionToBooleanConverter converter = new PositionToBooleanConverter();
                binding.Converter = (IValueConverter)converter;
                binding.ConverterParameter = position;
                binding.Mode = BindingMode.OneWay;
                chkBoxColumn.Binding = binding;

                this.dataGrid.Columns.Add(chkBoxColumn);
            }

            /* Bind the ItemsSource property of the DataGrid to the Users collection */
            this.dataGrid.SetBinding(DataGrid.ItemsSourceProperty, "Persons");
        }

    }
}
