﻿using DutyFier.Client.Wpf.Feedback;
using DutyFier.Client.Wpf.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DutyFier.Client.Wpf.Generate;
using System.Windows.Controls;
using DutyFier.Client.Wpf.Statistics;
using DutyFier.Client.Wpf.Settings;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity;
using DutyFier.Core.Repository;
using DutyFier.Core.Interfaces;
using DutyFier.Core.Entities;
using Unity.Lifetime;
using Unity.Interception;
using Unity.Interception.Interceptors.InstanceInterceptors.TransparentProxyInterception;

namespace DutyFier.Client.Wpf
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private UIElementCollection childrenPanel;
        private IGenerationState state;
        private GenerateContext generateContext;
        private Visibility visibility;
        private bool isForwardEnable;
        private bool isBackwardEnable;
        private WindowState windowState;
        private bool isDarkModeOn;
        public static UnityContainer Container { get; set; }
        public Visibility IsVisible
        {
            get
            {
                return visibility;
            }

            set
            {
                visibility = value;
                OnPropertyChanged("IsVisible");
            }
        }

        public bool IsForwardEnable
        {
            get
            {
                return isForwardEnable;
            }

            set
            {
                isForwardEnable = value;
                OnPropertyChanged("IsForwardEnable");
            }
        }
        public bool IsBackwardEnable
        {
            get
            {
                return isBackwardEnable;
            }

            set
            {
                isBackwardEnable = value;
                OnPropertyChanged("IsBackwardEnable");
            }
        }

        public WindowState WindowState
        {
            get
            {
                return windowState;
            }

            set
            {
                windowState = value;
                OnPropertyChanged("WindowState");
            }
        }
        public bool IsDarkModeOn
        {
            get
            {
                return isDarkModeOn;
            }

            set
            {
                isDarkModeOn = value;
                OnPropertyChanged("IsDarkModeOn");
            }
        }
        public RelayCommands<UIElementCollection> StatisticsCommand { get; set; }
        public RelayCommands<UIElementCollection> SettingCommand { get; set; }
        public RelayCommands<UIElementCollection> SelectDatesCommand { get; set; }
        public RelayCommands<UIElementCollection> FeedbackCommand { get; set; }

        public RelayCommands<UIElementCollection> ForwardCommand { get; set; }
        public RelayCommands<UIElementCollection> BackwardCommand { get; set; }
        public RelayCommands MinimizeCommand { get; set; }
        public RelayCommands ChangeWindowStateCommand { get; set; }
        public RelayCommands DarkModeCommand { get; set; }
        public RelayCommands PowerOffCommand { get; set; }
        public MainWindowViewModel()
        {
            StatisticsCommand = new RelayCommands<UIElementCollection>(statisticCommand, CanChange);
            SettingCommand = new RelayCommands<UIElementCollection>(settingsCommand, CanChange);
            SelectDatesCommand = new RelayCommands<UIElementCollection>(generateCommand, CanChange);
            FeedbackCommand = new RelayCommands<UIElementCollection>(feedbackCommand, CanChange);
            ForwardCommand = new RelayCommands<UIElementCollection>(forwardCommand, CanForward);
            BackwardCommand = new RelayCommands<UIElementCollection>(backwardCommand, CanBackward);
            MinimizeCommand = new RelayCommands(minimizeCommand, Can);
            ChangeWindowStateCommand = new RelayCommands(changeWindowStateCommand, Can);
            DarkModeCommand = new RelayCommands(darkModeCommand, Can);
            PowerOffCommand = new RelayCommands(powerOffCommand, Can);
            state = new DatesSelectionState();
            IsBackwardEnable = false;
            IsForwardEnable = true;
            isBackwardEnable = false;
            IsDarkModeOn = false;
            IsVisible = Visibility.Hidden;

            Container = new UnityContainer();

            Container.RegisterType<IRepository<DaysOfWeekWeight>, DaysOfWeekWeightRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Duty>, DutyRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<DutyType>, DutyTypeRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<PersonDutyFeedback>, PersonDutyFeedbackRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Position>, PositionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Person>, PersonRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Executor>, ExecuterRepository>(new ContainerControlledLifetimeManager());
            Container.AddNewExtension<Interception>();
            Container.Configure<Interception>()
                .SetInterceptorFor<IRepository<DaysOfWeekWeight>>(new TransparentProxyInterceptor());
            Container.Configure<Interception>()
                .SetInterceptorFor<IRepository<DutyType>>(new TransparentProxyInterceptor());
            Container.Configure<Interception>()
                .SetInterceptorFor<IRepository<PersonDutyFeedback>>(new TransparentProxyInterceptor());
            Container.Configure<Interception>()
                .SetInterceptorFor<IRepository<Position>>(new TransparentProxyInterceptor());
            Container.Configure<Interception>()
                .SetInterceptorFor<IRepository<Person>>(new TransparentProxyInterceptor());
            Container.Configure<Interception>()
                .SetInterceptorFor<IRepository<Executor>>(new TransparentProxyInterceptor());
            Container.Configure<Interception>()
                .SetInterceptorFor<IRepository<Duty>>(new TransparentProxyInterceptor());
            
        }

        public void powerOffCommand()
        {
            Application.Current.Shutdown();
        }
        public void darkModeCommand()
        {
            if (isDarkModeOn)
            {
                Uri uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/materialdesigntheme.dark.xaml");
                System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(0);
                System.Windows.Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary() { Source = uri });
                Uri uri2 = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/materialdesigncolor.teal.xaml");
                System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(2);
                System.Windows.Application.Current.Resources.MergedDictionaries.Insert(2, new ResourceDictionary() { Source = uri2 });
                IsDarkModeOn = true;
            }
            else
            {
                Uri uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/materialdesigntheme.light.xaml");
                System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(0);
                System.Windows.Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary() { Source = uri });
                Uri uri2 = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/materialdesigncolor.blue.xaml");
                System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(2);
                System.Windows.Application.Current.Resources.MergedDictionaries.Insert(2, new ResourceDictionary() { Source = uri2 });
                IsDarkModeOn = false;
            }
        }
        public void changeWindowStateCommand()
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
        public void minimizeCommand()
        {
            WindowState = WindowState.Minimized;
        }
        public void statisticCommand(UIElementCollection obj)
        {
            childrenPanel = obj;
            var statistic = new StatisticsView();
            childrenPanel.Clear();
            childrenPanel.Add(statistic);
            IsVisible = Visibility.Hidden;

        }
        public void settingsCommand(UIElementCollection obj)
        {
            childrenPanel = obj;
            var settings = new SettingsView();
            childrenPanel.Clear();
            childrenPanel.Add(settings);
            IsVisible = Visibility.Hidden;
        }
        public void generateCommand(UIElementCollection obj)
        {

            childrenPanel = obj;
           
            childrenPanel.Clear();
            childrenPanel.Add(state.CurrentStateControl);
            IsVisible = Visibility.Visible;
        }
        public void feedbackCommand(UIElementCollection obj)
        {
            childrenPanel = obj;
            var feedback = new FeedbackView();
            childrenPanel.Clear();
            childrenPanel.Add(feedback);

            IsVisible = Visibility.Hidden;
        }
        public bool CanChange(UIElementCollection obj)
        {
            return true;
        }
        public void forwardCommand(UIElementCollection obj)
        {
            state = state.GoForward();
            IsForwardEnable = state.IsForwardStateAllowed;
            IsBackwardEnable = state.IsBackwardStateAllowed;
            childrenPanel = obj;
            childrenPanel.Clear();
            childrenPanel.Add(state.CurrentStateControl);
        }
        public void backwardCommand(UIElementCollection obj)
        {
            state = state.GoBackward();
            IsBackwardEnable = state.IsBackwardStateAllowed;
            IsForwardEnable = state.IsForwardStateAllowed;
            childrenPanel = obj;
            childrenPanel.Clear();
            childrenPanel.Add(state.CurrentStateControl);
        }
        public bool CanForward(UIElementCollection obj)
        {
            return true;
        }
        public bool CanBackward(UIElementCollection obj)
        {
            return true;
        }
        public bool Can()
        {
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
