using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class ResidentialBuilding : ManagementCompany
    {
        private const double k = 1.3;

        public ResidentialBuilding() { }
        public ResidentialBuilding(RealEstateObjects type, string number, int count) : base(type, number)
        {
            Count = count;
        }

        //Колличество жильцов
        public int Count { get; set; }
        public override int NumberofPeoples
        {
            get { return (int)(k * Count * _numberOfPeople); }
        }

    }
}
