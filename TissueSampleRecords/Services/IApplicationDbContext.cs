using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using TissueSampleRecords.Models;
using TissueSampleRecords.Services;



namespace TissueSampleRecords.Services
{
    public interface IApplicationDbContext
    {
        DbSet<SampleModel> Samples { get; set; }
        DbSet<CollectionModel> Collections { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry Remove([NotNullAttribute] object entity);
        EntityEntry<TEntity> Remove<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

       
    }
}
