using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xSdk.Data;

namespace xSdk.Extensions.Links
{
    public interface ILinksService
    {
        Task AddLinksAsync<TModel>(IEnumerable<TModel> model, CancellationToken cancellationToken = default)
            where TModel : class, IModel;

        Task AddLinksAsync<TModel>(TModel model, CancellationToken cancellationToken = default)
            where TModel : class, IModel;
    }
}
