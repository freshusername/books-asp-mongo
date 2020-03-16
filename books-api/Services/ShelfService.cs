using books_api.DbSettings;
using books_api.Models;
using books_api.Services.LookupModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Services
{
    public class ShelfService
    {
        private readonly IMongoCollection<Shelf> _shelfs;
        private readonly IMongoCollection<Book> _books;
        //private readonly IMongoDatabase _db; 

        public ShelfService(IBooksDbSettings settings) //, IMongoDatabase db)
        {
            //_db = db;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _shelfs = database.GetCollection<Shelf>("Shelfs");
            _books = database.GetCollection<Book>("Books");
        }

        public List<Shelf> Get() =>
            _shelfs.Find(shelf => true).ToList();

        public List<ShelfWithBooks> GetWithBooks() 
        {
            //var books = _db.GetCollection<Book>("Books");
            List<ShelfWithBooks> res = _shelfs.Aggregate()
                                .Lookup<Shelf, Book, ShelfWithBooks>(
                                    _books,
                                    sh => sh.Id,
                                    b => b.ShelfId,
                                    b_wz_sh => b_wz_sh.Books)
                                .ToList();
            return res;
        }

        public List<BsonDocument> GetWithBooksBson() 
        {
            //var books = _db.GetCollection<Book>("Books");
           List<BsonDocument> r = _shelfs.Aggregate()
                                .Lookup("Books", "_id", "ShelfId", "shelf_with_books")
                                .ToList();

            var res = r;
            return res;
        }
        
        public Shelf Get(string id) =>
            _shelfs.Find(shelf => shelf.Id == id).FirstOrDefault();


        public Shelf Create(Shelf shelf)
        {
            _shelfs.InsertOne(shelf);
            return shelf;
        }

        public void Update(string id, Shelf shelfIn) =>
            _shelfs.ReplaceOne(shelf => shelf.Id == id, shelfIn);

        public void Remove(Book bookIn) =>
            _shelfs.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _shelfs.DeleteOne(book => book.Id == id);
    }
}
