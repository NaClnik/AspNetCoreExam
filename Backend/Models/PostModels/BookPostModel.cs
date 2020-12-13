using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.PostModels
{
    // Этот класс должен передавать клиент при передаче
    // методом POST.
    public class BookPostModel
    {
        // Свойства класса.
        // TODO: Создать класс.
        public string Title { get; set; }              // Название.
        public int YearOfIssue { get; set; }           // Год выпуска.
        public int Price { get; set; }                 // Цена.
        public int Amount { get; set; }                // Количество.
        public int AuthorId { get; set; }              // Внешний ключ автора.
        public int CategoryId { get; set; }            // Внешний ключ категории

        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public BookPostModel() { }

        // Конструктор с параметрами.
        public BookPostModel(string title, int yearOfIssue, int price, int amount, int authorId, int categoryId)
        {
            Title = title;
            YearOfIssue = yearOfIssue;
            Price = price;
            Amount = amount;
            AuthorId = authorId;
            CategoryId = categoryId;
        } // ctorf.
    } // BookPostModel.
}
