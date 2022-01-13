using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Lab3
{
    public enum RealEstateObjects
    {
        none,
        lenina80,
        tatynicheva11,
        lynacharskogo8,
        lenina76,

    }

    [XmlInclude(typeof(ResidentialBuilding))]
    [XmlInclude(typeof(OfficeBuilding))]

    public abstract class ManagementCompany
    {
        private static readonly Dictionary<RealEstateObjects, double> _numberOfPeoples = new Dictionary<RealEstateObjects, double>
        {
            [RealEstateObjects.lenina76] = 2,
            [RealEstateObjects.tatynicheva11] = 2.4,
            [RealEstateObjects.lynacharskogo8] = 1.2,
            [RealEstateObjects.lenina80] = 1.1,
        };

        protected RealEstateObjects _estateObject;
        protected double _numberOfPeople;
        //protected double _numberOfPeople;
        protected ManagementCompany() { }

        protected ManagementCompany(RealEstateObjects type, string name)
        {
            Type = type;
            Flats = name;
        }

        //свойства
        public RealEstateObjects Type
        {
            get { return _estateObject; }
            set
            {
                _estateObject = value;
                _numberOfPeople = _numberOfPeoples[_estateObject];
            }
        }

        public string Flats { get; set; }

        public abstract int NumberofPeoples { get; }
    }
}
