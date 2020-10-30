using System.Collections.Generic;
using System.Linq;
using TissueSampleRecords.Models;
using TissueSampleRecords.Services;

namespace TissueSampleRecords.Repositories
{
    public class SQLCollectionRepository : ICollectionRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLCollectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public CollectionModel Add(CollectionModel collection)
        {
            _context.Collections.Add(collection);
            _context.SaveChanges();
            return collection;
        }

        public CollectionModel Delete(int id)
        {
            CollectionModel collection = _context.Collections.Where(m => m.Collection_Id == id).FirstOrDefault();
            if (collection != null)
            {
                _context.Collections.Remove(collection);
                _context.SaveChanges();
            }
            return collection;
        }

        public CollectionModel Update(CollectionModel collectionChanges)
        {
            var collection = _context.Collections.Attach(collectionChanges);
            collection.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return collectionChanges;
        }

        public IEnumerable<CollectionModel> GetAllCollections()
        {
            return _context.Collections;
        }

        public CollectionModel GetCollection(int id)
        {
            CollectionModel collection = _context.Collections.Where(m => m.Collection_Id == id).FirstOrDefault();
            return collection;
        }

    }
}
