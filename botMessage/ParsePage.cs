using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace botMessage
{
    internal class ParsePage
    {
        public static void Scaner()
        {
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
