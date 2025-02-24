using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public interface IPagedLinkContainer : ILinkContainer
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
        int PageCount { get; set; }
    }
}
