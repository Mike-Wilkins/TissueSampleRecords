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
        private IApplicationDbContext _db;

        public SampleController(IApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Sample Details
        public IActionResult Details(int id, int? page)
        {
            var collectionType = _db.Collections.Where(m => m.Collection_Id == id).FirstOrDefault();
            ViewBag.CollectionTitle = collectionType.Title;
            ViewBag.CollectionId = collectionType.Collection_Id;

            var pageNumber = page ?? 1;
            var sampleList = _db.Samples.Where(m => m.Collection_Id == id).ToList().ToPagedList(pageNumber, 5);
            return View(sampleList);
        }

        // GET: Create
        public IActionResult Create(int id)
        {
            var collectionType = _db.Collections.Where(m => m.Collection_Id == id).FirstOrDefault();
            ViewBag.CollectionTitle = collectionType.Title;
            ViewBag.CollectionId = collectionType.Collection_Id;
            return View();
        }

        // POST: Create
        [HttpPost]
        public IActionResult Create(SampleModel sample, int? page, int id)
        {
            var collectionTitle = _db.Collections.Where(m => m.Collection_Id == id).FirstOrDefault();

            var newSample = new SampleModel();

            newSample.Collection_Id = id;
            newSample.Donor_Count = sample.Donor_Count;
            newSample.Material_Type = sample.Material_Type;
            newSample.Collection_Title = collectionTitle.Title;
            newSample.Last_Updated = DateTime.Now.ToShortDateString();

            _db.Samples.Add(newSample);
            _db.SaveChanges();

            ViewBag.CollectionId = collectionTitle.Collection_Id;
            ViewBag.CollectionTitle = collectionTitle.Title;

            var pageNumber = page ?? 1;
            var sampleList = _db.Samples.Where(m => m.Collection_Id == id).ToList().ToPagedList(pageNumber, 5);

            return View("Details", sampleList);

        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var sample = _db.Samples.Where(m => m.Id == id).FirstOrDefault();
            //var collectionType = _db.Collections.Where(m => m.Title == sample.Collection_Title).FirstOrDefault();

            ViewBag.CollectionTitle = sample.Collection_Title;
            ViewBag.CollectionId = sample.Collection_Id;
            return View(sample);
        }
        // POST: Edit
        [HttpPost]
        public IActionResult Edit(SampleModel sample, int? page)
        {
            var oldSample = _db.Samples.Where(m => m.Id == sample.Id).FirstOrDefault();
            _db.Samples.Remove(oldSample);

            sample.Collection_Id = oldSample.Collection_Id;
            sample.Collection_Title = oldSample.Collection_Title;
            sample.Last_Updated = DateTime.Now.ToShortDateString();

            _db.Samples.Add(sample);
            _db.SaveChanges();

            var pageNumber = page ?? 1;
            var sampleList = _db.Samples.Where(m => m.Collection_Title == sample.Collection_Title).ToList().ToPagedList(pageNumber, 5);

            ViewBag.CollectionTitle = sample.Collection_Title;
            ViewBag.CollectionId = sample.Collection_Id;

            return View("Details", sampleList);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var sample = _db.Samples.Where(m => m.Id == id).FirstOrDefault();
            var collectionType = _db.Collections.Where(m => m.Title == sample.Collection_Title).FirstOrDefault();
            ViewBag.CollectionId = collectionType.Collection_Id;
            return View(sample);
        }
        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteSample(int id, int? page)
        {
            var sample = _db.Samples.Where(m => m.Id == id).FirstOrDefault();
            _db.Samples.Remove(sample);
            _db.SaveChanges();

            var pageNumber = page ?? 1;
            var sampleList = _db.Samples.Where(m => m.Collection_Title == sample.Collection_Title).ToList().ToPagedList(pageNumber, 5);

            ViewBag.CollectionTitle = sample.Collection_Title;
            ViewBag.CollectionId = sample.Collection_Id;

            return View("Details", sampleList);
        }
    }
}
