
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TissueSampleRecords.Models;
using TissueSampleRecords.Repositories;
using TissueSampleRecords.Services;
using Xunit;

namespace TestSampleRecords.Tests
{
    public class SampleTests
    {
        private List<SampleModel> sampleInMemoryDb()
        {
            List<SampleModel> sampleList = new List<SampleModel>()
            {
                new SampleModel
                {
                    Id = 1,
                    Donor_Count = 123,
                    Material_Type = "ME219 Endoscopic Biopsy",
                    Last_Updated = "28/09/2020",
                    Collection_Title = "Phase II multicentre study",
                    Collection_Id = 2203
                },
                new SampleModel
                {
                    Id = 2,
                    Donor_Count = 5592,
                    Material_Type = "Cerebrospinal fluid",
                    Last_Updated = "08/11/2014",
                    Collection_Title = "Samples available include ME/CFS Cases 5",
                    Collection_Id = 2204
                }
            };
            return sampleList;
        }

        [Fact]
        public void SQLSampleRepository_Should_Add_Sample()
        {
            ISampleRepository sut = GetInMemorySampleRepository();
            List<SampleModel> samples = sampleInMemoryDb();

            SampleModel savedSample = sut.Add(samples[0]);

            Assert.Single(sut.GetAllSamples());

            Assert.Equal("ME219 Endoscopic Biopsy", savedSample.Material_Type);
            Assert.Equal("28/09/2020", savedSample.Last_Updated);
            Assert.Equal("Phase II multicentre study", savedSample.Collection_Title);
            Assert.Equal(2203, savedSample.Collection_Id);
        }

        [Fact]
        public void SQLSampleRepository_Should_Delete_Sample()
        {
            ISampleRepository sut = GetInMemorySampleRepository();
            List<SampleModel> samples = sampleInMemoryDb();

            sut.Add(samples[0]);
            Assert.Single(sut.GetAllSamples());

            sut.Delete(samples[0].Id);

            Assert.Empty(sut.GetAllSamples());
        }

        [Fact]
        public void SQLSampleRepository_Should_Update_Sample()
        {
            ISampleRepository sut = GetInMemorySampleRepository();
            List<SampleModel> samples = sampleInMemoryDb();

            sut.Add(samples[0]);
            Assert.Equal("28/09/2020", samples[0].Last_Updated);

            samples[0].Last_Updated = "14/10/2020";
            sut.Update(samples[0]);

            Assert.Equal("14/10/2020", samples[0].Last_Updated);
        }

        [Fact]
        public void SQLSpampleRepository_Should_Get_All_Samples()
        {
            ISampleRepository sut = GetInMemorySampleRepository();
            List<SampleModel> samples = sampleInMemoryDb();
            SampleModel savedSample1 = sut.Add(samples[0]);
            SampleModel savedSample2 = sut.Add(samples[1]);

            sut.GetAllSamples();

            Assert.Equal(2, samples.Count);
            Assert.Equal(123, savedSample1.Donor_Count);
            Assert.Equal(5592, savedSample2.Donor_Count);
        }

        [Fact]
        public void SQLSampleRepository_Get_Sample_By_Id()
        {
            ISampleRepository sut = GetInMemorySampleRepository();
            List<SampleModel> samples = sampleInMemoryDb();
            SampleModel savedSample1 = sut.Add(samples[0]);

            sut.GetSample(savedSample1.Id);

            Assert.Equal(123, savedSample1.Donor_Count);
        }

        private ISampleRepository GetInMemorySampleRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "SampleDatabase");

            options = builder.Options;
            ApplicationDbContext sampleDataContext = new ApplicationDbContext(options);
            sampleDataContext.Database.EnsureDeleted();
            sampleDataContext.Database.EnsureCreated();

            return new SQLSampleRepository(sampleDataContext);
        }
    }
}
