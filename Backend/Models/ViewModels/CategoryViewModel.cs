using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DataBase;

namespace Backend.Models.ViewModels
{
    // Модель представления для категории.
    public class CategoryViewModel
    {
        // Свойства класса.
        public int Id { get; set; }              // Идентификатор.
        public string CategoryName { get; set; } // Название категории.
        public bool IsDeleted { get; set; }      // Флаг логического удаления.

        // Конструктор.
        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            CategoryName = category.CategoryName;
            IsDeleted = category.IsDeleted;
        } // ctorf.
    } // CategoryViewModel.
}
