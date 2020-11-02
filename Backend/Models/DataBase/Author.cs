using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;

namespace Backend.Models.DataBase
{
    public class Author : ISoftDeletable
    {
        // Свойства класса.
        public int Id { get; set; }                          // Идентификатор.
        public string AuthorName { get; set; }               // Имя автора.
        public virtual ICollection<Book> Books { get; set; } // Книги.
        public bool IsDeleted { get; set; }                  // Флаг логического удаления.

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
