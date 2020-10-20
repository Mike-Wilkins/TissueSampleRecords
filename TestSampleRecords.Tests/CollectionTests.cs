using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TissueSampleRecords.Models;
using TissueSampleRecords.Repositories;
using TissueSampleRecords.Services;
using Xunit;


namespace TestSampleRecords.Tests
{
    public class CollectionTests
    {
        List<CollectionModel> CollectionInMemoryDb()
        {
            List<CollectionModel> collectionList = new List<CollectionModel>()
            {
                new CollectionModel
                {
                    Id = 1,
                    Collection_Id = 1305,
                    Disease_Term = "Lymphoblastoid cell lines",
                    Title = "Fit and well"
                },
                new CollectionModel
                {
                    Id = 2,
                    Collection_Id = 1306,
                    Disease_Term = "Chronic fatigue syndrome ",
                    Title = "Samples available include ME/CFS Cases 5 "
                }
            };
            return collectionList;
        }

        [Fact]
        public void SQLCollectionRepository_Should_Add_Collection()
        {
            ICollectionRepository sut = GetInMemoryCollectionRepository();
            List<CollectionModel> collection = CollectionInMemoryDb();

            CollectionModel savedCollection = sut.Add(collection[0]);

            Assert.Single(sut.GetAllCollections());
            Assert.Equal(1305, savedCollection.Collection_Id);
            Assert.Equal("Lymphoblastoid cell lines", savedCollection.Disease_Term);
            Assert.Equal("Fit and well", savedCollection.Title);

        }
        [Fact]
        public void SQLCollectionRepository_Should_Delete_Collection()
        {
            ICollectionRepository sut = GetInMemoryCollectionRepository();
            List<CollectionModel> collection = CollectionInMemoryDb();

            sut.Add(collection[0]);
            CollectionModel savedCollection2 = sut.Add(collection[1]);
            Assert.Equal(2, sut.GetAllCollections().Count());

            Assert.Equal(2, sut.GetAllCollections().Count());

            sut.Delete(collection[0].Collection_Id);

            Assert.Single(sut.GetAllCollections());
            Assert.Equal(1306, savedCollection2.Collection_Id);

        }
        [Fact]
        public void SQLCollectionRepository_Should_Get_Collection_By_Id()
        {
            ICollectionRepository sut = GetInMemoryCollectionRepository();
            List<CollectionModel> collection = CollectionInMemoryDb();

            sut.Add(collection[0]);
            sut.Add(collection[1]);
            Assert.Equal(2, sut.GetAllCollections().Count());

            Assert.Equal(1305, sut.GetCollection(collection[0].Collection_Id).Collection_Id);
        }

        [Fact]
        public void SQLCollectionRepository_Should_Update_Collection_Details()
        {
            ICollectionRepository sut = GetInMemoryCollectionRepository();
            List<CollectionModel> collection = CollectionInMemoryDb();

            sut.Add(collection[0]);
            sut.Add(collection[1]);

            Assert.Equal(1305, collection[0].Collection_Id);
            collection[0].Collection_Id = 5555;

            sut.Update(collection[0]);

            Assert.Equal(5555, collection[0].Collection_Id);
        }

        private ICollectionRepository GetInMemoryCollectionRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "CollectionDatabase");

            options = builder.Options;
            ApplicationDbContext collectionDataContext = new ApplicationDbContext(options);
            collectionDataContext.Database.EnsureDeleted();
            collectionDataContext.Database.EnsureCreated();

            return new SQLCollectionRepository(collectionDataContext);
        }
    }

    
}
