﻿using DutyFier.Core.Entities;
using DutyFier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutyFier.Core.Models
{
    public class SeedData
    {
        public static void StartData()
        {

            var lisPerson = new List<Person>()
            {
                new Person
                {
                    FirstName = "Batienko",
                    LastName = "Andree"
                },

                new Person
                {
                    FirstName = "Vinitckiy",
                    LastName = "Sergey"
                },

                new Person
                {
                    FirstName = "Getmanenko",
                    LastName = "Vitaliy"
                },

                new Person
                {

                    FirstName = "Juruk",
                    LastName = "Artem"
                },

                new Person
                {
                    FirstName = "Juravel",
                    LastName = "Andree"
                },

                new Person
                {
                    FirstName = "Kovalenko",
                    LastName = "Andree"
                },

                new Person
                {
                    FirstName = "Salykov",
                    LastName = "Illy"
                },

                new Person
                {
                    FirstName = "Skrupnik",
                    LastName = "Daniil"
                },

                new Person
                {
                    FirstName = "Fedor",
                    LastName = "Vitaly"
                },

                new Person
                {
                    FirstName = "Shevchuk",
                    LastName = "Andrey"
                },

                new Person
                {
                    FirstName = "Shevchuk",
                    LastName = "Nikolay"
                },

                new Person
                {
                    FirstName = "Shkorupskiy",
                    LastName = "Valintin"
                },

                new Person
                {
                    FirstName = "Zlobin",
                    LastName = "Oleksandr"
                },

                new Person
                {
                    FirstName = "Gumenuk",
                    LastName = "Oleksandr"
                }
            };

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

            var pr = new PersonRepository();

            var ListDateWeight = (new DaysOfWeekWeight { Day = DayOfWeek.Monday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Tuesday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Wednesday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Thursday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Saturday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Friday, Weight = 1 },
                new DaysOfWeekWeight { Day = DayOfWeek.Sunday, Weight = 1 }
                );

            if (!pr.GetAll().Any())
            {
                pr.AddRange(lisPerson);
            }


            var tt = new DutyFierContext();
            if (!tt.DutyTypes.Any())
            {
                tt.DutyTypes.AddRange(listDutyType);
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
                        DutyType = listDutyType[0]
                    },
                    new Position()
                    {
                        Name = "Днювальний курсу",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[0]
                    },
                    new Position()
                    {
                        Name = "ПЧІ",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2]
                    },
                    new Position()
                    {
                        Name = "ПЧФ",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[1]
                    },
                    new Position()
                    {
                        Name = "Днювальний парку",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2]
                    },
                    new Position()
                    {
                        Name = "Черговий КПП-1",
                        IsSeniorPosition = true,
                        Weight = 2.6,
                        DutyType = listDutyType[2]
                    },
                    new Position()
                    {
                        Name = "Помічник чергового КПП-1",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2]
                    },
                    new Position()
                    {
                        Name = "Черговий КПП-2",
                        IsSeniorPosition = true,
                        Weight = 2.6,
                        DutyType = listDutyType[2]
                    },
                    new Position()
                    {
                        Name = "Помічник чергового КПП-2",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2]
                    },
                    new Position()
                    {
                        Name = "Черговий спортзалу",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[2]
                    },

                    new Position()
                    {
                        Name = "Черговий БП",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[3]
                    },

                    new Position()
                    {
                        Name = "Помічник чергового БП",
                        IsSeniorPosition = false,
                        Weight = 2.6,
                        DutyType = listDutyType[3]
                    }
                };
                tt.Positions.AddRange(listPosition);
                tt.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}