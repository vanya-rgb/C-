
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Lab3
{
    public class Company
    {
        private readonly List<ManagementCompany> _flats = new List<ManagementCompany>();

        //добавить здание
        public void Add(ManagementCompany flat)
        {
            if (flat == null || flat.Type == RealEstateObjects.none || string.IsNullOrEmpty(flat.Flats))
            {
                throw new ArgumentException(nameof(flat));
            }
            _flats.Add(flat);
        }
        //показать список
        public IEnumerable<ManagementCompany> GetFlats()
        {
            return _flats;
        }
        
       //отсортировать по колличеству человек
       public void SortByNumberofPeople()
        {
            _flats.Sort(new ByPeopleComparer());
        }

        //сериализовать в XML-файл
        public void ToXml(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<ManagementCompany>));
            using (var stream = File.OpenWrite(fileName))
            {
                serializer.Serialize(stream, _flats);
                stream.Flush();
            }    
        }
        public static Company FromXml(string fileName)
        {
            var company = new Company();
            var serializer = new XmlSerializer(typeof(List<ManagementCompany>));

            using (var stream = File.OpenRead(fileName))
            {
                var flats = serializer.Deserialize(stream) as IEnumerable<ManagementCompany>;
                if (flats != null) company._flats.AddRange(flats);
            }
            return company;
        }
        

    }
}
