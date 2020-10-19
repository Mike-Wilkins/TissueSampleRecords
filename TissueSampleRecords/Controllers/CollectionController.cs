using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TissueSampleRecords.Models;
using TissueSampleRecords.Repositories;
using TissueSampleRecords.Services;
using X.PagedList;

namespace TissueSampleRecords.Controllers
{
    public class CollectionController : Controller
    {
        private ICollectionRepository _db;

        public CollectionController(ICollectionRepository db)
        {
            _db = db;
        }

        // GET: Collection List
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var collections = _db.GetAllCollections();
            var result = await collections.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 4);
            return View(result);
        }

        // GET: Create
        public IActionResult Create()
        {
            var collections = _db.GetAllCollections();

            var collectionId = collections.Max(m => m.Collection_Id);
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

            _db.Add(collection);
            var collections = _db.GetAllCollections();

            var pageNumber = page ?? 1;
            var collectionList = await collections.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 4);

            return View("Index", collectionList);
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var collection = _db.GetCollection(id);
            return View(collection);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(CollectionModel collection, int? page)
        {
            _db.Update(collection);

            var pageNumber = page ?? 1;
            var collections = _db.GetAllCollections();
            var collectionList = await collections.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 4);

            return View("Index", collectionList);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var collection = _db.GetCollection(id);
            return View(collection);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCollection(int id, int? page)
        {
            _db.Delete(id);

            var pageNumber = page ?? 1;
            var collections = _db.GetAllCollections();
            var collectionList = await collections.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 4);
            return View("Index", collectionList);
        }
        
    }
}
