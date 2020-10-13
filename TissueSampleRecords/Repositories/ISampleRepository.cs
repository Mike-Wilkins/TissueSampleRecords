using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TissueSampleRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace TissueSampleRecords.Repositories
{
    public interface ISampleRepository
    {
        SampleModel GetSample(int id);
        IEnumerable<SampleModel> GetAllSamples();
        SampleModel Add(SampleModel collection);
        SampleModel Delete(int id);
        CollectionModel GetCollection(int id);
    }
}
