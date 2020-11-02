﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models.DataBase;
using Backend.Models.FormModels;
using Backend.Models.ViewModels;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        // Поля класса.
        private readonly LibraryDbContext _context; // БД.

        public AuthorsController(LibraryDbContext context)
        {
            _context = context;
        } // ctorf.

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAuthors()
        {
            // Получаем коллекцию авторов.
            var collection = await _context.Authors.ToListAsync();

            // Возвращаем коллекцию AuthorViewModel.
            return collection.Select(author => new AuthorViewModel(author)).ToList();
        } // GetAuthors.

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorViewModel>> GetAuthor(int id)
        {
            // Находим автора по переданному идентификатору.
            var author = await _context.Authors.FindAsync(id);

            // Если автор не найден, то отправляем
            // клиенту ответ NotFound.
            if (author == null)
            {
                return NotFound();
            } // if.

            // Отправляем найденного автора.
            return new AuthorViewModel(author);
        } // GetAuthor.

        // PUT: api/Authors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // TODO: Разобраться с этим методом.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } // PutAuthor.

        // POST: api/Authors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AuthorViewModel>> PostAuthor([FromBody]AuthorFormModel authorFormModel)
        {
            Author author = new Author(authorFormModel.AuthorName);

            Debug.WriteLine(author);

            // Добавляем в базу данных переданного автора.
            _context.Authors.Add(author);

            // Сохраняем изменения базы данных.
            await _context.SaveChangesAsync();

            // TODO: Разобраться с методом CreatedAtAction.
            return CreatedAtAction(nameof(GetAuthor), new {Id = author.Id}, author);
        } // PostAuthor.

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorViewModel>> DeleteAuthor(int id)
        {
            // Находим автора по заданному идентификатору.
            var author = await _context.Authors.FindAsync(id);

            // Если автор не найден, то отправляем
            // клиенту ответ NotFound.
            if (author == null)
            {
                return NotFound();
            } // if.

            var viewModel = new AuthorViewModel(author);

            // Удаляем автора из базы данных.
            _context.Authors.Remove(author);

            // Сохраняем изменения в базе данных.
            await _context.SaveChangesAsync();

            // Возвращаем AuthorViewModel.
            return viewModel;
        } // DeleteAuthor.

        // Если автор с переданным id существует, то возвращаем true.
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        } // AuthorExists.
    } // AuthorsController.
}
