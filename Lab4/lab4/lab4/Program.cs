//Лысиков И.А Кэ-214
//Вариант № 4
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //WebClient client = new WebClient();
            //string page = client.DownloadString(new Uri("https://www.susu.ru/"));

            //File.WriteAllText(@"C:\Users\Vanya\Desktop\ЮУрГУ\3 семестр\С#\my\Lab4\lab4\page.txt", page);

            using (WebScaner scaner = new WebScaner())
            {
                scaner.TargetFound += (page, links) =>
                 {
                     Console.WriteLine($"\nPage:\n\t{page}\nLinks:");
                     foreach (var link in links)
                     {
                         Console.WriteLine($"\t{link}");
                     }
                 };
                scaner.Scan(new Uri("https://www.susu.ru"), 10);
                Console.WriteLine("Done!");
            }
        }
    }
}
