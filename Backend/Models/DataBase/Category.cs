using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;

namespace Backend.Models.DataBase
{
    public class Category : ISoftDeletable
    {
        // Свойства класса.
        public int Id { get; set; }                          // Идентификатор.
        public string CategoryName { get; set; }             // Название категории.
        public virtual ICollection<Book> Books { get; set; } // Книги.
        public bool IsDeleted { get; set; }                  // Флаг логического удаления.

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
