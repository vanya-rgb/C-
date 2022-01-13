/*
 *  Описать класс «Домашняя библиотека». Предусмотреть возможность работы с произвольным
    числом книг, поиска книги по какому-либо признаку (например, по автору или году издания),
    добавления книг в библиотеку, удаления книг из нее, сортировки книг по разным полям.
    Программа должна содержать меню, позволяющее осуществлять проверку всех методов.
 */

//Лысиков И.А КЭ-214 вар. №4

using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isWork = true;
            NoteBook item = new NoteBook(4);


            while (isWork)
            {
                Console.WriteLine("Что бы вы хотели сделать?\n1.Добавить книгу\n2.Найти книгу по автору\n3.Найти по году издания\n4.Отсортировать книги по авторам\n5.Отсортировать книги по году издания\n6.Удалить книгу");
                //decimal answer = Convert.ToInt32(Console.ReadLine());
                switch(Console.ReadLine())
                {
                    default:
                        Console.WriteLine("Не понял что хотите сделать");
                        break;
                    case "1":
                        string autor = Console.ReadLine();
                        int dateRelease;
                        try
                        {
                            dateRelease = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Неверный тип");
                            continue;
                        }

                        item.Add(new Note
                        {
                            Autor = autor,
                            DateRelease = dateRelease
                        });
                        item.Read();
                        break;
                    case "2":
                        Console.WriteLine(item.FindByAutor(Console.ReadLine()));
                        Console.WriteLine("Введите фамилию автора");
                        break;

                    case "3":
                        Console.WriteLine("Введите год издания");
                        Console.WriteLine(item.FindByDate(Convert.ToInt32(Console.ReadLine())));
                        break;

                    case "4":
                
                        item.SortByName();
                        item.Read();
                        break;

                    case "5":
                        item.SortByDate();
                        item.Read();
                        break;

                    case "6":
                        item.DeleteRecords();
                        item.Read();
                }

                
                while(true)
                {
                    Console.WriteLine("Продолжить работу(y/n)?");
                    string i = Console.ReadLine();
                    if (i.ToLower() == "y" || i.ToLower()== "yes" || i.ToLower() == "yep")
                    {
                        break;
                    }
                    else if (i.ToLower() == "n" || i.ToLower() == "no" || i.ToLower() == "nope")
                    {
                        isWork = false;
                        break;
                    }
                    Console.WriteLine("Введите корректный ответ.");
                }


            }

        }

    }
}
