using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DataBase;
using Backend.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Services
{
    // Сервис для запросов к БД.
    public class QueriesService
    {
        // Поля класса.
        private readonly SoftDeleteLibraryDbContext _libraryDbContext;

        // Конструктор.
        public QueriesService(SoftDeleteLibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        } // QueriesService.

        // Методы класса.
        // Запрос 1. Вывести полную информацию обо всех книгах.
        public async Task<string> Query1Async()
        {
            var collection = 
                await _libraryDbContext.Books.Where(p => !p.IsDeleted)
                    .Select(book => new BookViewModel(book)).ToListAsync();

            return JsonConvert.SerializeObject(collection);
        } // Query1Async.

        // Запрос 2. Вывести название, год издания, автора и цену учебников по Android.
        public async Task<string> Query2Async()
        {
            var collection = await _libraryDbContext.Books.Where(book =>
                    !book.IsDeleted
                    && book.Category.CategoryName.ToLower() == "учебник"
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

            return JsonConvert.SerializeObject(collection);
        } // Query2Async.
            

        // Запрос 3. Вывести название, год издания и количество задачников по LINQ.
        public async Task<string> Query3Async()
        {
            var collection = await _libraryDbContext.Books
                .Where(book =>
                    !book.IsDeleted
                    && book.Category.CategoryName.ToLower() == "задачник"
                    && book.Title.Contains("LINQ"))
                .Select(book => new
                {
                    book.Title,
                    book.YearOfIssue,
                    book.Amount,
                    book.Category.CategoryName,
                }).ToListAsync();

            return JsonConvert.SerializeObject(collection);
        } // Query3Async.
            

        // Запрос 4. Вывести автора, название, категорию и стоимость для каждой книги, количество которых от 4 до 6.
        public async Task<string> Query4Async()
        {
            var collection = await _libraryDbContext.Books
                .Where(book =>
                    !book.IsDeleted
                    && book.Amount >= 4 && book.Amount <= 6)
                .Select(book => new
                {
                    book.Author.AuthorName,
                    book.Title,
                    book.Category.CategoryName,
                    book.Price,
                    book.Amount
                }).ToListAsync();

            return JsonConvert.SerializeObject(collection);
        } // Query4Async.

        // TODO: Не уверен в паравильности.
        // Запрос 5. Вывести фамилии и инициалы авторов и количество книг этих авторов.
        public async Task<string> Query5Async()
        {
            var collection = await _libraryDbContext.Books
                .Where(book => !book.IsDeleted)
                .GroupBy(item => item.Author.AuthorName)
                .Select(item => new
                {
                    item.Key,
                    Amount = item.Sum(p => p.Amount)
                }).ToListAsync();

            return JsonConvert.SerializeObject(collection);
        } // Query5Async.

        // Запрос 6. Для категорий книг вывести количество, минимальную стоимость книги, среднюю стоимость книги, максимальную стоимость книги.
        public async Task<string> Query6Async()
        {
            var collection = await _libraryDbContext.Books
                .Where(book => !book.IsDeleted)
                .GroupBy(item => item.Category.CategoryName)
                .Select(item => new
                {
                    item.Key,
                    Min = item.Min(p => p.Price),
                    Avg = item.Average(p => p.Price),
                    Max = item.Max(p => p.Price)
                }).ToListAsync();

            return JsonConvert.SerializeObject(collection);
        } // Query6Async.
            

        // Запрос 7. Для всех книг автора Абрамян М.Э. увеличить стоимость книг на 15%.
        public async Task<string> Query7Async()
        {
            var books = _libraryDbContext.Books
                .Where(book =>
                    !book.IsDeleted
                    && book.Author.AuthorName == "Абрамян М.Э.");

            foreach (var book in books)
            {
                // TODO: Проверить работу этой конструкции.
                book.Price = (int) Math.Round(book.Price * 1.15);
            } // foreach.

            await _libraryDbContext.SaveChangesAsync();

            return JsonConvert.SerializeObject(books.Where(book => !book.IsDeleted).Select(book => new BookViewModel(book)));
        } // Query7Async.

        // Запрос 8. Уменьшить количество книг по C++ на 1, не допускать отрицательных значений.
        public async Task<string> Query8Async()
        {
            var books = _libraryDbContext.Books
                .Where(book => 
                    !book.IsDeleted
                    && book.Title.Contains("C++"));

            foreach (var book in books)
            {
                if (book.Amount != 0)
                {
                    --book.Amount;
                } // if.
            } // foreach.

            await _libraryDbContext.SaveChangesAsync();

            return JsonConvert.SerializeObject(books.Where(book => !book.IsDeleted).Select(book => new BookViewModel(book)));
        } // Query8Async.
    } // Queries.
}