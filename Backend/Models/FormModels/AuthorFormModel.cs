using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.FormModels
{
    // Класс, который описывает объекты, приходящие
    // со стороны клиента.
    public class AuthorFormModel
    {
        // Свойства класса.
        public string AuthorName { get; set; } // Имя автора.

        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public AuthorFormModel() { }

        // Конструктор с параметрами.
        public AuthorFormModel(string authorName)
        {
            AuthorName = authorName;
        } // ctorf.
    } // AuthorFormModel.
}
