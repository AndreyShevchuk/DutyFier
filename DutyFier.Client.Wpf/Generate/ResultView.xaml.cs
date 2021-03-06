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
    /// Логика взаимодействия для ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl
    {
        public ResultView(GenerateContext context)
        {
            context.GeneratorRunWhereNoDutys();
            InitializeComponent();
            DataContext = new ResultViewModel(context);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
