using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Exeptions
{
    public class PersonAvalibleException : ArgumentException
    {
        public PersonAvalibleException(string message) : base(message)
        {
        }
    }
}
