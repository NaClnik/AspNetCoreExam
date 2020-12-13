namespace Backend.Models.PostModels
{
    // Этот класс должен передавать клиент при передаче
    // методом POST.
    public class AuthorPostModel
    {
        // Свойства класса.
        public string Author { get; set; } // Имя автора.

        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public AuthorPostModel() { }

        // Конструктор с параметрами.
        public AuthorPostModel(string author)
        {
            Author = author;
        } // ctorf.
    } // AuthorPostModel.
}
