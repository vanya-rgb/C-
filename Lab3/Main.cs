﻿/*
 * На основе одной из готовых обобщенных (шаблонных) объектных коллекций.NET создать класс
«Управляющая компания», включающий список объектов недвижимости (строений). Классы
строений должны образовывать иерархию с базовым классом. Объекты недвижимости бывают
двух типов: жилые и нежилые.Описать в базовом классе абстрактный метод для расчета
приближенного среднего количества жильцов/работников строения. Для жилых построек среднее
количество жильцов – это количество квартир на количество комнат в квартире (тип квартиры) на
1.3, для нежилых строений среднее количество сотрудников пропорционально площади с
коэффициентом 0.2. В виде меню программы реализовать нижеприведенный функционал.
1. Упорядочить всю последовательность объектов недвижимости по убыванию среднего
количества жильцов/работников.При совпадении значения – упорядочивать данные по типу
строения (жилые, нежилые), затем по алфавиту по адресу строения. Вывести тип строения, адрес
строения, среднее количество жильцов/работников для всех элементов списка.
2. Вывести первые 3 объекта из полученного в пункте 1 списка.
3. Вывести последние 4 адреса объекта из полученного в пункте 1 списка.
4. В реальном времени (в процессе заполнения списка строений) рассчитывать и поддерживать в
актуальном состоянии среднее количество жильцов/работников объекта недвижимости по
компании в целом, сохранить значение как поле класса «Управляющая компания».
5. Организовать запись и чтение всех данных в/из файла. Реализовать поддержку формата файлов
XML.
6. Организовать обработку некорректного формата входного файла.

*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var myFlat = new Company();
            myFlat.Add(new ResidentialBuilding(RealEstateObjects.lenina76, "Ленина 76", 122));
            myFlat.Add(new OfficeBuilding(RealEstateObjects.tatynicheva11, "Татьяничевой 11", 1200));
            myFlat.Add(new ResidentialBuilding(RealEstateObjects.lynacharskogo8, "Луначарского 8", 201));
            myFlat.Add(new ResidentialBuilding(RealEstateObjects.lenina80, "Ленина 80", 170));

            myFlat.SortByNumberofPeople();

            const string fileName = @"C:\Users\Vanya\Desktop\ЮУрГУ\3 семестр\С#\my\Lab3\MyList.xml";
            myFlat.ToXml(fileName);

            try
            {
                var myFlatNew = Company.FromXml(fileName);
                foreach(var flat in myFlatNew.GetFlats())
                {
                    Console.WriteLine(
                        $"Flat: {flat.Type}, {flat.Flats}, {flat.NumberofPeoples} чел.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
