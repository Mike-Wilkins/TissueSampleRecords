using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TissueSampleRecords.Models;
using TissueSampleRecords.Repositories;
using TissueSampleRecords.Services;
using X.PagedList;


namespace TissueSampleRecords.Controllers
{
    public class SampleController : Controller
    {
        private ISampleRepository _db;

        public SampleController(ISampleRepository db)
        {
            _db = db;
        }

        // GET: Sample Details
        public async Task<IActionResult> Details(int id, int? page)
        {
            var collections = _db.GetCollection(id);
           
            ViewBag.CollectionTitle = collections.Title;
            ViewBag.CollectionId = collections.Collection_Id;

            var pageNumber = page ?? 1;
            var samples = _db.GetAllSamples();
            var sampleList = await samples.Where(m => m.Collection_Id == id).ToPagedListAsync(pageNumber, 5);
            return View(sampleList);
        }

        // GET: Create
        public IActionResult Create(int id)
        {
            var collections = _db.GetCollection(id);
            ViewBag.CollectionTitle = collections.Title;
            ViewBag.CollectionId = collections.Collection_Id;
            return View();
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(SampleModel sample, int? page, int id)
        {
            var collections = _db.GetCollection(id);

            var newSample = new SampleModel();
            newSample.Collection_Id = id;
            newSample.Donor_Count = sample.Donor_Count;
            newSample.Material_Type = sample.Material_Type;
            newSample.Collection_Title = collections.Title;
            newSample.Last_Updated = DateTime.Now.ToShortDateString();

            _db.Add(newSample);

            ViewBag.CollectionId = collections.Collection_Id;
            ViewBag.CollectionTitle = collections.Title;

            var samples = _db.GetAllSamples();

            var pageNumber = page ?? 1;
            var sampleList = await samples.Where(m => m.Collection_Id == id).ToPagedListAsync(pageNumber, 5);

            return View("Details", sampleList);

        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var sample = _db.GetSample(id);
            ViewBag.CollectionTitle = sample.Collection_Title;
            ViewBag.CollectionId = sample.Collection_Id;
            return View(sample);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(SampleModel sample, int? page)
        {
            var oldSample = _db.GetSample(sample.Id);
            _db.Delete(oldSample.Id);

            var editedSample = new SampleModel();

            editedSample.Collection_Id = oldSample.Collection_Id;
            editedSample.Collection_Title = oldSample.Collection_Title;
            editedSample.Last_Updated = DateTime.Now.ToShortDateString();
            editedSample.Material_Type = sample.Material_Type;
            editedSample.Donor_Count = sample.Donor_Count;

            _db.Add(editedSample);

            var samples = _db.GetAllSamples();

            var pageNumber = page ?? 1;
            var sampleList = await samples.Where(m => m.Collection_Title == oldSample.Collection_Title).ToPagedListAsync(pageNumber, 5);

            ViewBag.CollectionTitle = oldSample.Collection_Title;
            ViewBag.CollectionId = oldSample.Collection_Id;

            return View("Details", sampleList);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var sample = _db.GetSample(id);
            var collections = _db.GetCollection(sample.Collection_Id);

            ViewBag.CollectionId = collections.Collection_Id;
            return View(sample);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSample(int id, int? page)
        {
            var sample = _db.GetSample(id);
            _db.Delete(id);
           
            var samples = _db.GetAllSamples();
            var pageNumber = page ?? 1;
            var sampleList = await samples.Where(m => m.Collection_Title == sample.Collection_Title).ToPagedListAsync(pageNumber, 5);

            ViewBag.CollectionTitle = sample.Collection_Title;
            ViewBag.CollectionId = sample.Collection_Id;

            return View("Details", sampleList);
        }
    }
}
