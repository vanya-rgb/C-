using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public class NoteBook
    {
        private List<Note> _notes = new List<Note>();

        //public NoteBook(int count = 0)
        //{
        //    _notes.Add(new Note());
        //}

        //private class FullNameComparer : IComparer<Note>
        //{
        //        public int Compare(Note x, Note y)
        //    {
        //        return string.Compare(x.Autor, y.Autor, StringComparison.CurrentCulture);
        //    }
        //}


        //Добавление записи
        public void Add(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            if (string.IsNullOrEmpty(note.Autor))
                throw new ArgumentNullException(nameof(note.Autor));

            _notes.Add(note);
        }

        //Сортировка
        public void SortByName()
        {
            if (_notes.Count == 0)
                return;
            _notes = _notes.OrderBy(x => x.Autor).ToList();
            //_notes.Sort(new FullNameComparer());
        }

        public void SortByDate()
        {
            if (_notes.Count == 0)
                return;
            _notes = _notes.OrderBy(x => x.DateRelease).ToList();
            //Note[] a = _notes.ToArray();
            //foreach (var note in _notes)
            //{
            //}
            //return notes as List<Note>;//.ToArray();
            //return (List<Note>)_notes;
        }
        

        public Note FindByDate(int date)
        {
            foreach (Note note in _notes)
            {
                if (note.DateRelease == date)
                {
                    return note;
                }
            }
            return null;
        }

        public Note FindByAutor(string autor)
        {
            foreach (var note in _notes)
                if (note.Autor == autor)
                    return note;
            return null;

        }


        public Note[] GetAll()
        {
            return _notes.ToArray();
        }
        public void Read()
        {
            foreach (var note in GetAll())
                Console.WriteLine(note);
        }

        // Метод поиска по имени
        public List<Note> SearchRecordsByName()
        {
            string name = Console.ReadLine();
            // Linq запрос к списку с записями
            var result = _notes.Where(rec => rec.Autor == name).ToList();

            return result;
        }

        public List<Note> SearchRecords(Note rec)
        {
            // Linq запрос к списку с записями
            var ret = _notes.Where(r => r.Autor == rec.Autor).ToList();

            return ret;
        }

        // Метод удаления записи из списка
        public void DeleteRecords()
        {
            
            // Поиск введенной записи
            List<Note> records = SearchRecordsByName();
            // Удаление найденной записи
            foreach (Note r in records)
                // Linq запрос удаления
                _notes.Remove(r);
        }
        

    }
}
