using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Exeptions
{
    class EmptyRequestException : ArgumentNullException
    {
        public EmptyRequestException(string message) : base(message)
        {
        }
    }
}
