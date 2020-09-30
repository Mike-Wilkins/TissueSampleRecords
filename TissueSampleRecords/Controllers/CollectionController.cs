using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TissueSampleRecords.Models;
using TissueSampleRecords.Services;
using X.PagedList;

namespace TissueSampleRecords.Controllers
{
    public class CollectionController : Controller
    {
        private IApplicationDbContext _db;

        public CollectionController(IApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Collection List
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var result = await _db.Collections.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 4);
            return View(result);
        }

        // GET: Create
        public IActionResult Create()
        {
            var collectionId =  _db.Collections.Max(m => m.Collection_Id);
            var model = new CollectionModel();
            model.Collection_Id = collectionId + 1;
            return View(model);
        }
        [HttpPost]
        // POST: Create
        public async Task<IActionResult> Create(CollectionModel collection, int? page)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _db.Collections.Add(collection);
            _db.SaveChanges();

            var pageNumber = page ?? 1;
            var collectionList = await _db.Collections.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 4);

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
        public async Task<IActionResult> Edit(CollectionModel collection, int? page)
        {
            var oldCollection = _db.Collections.Where(m => m.Id == collection.Id).FirstOrDefault();
            _db.Collections.Remove(oldCollection);
            _db.Collections.Add(collection);
            _db.SaveChanges();

            var pageNumber = page ?? 1;
            var collectionList = await _db.Collections.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 4);

            return View("Index", collectionList);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var collection = _db.Collections.Where(m => m.Collection_Id == id).FirstOrDefault();
            return View(collection);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCollection(int id, int? page)
        {
            var collection = _db.Collections.Where(m => m.Collection_Id == id).FirstOrDefault();
            _db.Remove(collection);

            var deleteSamples = _db.Samples.Where(m => m.Collection_Id == collection.Collection_Id);
            _db.Samples.RemoveRange(deleteSamples);

            _db.SaveChanges();

            var pageNumber = page ?? 1;
            var collectionList = await _db.Collections.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 4);
            return View("Index", collectionList);
        }
        // POST: Delete


    }
}
