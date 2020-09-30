using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TissueSampleRecords.Models;
using TissueSampleRecords.Services;

namespace TissueSampleRecords.Services
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<SampleModel> Samples { get; set; }
        public DbSet<CollectionModel> Collections { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        
    }
}

