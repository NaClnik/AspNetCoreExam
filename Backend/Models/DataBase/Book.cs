using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;

namespace Backend.Models.DataBase
{
    public class Book :ISoftDeletable
    {
        // Свойства класса.
        public int Id { get; set; }                    // Идентификатор.
        public string Title { get; set; }              // Название.
        public int YearOfIssue { get; set; }           // Год выпуска.
        public int Price { get; set; }                 // Цена.
        public int Amount { get; set; }                // Количество.
        public int AuthorId { get; set; }              // Внешний ключ автора.
        public virtual Author Author { get; set; }     // Автор.
        public int CategoryId { get; set; }            // Внешний ключ категории
        public virtual Category Category { get; set; } // Категория.
        public bool IsDeleted { get; set; }            // Флаг логического удаления.

        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public Book() { }

        // Конструктор с параметрами.
        public Book(string title, int yearOfIssue, int price, int amount)
        {
            Title = title;
            YearOfIssue = yearOfIssue;
            Price = price;
            Amount = amount;
        } // ctorf.
    } // Book.
}
