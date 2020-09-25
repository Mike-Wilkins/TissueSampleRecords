using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TissueSampleRecords.Models;
using TissueSampleRecords.Services;

namespace TissueSampleRecords.Controllers
{
    public class CollectionController : Controller
    {
        private ApplicationDbContext _db;

        public CollectionController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Collection List
        public IActionResult Index()
        {
            var result = _db.Collections.OrderBy(m => m.Id).ToList();
            return View(result);
        }

        // GET: Create
        public IActionResult Create()
        {
            var collectionId = _db.Collections.Max(m => m.Collection_Id);
            var model = new CollectionModel();
            model.Collection_Id = collectionId + 1;
            return View(model);
        }
        [HttpPost]
        // POST: Create
        public IActionResult Create(CollectionModel collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _db.Collections.Add(collection);
            _db.SaveChanges();

            var collectionList = _db.Collections.OrderBy(m => m.Id).ToList();

            return View("Index", collectionList);
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var result = _db.Collections.Where(m => m.Id == id).FirstOrDefault();

            return View(result);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(CollectionModel collection)
        {
            var oldCollection = _db.Collections.Where(m => m.Id == collection.Id).FirstOrDefault();
            _db.Collections.Remove(oldCollection);
            _db.Collections.Add(collection);
            _db.SaveChanges();
            var collectionList = _db.Collections.OrderBy(m => m.Id).ToList();

            return View("Index", collectionList);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var collection = _db.Collections.Where(m => m.Id == id).FirstOrDefault();
            return View(collection);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCollection(int id)
        {
            var collection = _db.Collections.Where(m => m.Id == id).FirstOrDefault();
            _db.Remove(collection);
            _db.SaveChanges();

            var collectionList = _db.Collections.OrderBy(m => m.Id).ToList();
            return View("Index", collectionList);
        }
        // POST: Delete


    }
}
