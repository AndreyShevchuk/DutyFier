﻿using DutyFier.Client.Wpf.State;
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


namespace DutyFier.Client.Wpf.Generate
{
    /// <summary>
    /// Логика взаимодействия для SelectDatesView.xaml
    /// </summary>
    public partial class SelectDatesView : UserControl
    {
        //public SelectDatesView()
        //{
        //    InitializeComponent();
        //    DataContext = new SelectDatesViewModel();
        //}
        public SelectDatesView(GenerateContext generateContext)
        {
            InitializeComponent();
            DataContext = new SelectDatesViewModel(generateContext);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PosBox.Items.Refresh();
        }
    }
}
