using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DataBase
{
    public class Author
    {
        // Свойства класса.
        public int Id { get; set; }                          // Идентификатор.
        public string AuthorName { get; set; }               // Имя автора.
        public virtual ICollection<Book> Books { get; set; } // Книги.

        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public Author() { }

        // Конструктор с параметрами.
        public Author(string authorName)
        {
            AuthorName = authorName;
        } // ctorf.
    } // Author.
}
