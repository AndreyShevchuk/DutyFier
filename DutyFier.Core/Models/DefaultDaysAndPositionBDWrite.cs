using DutyFier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DutyFier.Core.Models
{
    public static class DefaultDaysAndPositionBDWrite
    {
        public static void WriteToData( DutyFierContext context)
        {
            var tt = context;
            var listDutyType = new List<DutyType>()
            {
                new DutyType()
                {
                    Name = "Курс"
                },
                new DutyType()
                {
                    Name = "ПЧФ"
                },
                new DutyType()
                {
                    Name = "Великий наряд"
                },
                new DutyType()
                {
                    Name = "БП"
                }
            };
            if (!tt.DutyTypes.Any())
            {
                tt.DutyTypes.AddRange(listDutyType);
                tt.SaveChanges();
            }


            var ListDateWeight = new List<DaysOfWeekWeight>() {new DaysOfWeekWeight { Day = DayOfWeek.Monday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Tuesday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Wednesday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Thursday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Saturday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Friday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Sunday, Weight = 1 }
            };
            if (!tt.DaysOfWeekWeights.Any())
            {
                tt.DaysOfWeekWeights.AddRange(ListDateWeight);
                tt.SaveChanges();
            }

            if (!tt.Positions.Any())
            {
                listDutyType = tt.DutyTypes.ToList();
                var listPosition = new List<Position>()
                {
                    new Position()
                    {
                        Name = "Черновий курсу",
                        IsSeniorPosition = true,
                        Weight = 2.6,
                        DutyType = listDutyType[0],
                        DefaultPositionCount = 1
                    },
                    new Position()
                    {
                        Name = "Днювальний курсу",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[0],
                        DefaultPositionCount = 3
                    },
                    new Position()
                    {
                        Name = "ПЧІ",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2],
                        DefaultPositionCount = 1
                    },
                    new Position()
                    {
                        Name = "ПЧФ",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[1],
                        DefaultPositionCount = 1
                    },
                    new Position()
                    {
                        Name = "Днювальний парку",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2],
                        DefaultPositionCount = 1
                    },
                    new Position()
                    {
                        Name = "Черговий КПП-1",
                        IsSeniorPosition = true,
                        Weight = 2.6,
                        DutyType = listDutyType[2],
                        DefaultPositionCount = 1
                    },
                    new Position()
                    {
                        Name = "Помічник чергового КПП-1",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2],
                        DefaultPositionCount = 3
                    },
                    new Position()
                    {
                        Name = "Черговий КПП-2",
                        IsSeniorPosition = true,
                        Weight = 2.6,
                        DutyType = listDutyType[2],
                        DefaultPositionCount = 1
                    },
                    new Position()
                    {
                        Name = "Помічник чергового КПП-2",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2],
                        DefaultPositionCount = 1
                    },
                    new Position()
                    {
                        Name = "Черговий спортзалу",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2],
                        DefaultPositionCount = 1
                    },

                    new Position()
                    {
                        Name = "Черговий БП",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[3],
                        DefaultPositionCount = 1
                    },

                    new Position()
                    {
                        Name = "Помічник чергового БП",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[3],
                        DefaultPositionCount = 1
                    },
                    new Position()
                    {
                        Name = "Патрульний ВСП",
                        IsSeniorPosition = false,
                        Weight = 1.5,
                        DutyType = listDutyType[1],
                        DefaultPositionCount = 6
                    }
                };
                tt.Positions.AddRange(listPosition);
                tt.SaveChanges();
            }
        }
    }
}
