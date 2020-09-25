using Microsoft.EntityFrameworkCore;
using TissueSampleRecords.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TissueSampleRecords.Services
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CollectionModel> Collections { get; set; }
        public DbSet<SampleModel> Samples { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
