using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DutyFier.Client.Wpf.Settings.Infrastructure
{
    class GroupDataGridCheckBoxColumn :DataGridCheckBoxColumn
    {
        private readonly Position position;
        SettingsViewModel viewModel;
        public GroupDataGridCheckBoxColumn(Position position, SettingsViewModel personDutyViewModel)
            : base()
        {
            this.position = position;
            viewModel = personDutyViewModel;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            CheckBox checkBox = base.GenerateElement(cell, dataItem) as CheckBox;
            checkBox.IsHitTestVisible = true;
            checkBox.Checked += CheckBox_Checked;
            checkBox.Unchecked += CheckBox_Unchecked;

            /* Set Command binding */
            Binding commandBinding = new Binding("DataContext.AddOrRemoveGroupCommand");
            commandBinding.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ItemsControl), 3);
            checkBox.SetBinding(CheckBox.CommandProperty, commandBinding);

            /* Set Command parameter */
            MultiBinding commandParameterBinding = new MultiBinding();
            commandParameterBinding.Converter = new CommandParameterMultiConverter();
            commandParameterBinding.Bindings.Add(new Binding("IsChecked") { RelativeSource = RelativeSource.Self });
            commandParameterBinding.Bindings.Add(new Binding(".")); //the user object
            commandParameterBinding.Bindings.Add(new Binding(".") { Source = this.position }); //the group object
            checkBox.SetBinding(CheckBox.CommandParameterProperty, commandParameterBinding);

            return checkBox;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // viewModel._addOrRemoveGroupCommand.Execute(new object[]{ false, , );
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // viewModel._addOrRemoveGroupCommand.Execute(new object[] { true, , );
        }
    }
}

