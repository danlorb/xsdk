using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using xSdk.Data;

namespace xSdk.Demos.Data
{
    internal class SampleDbContext : MongoDbContext<SampleDbContext>
    {
        public SampleDbContext([NotNull] DbContextOptions<SampleDbContext> options) : base(options) { }

        public DbSet<SampleEntity> Sample { get; set; }
    }
}
