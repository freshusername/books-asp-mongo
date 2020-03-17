using books_api.Models;

namespace books_api.Services.LookupModels
{
    public class ShelfWithBooks : Shelf
    {
        public Book[] Books { get; set; }
    }
}
