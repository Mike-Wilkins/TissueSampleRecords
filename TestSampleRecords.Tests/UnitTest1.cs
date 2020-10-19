using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TissueSampleRecords.Models;
using TissueSampleRecords.Repositories;
using TissueSampleRecords.Services;
using Xunit;



namespace TestSampleRecords.Tests
{
    public class UnitTest1
    {
        List<CollectionModel> CollectionInMemoryDb()
        {
            List<CollectionModel> collectionList = new List<CollectionModel>()
            {
                new CollectionModel
                {
                    Id = 1,
                    Collection_Id = 1,
                    Disease_Term = "Lymphoblastoid cell lines",
                    Title = "Fit and well"
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
            Assert.Equal("Lymphoblastoid cell lines", savedCollection.Disease_Term);

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
