using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace xSdk.Data
{
    public partial class NoSqlRepository<TEntity>
    {
        public override Task<bool> UpdateAsync(
            IPrimaryKey primary,
            TEntity entity,
            CancellationToken token = default
        )
        {
            _logger.Trace("Update Entity");
            return ExecuteInternalAsync(col => col.UpdateAsync(entity), true, token);
        }
    }
}
