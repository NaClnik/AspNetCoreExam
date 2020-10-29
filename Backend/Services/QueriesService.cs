using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DataBase;
using Backend.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    // Сервис для запросов к БД.
    public class QueriesService
    {
        // Поля класса.
        private readonly LibraryDbContext _libraryDbContext;

        // Конструктор.
        public QueriesService(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        } // QueriesService.

        // Методы класса.
        // Запрос 1. Вывести полную информацию обо всех книгах.
        public async Task<IEnumerable<BookViewModel>> Query1Async() =>
            await _libraryDbContext.Books.Select(book => new BookViewModel(book)).ToListAsync();

        // Запрос 2. Вывести название, год издания, автора и цену учебников по Android.
        public async Task<IEnumerable> Query2Async() =>
            await _libraryDbContext.Books.Where(book => 
                    book.Category.CategoryName.ToLower() == "учебник"
                    && book.Title.Contains("Android")
                    || book.Title.Contains("Андроид"))
                .Select(book => new
                {
                    book.Title,
                    book.YearOfIssue,
                    book.Author.AuthorName,
                    book.Price,
                    book.Category.CategoryName
                }).ToListAsync();

        // Запрос 3. Вывести название, год издания и количество задачников по LINQ.
        public async Task<IEnumerable> Query3Async() =>
            await _libraryDbContext.Books
                .Where(book => book.Category.CategoryName.ToLower() == "задачник"
                               && book.Title.Contains("LINQ"))
                .Select(book => new
                {
                    book.Title,
                    book.YearOfIssue,
                    book.Amount,
                    book.Category.CategoryName,
                }).ToListAsync();

        // Запрос 4. Вывести автора, название, категорию и стоимость для каждой книги, количество которых от 4 до 6.
        public async Task<IEnumerable> Query4Async() =>
            await _libraryDbContext.Books
                .Where(book => book.Amount >= 4 && book.Amount <= 6)
                .Select(book => new
                {
                    book.Author.AuthorName,
                    book.Title,
                    book.Category.CategoryName,
                    book.Price,
                    book.Amount
                }).ToListAsync();

        // TODO: Не уверен в паравильности.
        // Запрос 5. Вывести фамилии и инициалы авторов и количество книг этих авторов.
        public async Task<IEnumerable> Query5Async() =>
            await _libraryDbContext.Books
                .GroupBy(item => item.Author.AuthorName)
                .Select(item => new
                {
                    item.Key,
                    Amount = item.Sum(p => p.Amount)
                }).ToListAsync();

        // Запрос 6. Для категорий книг вывести количество, минимальную стоимость книги, среднюю стоимость книги, максимальную стоимость книги.
        public async Task<IEnumerable> Query6Async() =>
            await _libraryDbContext.Books
                .GroupBy(item => item.Category.CategoryName)
                .Select(item => new
                {
                    item.Key,
                    Min = item.Min(p => p.Price),
                    Avg = item.Average(p => p.Price),
                    Max = item.Max(p => p.Price)
                }).ToListAsync();

        // Запрос 7. Для всех книг автора Абрамян М.Э. увеличить стоимость книг на 15%.
        public async Task Query7Async()
        {
            var books = _libraryDbContext.Books
                .Where(item => item.Author.AuthorName == "Абрамян М.Э.");

            foreach (var book in books)
            {
                // TODO: Проверить работу этой конструкции.
                book.Price = (int) Math.Round(book.Price * 1.15);
            } // foreach.

            await _libraryDbContext.SaveChangesAsync();
        } // Query7Async.

        // Запрос 8. Уменьшить количество книг по C++ на 1, не допускать отрицательных значений.
        public async Task Query8Async()
        {
            var books = _libraryDbContext.Books
                .Where(item => item.Title.Contains("C++"));

            foreach (var book in books)
            {
                if (book.Amount != 0)
                {
                    --book.Amount;
                } // if.
            } // foreach.

            await _libraryDbContext.SaveChangesAsync();
        } // Query8Async.
    } // Queries.
}