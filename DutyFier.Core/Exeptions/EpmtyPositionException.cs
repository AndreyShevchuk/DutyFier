using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Exeptions
{
    class EpmtyPositionException : ArgumentNullException
    {
        public EpmtyPositionException(string message) : base(message)
        {
        }
    }
}
