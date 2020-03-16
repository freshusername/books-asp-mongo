using books_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Services.LookupModels
{
    public class ShelfWithBooks : Shelf
    {
        public Book[] Books { get; set; }
    }
}
