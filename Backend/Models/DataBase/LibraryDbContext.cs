using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Backend.Models.DataBase
{
    public sealed class LibraryDbContext : DbContext
    {
        // Таблицы БД.
        public DbSet<Author> Authors { get; set; }      // Авторы.
        public DbSet<Category> Categories { get; set; } // Категории.
        public DbSet<Book> Books { get; set; }          // Книги.

        #region Ансамбль конструкторов
        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public LibraryDbContext() { }

        // Конструктор с параметрами.
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
            // Чтобы вызвать этот метод в конструкторе, Intellisense посоветовал
            // Сделать запечатать класс (sealed).
            // TODO: Убрать при готовом проекте Database.EnsureDeleted().
            Database.EnsureDeleted();
            //Database.EnsureCreated();
        } // ctorf.
        #endregion

        // Методы класса.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Настройка связей "один-ко-многим"
            // Настройка связей "один-ко-многим".
            modelBuilder.Entity<Book>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Book>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.AuthorId);
            
            // Настройка навигационных свойств.
            modelBuilder.Entity<Author>()
                .Navigation(p => p.Books)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder.Entity<Category>()
                .Navigation(p => p.Books)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            #endregion

            // Настройка сущности Category.
            modelBuilder.Entity<Category>()
                .Property(p => p.CategoryName)
                .IsUnicode()
                .IsRequired();

            // Настройка сущности Author.
            modelBuilder.Entity<Author>()
                .Property(p => p.AuthorName)
                .IsUnicode()
                .IsRequired();

            #region Настройка сущности Book
            // Настройка сущности Book.
            modelBuilder.Entity<Book>()
                .Property(p => p.Title)
                .IsUnicode()
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(p => p.YearOfIssue)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(p => p.Amount)
                .IsRequired();
            #endregion

            #region Подготовка данных для инициализации БД
            // Подготовка данных для инициализации БД.
            // Категории.
            var categories = new[]
            {
                new Category("учебник") {Id = 1, IsDeleted = false},
                new Category("задачник") {Id = 2, IsDeleted = false},
                new Category("монография") {Id = 3, IsDeleted = false}
            }; // categories

            var authors = new[]
            {
                new Author("Шилдт Г."){Id = 1, IsDeleted = false},
                new Author("Кент Дж."){Id = 2, IsDeleted = false},
                new Author("Абрамян М.Э."){Id = 3, IsDeleted = false},
                new Author("Дейтел П."){Id = 4, IsDeleted = false},
                new Author("Кузнецов И.А."){Id = 5, IsDeleted = false},
                new Author("Егоренко В.Н."){Id = 6, IsDeleted = false},
                new Author("Кравец С.А."){Id = 7, IsDeleted = false}
            }; // authors.

            // Книги.
            var books = new[]
            { 
                new Book("Экстремальное программирование", 2001, 150, 3){Id = 1, AuthorId = 2, CategoryId = 1, IsDeleted = false}, 
                new Book("Задачник по программированию", 2005, 350, 2){Id = 2, AuthorId = 3, CategoryId = 2, IsDeleted = false}, 
                new Book("Как программировать на Android", 2011, 520, 4){Id = 3, AuthorId = 4, CategoryId = 3, IsDeleted = false}, 
                new Book("Мои походы за бугор", 1988, 15, 2){Id = 4, AuthorId = 5, CategoryId = 3, IsDeleted = false}, 
                new Book("Как программировать на C++", 1995, 590, 5){Id = 5, AuthorId = 4, CategoryId = 1, IsDeleted = false}, 
                new Book("WPF - введение в технологию", 2007, 890, 3){Id = 6, AuthorId = 6, CategoryId = 1, IsDeleted = false}, 
                new Book("Android для профессионалов", 2016, 510, 4){Id = 7, AuthorId = 7, CategoryId = 1, IsDeleted = false}, 
                new Book("Как программировать в C#.NET", 2009, 480, 2){Id = 8, AuthorId = 4, CategoryId = 1, IsDeleted = false}, 
                new Book("Сборник задач по LINQ", 2012, 390, 6){Id = 9, AuthorId = 3, CategoryId = 2, IsDeleted = false}, 
                new Book("С хоббитом туда и обратно", 2011, 35, 5){Id = 10, AuthorId = 5, CategoryId = 3, IsDeleted = false}, 
                new Book("Базовый курс C++", 2010, 280, 2){Id = 11, AuthorId = 1, CategoryId = 1, IsDeleted = false}, 
                new Book("WCF - технология распреденных приложений", 2012, 360, 3){Id = 12, AuthorId = 6, CategoryId = 1, IsDeleted = false}, 
                new Book("Полное руководство по Java SE 8", 2014, 490, 3){Id = 13, AuthorId = 1, CategoryId = 1, IsDeleted = false}, 
                new Book("Руководство по разработке под Android", 2016, 680, 2){Id = 14, AuthorId = 4, CategoryId = 1, IsDeleted = false}, 
                new Book("Типичные задачи по LINQ в оперативном учете", 2012, 350, 1){Id = 15, AuthorId = 6, CategoryId = 2, IsDeleted = false}, 
                new Book("Взгляд на Android для профессионалов", 2016, 580, 2){Id = 16, AuthorId = 4, CategoryId = 1, IsDeleted = false}, 
                new Book("Практикум по Python 3.7", 2019, 410, 2){Id = 17, AuthorId = 6, CategoryId = 2, IsDeleted = false}, 
            }; // books.
            #endregion
           
            // Записываем данные в БД.
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);
        } // OnModelCreating.
    } // LibraryDbContext.
}
