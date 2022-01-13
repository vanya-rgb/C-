//Лысиков И.А Кэ-214
//Вариант № 4
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab4
{
    public class WebScaner : IDisposable
    {
        private readonly HashSet<Uri> _procLinks = new HashSet<Uri>();
        private readonly WebClient _webClient = new WebClient();

        private readonly HashSet<string> _ignoreFiles = new HashSet<string> { ".ico", ".xml" };


        //событие
        //адрес текущей обрабатываемой страницы и массив внешних ссылок на ней
        public event Action<Uri, Uri[]> TargetFound;
        //метод для вызова события
        private void OnTargetFound(Uri page, Uri[] links)
        {
            TargetFound?.Invoke(page, links);
        }
        public void Scan(Uri startPage, int pageCount)
        {
            _procLinks.Clear();
            string domain = $"{startPage.Scheme}://{startPage.Host}";
            Process(domain, startPage, pageCount);
        }

        private void Process (string domain, Uri page, int count)
        {
            if (count <= 0 ) return;
            //пропуск
            if (_procLinks.Contains(page)) return;
            _procLinks.Add(page);

            string html = File.ReadAllText(@"C:\Users\Vanya\Desktop\ЮУрГУ\3 семестр\С#\my\Lab4\lab4\page.txt");
            string getPhone = @"href=""tel\S*""";
            string getEmail = @"href=\/[c][o][n][t][a][c][t]\S*[u]";

            var numHrefs = (from href in Regex.Matches(html, getPhone).Cast<Match>()
                   let url = href.Value.Replace("href=", "").Trim('"')
                   let loc = url.StartsWith("/")
                   select new
                       {
                           Ref = new Uri(loc ?$"{domain}{url}" : url),
                           IsLocal = loc || url.StartsWith(domain)
                       }
            ).ToList();
            
            var mailHrefs = (from href in Regex.Matches(html, getEmail).Cast<Match>()
                            let url = href.Value.Replace("href=", "").Trim('"')
                            
                            let loc = url.StartsWith("/")
                            select new
                            {
                                Ref = new Uri(loc ? $"{domain}{url}" : url),
                                IsLocal = loc || url.StartsWith(domain)
                            }
            ).ToList();


            //выделение  ссылок
            var numbers = (from href in numHrefs select href.Ref).ToArray();
            var emails = (from href in mailHrefs select href.Ref).ToArray();
            //передача параметров в событие
            if (numbers.Length > 0 || emails.Length > 0)
            {
                OnTargetFound(page, numbers);
                OnTargetFound(page, emails);
            }
            ////выделение локальных ссылок
            //var locals = (from href in hrefs where href.IsLocal select href.Ref).ToList();
            //foreach (var href in locals)
            //{
            //   //???
            //    string fileEx = Path.GetExtension(href.LocalPath).ToLower();
            //    if (_ignoreFiles.Contains(fileEx)) continue;

            //    Process(domain, href, --count);
            //}

        }
            
        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
