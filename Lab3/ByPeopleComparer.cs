using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class ByPeopleComparer : IComparer<ManagementCompany>
    {
        public int Compare(ManagementCompany x, ManagementCompany y)
        {
            return x.NumberofPeoples.CompareTo(y.NumberofPeoples);
        }

    }
}
