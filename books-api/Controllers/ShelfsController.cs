using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using books_api.Models;
using books_api.Services;
using books_api.Services.LookupModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace books_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShelfsController : ControllerBase
    {
        private readonly ShelfService _shelfService;

        public ShelfsController(ShelfService shelfService)
        {
            _shelfService = shelfService;
        }
        // GET: api/Shelfs
        [HttpGet]
        public ActionResult<List<Shelf>> Get() =>
            _shelfService.Get();

        // GET: api/Shelfs/GetWithBooks
        [HttpGet]
        public ActionResult<List<ShelfWithBooks>> GetWithBooks() =>
            _shelfService.GetWithBooks();

        // GET: api/Shelfs/GetWithBooksBson
        [HttpGet]
        public ActionResult GetWithBooksBson()
        {
            var x = _shelfService.GetWithBooksBson();
            var res = x.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict });
            return Content(res);
        }
    }
}
