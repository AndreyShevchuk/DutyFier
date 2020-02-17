using DutyFier.Client.Wpf.State;
using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DutyFier.Client.Wpf.Generate
{
    class SelectDatesViewModel : INotifyPropertyChanged
    {
        private GenerateContext generateContext;
        public Dictionary<Position, List<DateTime>> DatesPosition { get; set; }

        private SelectedDatesCollection selectDates;

        private RelayCommand comadAcpet;

        public RelayCommands<SelectedDatesCollection> GetSelectedDatesCollectionComand { get; set; }

        public SelectDatesViewModel(GenerateContext generateContext)
        {
            this.generateContext = generateContext;
            DatesPosition = this.generateContext.PositionsDate;

            GetSelectedDatesCollectionComand = new RelayCommands<SelectedDatesCollection>(SelectDateColetion, x => true );
        }

        private void SelectDateColetion(SelectedDatesCollection obj)
        {
            SelectDates = obj;
        }

        public RelayCommand ComadAcpet
        {
            get
            {
                return comadAcpet ??
                (comadAcpet = new RelayCommand(obj =>
                {
                    DatesPosition[selectPosition].Clear();
                    foreach (var item in selectDates)
                    {
                        DatesPosition[selectPosition].Add(item);
                    }
                },
                (obj) => CheckActivityButton()));
            }
        }

        private bool CheckActivityButton()
        {
            if (selectDates == null)
            {
                return false;
            }
            if (selectPosition == null)
            {
                return false;
            }
            foreach (var item in selectDates)
            {
                if (!DatesPosition[selectPosition].Contains(item))
                {
                    return true;
                }
            }
            if (selectDates.Count != DatesPosition[selectPosition].Count)
            {
                return true;
            }
            return false;
        }

        private RelayCommand comandSelectDates;
        public RelayCommand ComandSelectDates
        {
            get
            {
                return comandSelectDates ??
                (comandSelectDates = new RelayCommand(obj =>
                {
                    if (selectPosition == null) // помилка на дурака
                    {
                        MessageBox.Show("Вибери тип наояду");
                        return;
                    };
                },
                (obj) => true));
            }
        }
        public SelectedDatesCollection SelectDates
        {
            get
            {
                return selectDates;
            }
            set
            {
                selectDates = value;
                OnPropertyChanged("SelectDates");
            }
        }

        private Position selectPosition;
        public Position SelectPosition
        {
            get
            {
                return selectPosition;
            }
            set
            {
                selectPosition = value;
                selectDates.Clear();
                foreach (var item in DatesPosition[selectPosition])
                {
                    selectDates.Add(item);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
