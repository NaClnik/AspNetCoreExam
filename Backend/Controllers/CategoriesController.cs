using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models.DataBase;
using Backend.Models.PostModels;
using Backend.Models.ViewModels;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Поля класса.
        private readonly SoftDeleteLibraryDbContext _context; // БД.

        // Конструктор.
        public CategoriesController(SoftDeleteLibraryDbContext context)
        {
            _context = context;
        } // ctorf.

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<string>> GetCategories()
        {
            // Получаем коллекцию категорий.
            var collection = await _context.Categories.Where(p => !p.IsDeleted).ToListAsync();

            var viewModelCollection = collection
                .Select(category => new CategoryViewModel(category)).ToList();

            var json = JsonConvert.SerializeObject(viewModelCollection);

            // Возвращаем коллекцию CategoryViewModel.
            return json;
        } // GetCategories.

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetCategory(int id)
        {
            // Ищем категорию по идентификатору.
            var category = await _context.Categories.FindAsync(id);

            // Если категория не найдена, то отправляем клиенту
            // ответ NotFound.
            if (category == null || category.IsDeleted)
            {
                return NotFound();
            } // if.

            // Возвращаем клиенту CategoryViewModel.
            return JsonConvert.SerializeObject(new CategoryViewModel(category));
        } // GetCategory.

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // TODO: Разобраться с этим методом.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } // PutCategory.

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<string>> PostCategory([FromBody]CategoryPostModel categoryPostModel)
        {
            Category category = new Category(categoryPostModel.Category);

            // Добавляем переданную категорию.
            _context.Categories.Add(category);

            // Сохраняем изменения в базе данных.
            await _context.SaveChangesAsync();

            // TODO: Разобраться в методе CreatedAtAction.
            return JsonConvert.SerializeObject(category);
        } // PostCategory.

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryViewModel>> DeleteCategory(int id)
        {
            // Ищем категорию по идентификатору.
            var category = await _context.Categories.FindAsync(id);

            // Если категория не найдена, то отправляем
            // клиенту ответ NotFound.
            if (category == null || category.IsDeleted)
            {
                return NotFound();
            } // if.

            // Удаляем категорию из базы данных.
            _context.Categories.Remove(category);

            // Сохраняем изменения.
            await _context.SaveChangesAsync();

            return new CategoryViewModel(category);
        } // DeleteCategory.

        // Если есть категория по указанному идентификатору,
        // то возвращаем true.
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        } // CategoryExists.
    } // CategoriesController.
}
