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
        public string AuthorName { get; set; }   // Автор.
        public string CategoryName { get; set; } // Категория.
        public bool IsDeleted { get; set; }      // Флаг логического удаления.

        // Конструктор.
        public BookViewModel(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            YearOfIssue = book.YearOfIssue;
            Price = book.Price;
            Amount = book.Amount;
            AuthorName = book.Author.AuthorName;
            CategoryName = book.Category.CategoryName;
            IsDeleted = book.IsDeleted;
        } // ctorf.
    } // BookViewModel.
}
