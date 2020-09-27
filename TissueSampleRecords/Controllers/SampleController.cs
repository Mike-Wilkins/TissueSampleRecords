using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TissueSampleRecords.Models;
using TissueSampleRecords.Services;
using X.PagedList;

namespace TissueSampleRecords.Controllers
{
    public class SampleController : Controller
    {
        private ApplicationDbContext _db;

        public SampleController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Sample Details
        public IActionResult Details(int id, int? page)
        {
            var collectionType = _db.Collections.Where(m => m.Id == id).FirstOrDefault();
            ViewBag.CollectionTitle = collectionType.Title;
            ViewBag.CollectionId = collectionType.Id;

            var pageNumber = page ?? 1;
            var sampleList = _db.Samples.Where(m => m.Collection_Title == collectionType.Title).ToList().ToPagedList(pageNumber, 5);

            return View(sampleList);
        }

        // GET: Create
        public IActionResult Create(int id)
        {
            var collectionType = _db.Samples.Where(m => m.Id == id).FirstOrDefault();
            ViewBag.CollectionTitle = collectionType.Collection_Title;
            ViewBag.CollectionId = collectionType.Id;
            return View();
        }

        // POST: Create
        [HttpPost]
        public IActionResult Create(SampleModel sample)
        {
            var collectionTitle = _db.Samples.Where(m => m.Id == sample.Id).FirstOrDefault();

            var newSample = new SampleModel();

            newSample.Donor_Count = sample.Donor_Count;
            newSample.Material_Type = sample.Material_Type;
            newSample.Collection_Title = collectionTitle.Collection_Title;
            newSample.Last_Updated = DateTime.Now.ToShortDateString();

            _db.Samples.Add(newSample);
            _db.SaveChanges();

            ViewBag.CollectionId = collectionTitle.Collection_Title;

            var sampleList = _db.Samples.Where(m => m.Collection_Title == collectionTitle.Collection_Title).ToList();

            return View("Details", sampleList);

        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var sample = _db.Samples.Where(m => m.Id == id).FirstOrDefault();
            var collectionType = _db.Collections.Where(m => m.Title == sample.Collection_Title).FirstOrDefault();

            ViewBag.CollectionTitle = collectionType.Title;
            ViewBag.CollectionId = collectionType.Id;
            return View(sample);
        }
        // POST: Edit
        [HttpPost]
        public IActionResult Edit(SampleModel sample)
        {
            var oldSample = _db.Samples.Where(m => m.Id == sample.Id).FirstOrDefault();
            _db.Samples.Remove(oldSample);

            sample.Collection_Title = oldSample.Collection_Title;
            sample.Last_Updated = DateTime.Now.ToShortDateString();

            _db.Samples.Add(sample);
            _db.SaveChanges();

            var sampleList = _db.Samples.Where(m => m.Collection_Title == sample.Collection_Title).ToList();

            ViewBag.CollectionTitle = sample.Collection_Title;
            ViewBag.CollectionId = sample.Id;

            return View("Details", sampleList);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var sample = _db.Samples.Where(m => m.Id == id).FirstOrDefault();
            var collectionType = _db.Collections.Where(m => m.Title == sample.Collection_Title).FirstOrDefault();
            ViewBag.CollectionId = collectionType.Id;
            return View(sample);
        }
        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteSample(int id)
        {
            var sample = _db.Samples.Where(m => m.Id == id).FirstOrDefault();
            _db.Samples.Remove(sample);
            _db.SaveChanges();

            var sampleList = _db.Samples.Where(m => m.Collection_Title == sample.Collection_Title).ToList();

            return View("Details", sampleList);
        }
    }
}
