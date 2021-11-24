using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App02._1
{
    /// <summary>
    /// Represent a class that contains the collection books.  
    /// </summary>
    public class Catalog : IEnumerable<Book>
    {
        List<Book> _books;

        public Catalog()
        {
            _books = new List<Book>();
        }

        public Book this[string index]
        {
            get
            {
                string tempIndex = index.Replace("-", "");

                return _books.FirstOrDefault(bookItem => bookItem.ISBN == tempIndex);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public IEnumerator<Book> GetEnumerator()
        {
            foreach (var item in _books.OrderBy(bookItem => bookItem.Title))
            {
                yield return item;
            }
        }
        
        public void Add(Book book)
        {
            _books.Add(book);
        }
        
        public void Remove(Book book)
        {
            _books.Remove(book);
        }
        
        public List<Book> GetBooksForNameAvtor(Writer avtor)
        {
            return _books.Where(bookItem=>bookItem.Avtors.Contains(bookItem.Avtors.FirstOrDefault(avtorItem=> avtorItem.Equals(avtor)))).ToList();
        }
        
        public IEnumerable GetBooksForDateByDescending() 
        {
            return _books.OrderBy(bookItem => bookItem.Date);
        }
       
        public (Writer,int)[] GetTupleAvtorsCountBooks()
        {
            return _books.SelectMany(bookItem => bookItem.Avtors).GroupBy(avtorItem => avtorItem).Select(temp => (temp.Key, temp.Count())).ToArray();
        }

        public override string ToString()
        {
            StringBuilder builderCatalog = new StringBuilder();

            foreach (var item in _books)
            {
                builderCatalog.Append($"{item}");
            }

            return string.Format($"{builderCatalog}");
        }
    }
}
