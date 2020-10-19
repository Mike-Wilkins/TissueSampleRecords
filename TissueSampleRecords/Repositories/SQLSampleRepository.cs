using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TissueSampleRecords.Models;
using TissueSampleRecords.Services;
using Microsoft.EntityFrameworkCore;

namespace TissueSampleRecords.Repositories
{
    public class SQLSampleRepository : ISampleRepository
    {
        private ApplicationDbContext _context;

        public SQLSampleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public SampleModel Add(SampleModel sample)
        {
            _context.Samples.Add(sample);
            _context.SaveChanges();
            return sample;
        }

        public SampleModel Delete(int id)
        {
            SampleModel sample = _context.Samples.Where(m => m.Id == id).FirstOrDefault();
            if (sample != null)
            {
                _context.Samples.Remove(sample);
                _context.SaveChanges();
            }
            return sample;
        }

        public SampleModel Update(SampleModel sampleChanges)
        {
            var sample = _context.Samples.Attach(sampleChanges);
            sample.State = EntityState.Modified;
            _context.SaveChanges();
            return sampleChanges;
        }

        public IEnumerable<SampleModel> GetAllSamples()
        {
            return _context.Samples;
        }

        public SampleModel GetSample(int id)
        {
            SampleModel sample = _context.Samples.Where(m => m.Id == id).FirstOrDefault();
            return sample;
        }

        public CollectionModel GetCollection(int id)
        {
            CollectionModel collection = _context.Collections.Where(m => m.Collection_Id == id).FirstOrDefault();
            return collection;
        }
    }
}
