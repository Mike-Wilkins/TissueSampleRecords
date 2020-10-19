using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TissueSampleRecords.Models;

namespace TissueSampleRecords.Repositories
{
    public interface ICollectionRepository
    {
        CollectionModel GetCollection(int id);
        IEnumerable<CollectionModel> GetAllCollections();
        CollectionModel Add(CollectionModel collection);
        CollectionModel Delete(int id);
        CollectionModel Update(CollectionModel collectionChanges);
        
    }
}
