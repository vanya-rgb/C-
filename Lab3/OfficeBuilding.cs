using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
        public class OfficeBuilding : ManagementCompany
    {
        public OfficeBuilding() { }

        public OfficeBuilding(RealEstateObjects type, string number, double square):base(type, number)
        {
            Square = square;
        }
        public double Square { get; set; }
        public override int NumberofPeoples
        {
            get { return (int)(_numberOfPeople * Square * 0.2); }
        }

    }
}
