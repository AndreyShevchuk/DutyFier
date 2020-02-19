using System;
using System.Collections.Generic;
using System.Text;

namespace DutyFier.Core.Entities
{
    public class DutyReqestComparer : IEqualityComparer<DutyRequest>
    {
        public bool Equals(DutyRequest x, DutyRequest y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(DutyRequest obj)
        {
            return obj.GetHashCode();
        }
    }
}
