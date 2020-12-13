using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.PostModels
{
    // Этот класс должен передавать клиент при передаче
    // методом POST.
    public class CategoryPostModel
    {
        // Свойства класса.
        public string Category { get; set; }

        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public CategoryPostModel() { }

        // Конструктор с параметрами.
        public CategoryPostModel(string category)
        {
            Category = category;
        } // ctorf.
    } // CategoryPostModel.
}
