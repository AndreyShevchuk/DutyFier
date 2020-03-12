using DutyFier.Core.Entities;
using DutyFier.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutyFier.Core.Models
{
    public class SeedData
    {
        public static void StartData(DutyFierContext dutyFierContext)
        {

            var lisPerson = new List<Person>()
            {
                new Person
                {
                    FirstName = "Batienko",
                    LastName = "Andree",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Vinitckiy",
                    LastName = "Sergey",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Getmanenko",
                    LastName = "Vitaliy",
                    Factor = 1
                },

                new Person
                {

                    FirstName = "Juruk",
                    LastName = "Artem",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Juravel",
                    LastName = "Andree",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Kovalenko",
                    LastName = "Andree",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Salykov",
                    LastName = "Illy",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Skrupnik",
                    LastName = "Daniil",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Fedor",
                    LastName = "Vitaly",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Shevchuk",
                    LastName = "Andrey",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Shevchuk",
                    LastName = "Nikolay",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Shkorupskiy",
                    LastName = "Valintin",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Zlobin",
                    LastName = "Oleksandr",
                    Factor = 1
                },

                new Person
                {
                    FirstName = "Gumenuk",
                    LastName = "Oleksandr",
                    Factor = 1
                }
            };

            var pr = new PersonRepository(dutyFierContext);
            if (!pr.GetAll().Any())
            {
                pr.AddRange(lisPerson);
            }

            
            //Console.ReadKey();
        }
    }
}
