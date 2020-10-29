using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models.DataBase;
using Backend.Models.ViewModels;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // Поля класса.
        private readonly LibraryDbContext _context; // БД.

        // Конструктор.
        public BooksController(LibraryDbContext context)
        {
            _context = context;
        } // ctorf.

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooks()
        {
            // Получаем коллекцию книг.
            var collection = await _context.Books.ToListAsync();

            // Возвращаем коллекцию BookViewModel.
            return collection.Select(book => new BookViewModel(book)).ToList();
        } // GetBooks.

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> GetBook(int id)
        {
            // Находим книгу по переданному идентификатору.
            var book = await _context.Books.FindAsync(id);

            // Если книга не найдена, то возвращаем
            // клиенту ответ NotFound.
            if (book == null)
            {
                return NotFound();
            } // if.

            // Возвращаем BookViewModel.
            return new BookViewModel(book);
        } // GetBook.

        // PUT: api/Books/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // TODO: Разобраться с этим методом.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } // PutBook.

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            // Добавляем в базу данных переданную книгу.
            _context.Books.Add(book);

            // Сохраняем изменения в базе данных.
            await _context.SaveChangesAsync();

            // TODO: Разобраться с методом CreatedAtAction.
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        } // PostBook.

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookViewModel>> DeleteBook(int id)
        {
            // Ищем книгу по переданному идентификатору.
            var book = await _context.Books.FindAsync(id);

            // Если книга не найдена, то возвращаем
            // клиенту ответ NotFound.
            if (book == null)
            {
                return NotFound();
            } // if.

            // Удаляем книгу из базы данных.
            _context.Books.Remove(book);

            // Сохраняем изменения в базе данных.
            await _context.SaveChangesAsync();

            // Возвращаем BookViewModel.
            return new BookViewModel(book);
        } // DeleteBook.

        // Если книга с переданным идентификатором существует, то возвращаем true.
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        } // BookExists.
    } // BooksController.
}
