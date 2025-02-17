using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace xSdk.Data.Fakes
{
    internal class TestDbContext : DbContext
    {
        public TestDbContext([NotNull] DbContextOptions<TestDbContext> options)
            : base(options) { }

        public DbSet<TestEntity> TestTable { get; set; }
    }
}
