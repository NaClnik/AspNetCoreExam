using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DataBase;

namespace Backend.Models.ViewModels
{
    public class BookViewModel
    { 
        // Свойства класса.
        public int Id { get; set; }              // Идентификатор.
        public string Title { get; set; }        // Название.
        public int YearOfIssue { get; set; }     // Год выпуска.
        public int Price { get; set; }           // Цена.
        public int Amount { get; set; }          // Количество.
        public int AuthorId { get; set; }        // Внешний ключ автора.
        public string AuthorName { get; set; }   // Автор.
        public int CategoryId { get; set; }      // Внешний ключ категории.
        public string CategoryName { get; set; } // Категория.

        // Конструктор.
        public BookViewModel(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            YearOfIssue = book.YearOfIssue;
            Price = book.Price;
            Amount = book.Amount;
            AuthorId = book.AuthorId;
            AuthorName = book.Author.AuthorName;
            CategoryId = book.CategoryId;
            CategoryName = book.Category.CategoryName;
        } // ctorf.
    } // BookViewModel.
}
