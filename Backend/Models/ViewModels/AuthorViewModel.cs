using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DataBase;

namespace Backend.Models.ViewModels
{
    // Модель представления для автора.
    public class AuthorViewModel
    {
        // Свойства класса.
        public int Id { get; set; }                    // Идентификатор.
        public string AuthorName { get; set; }         // Имя автора.
        public List<BookViewModel> Books { get; set; } // Книги автора.

        // Конструктор.
        public AuthorViewModel(Author author)
        {
            Id = author.Id;
            AuthorName = author.AuthorName;
            Books = author.Books.Select(book => new BookViewModel(book)).ToList(); 
        } // ctorf.
    } // AuthorViewModel.
}
