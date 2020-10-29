using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DataBase
{
    public class Category
    {
        // Свойства класса.
        public int Id { get; set; }                          // Идентификатор.
        public string CategoryName { get; set; }             // Название категории.
        public virtual ICollection<Book> Books { get; set; }         // Книги.

        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public Category() { }

        // Конструктор с параметрами.
        public Category(string categoryName)
        {
            CategoryName = categoryName;
        } // ctorf.
    } // Category.
}
