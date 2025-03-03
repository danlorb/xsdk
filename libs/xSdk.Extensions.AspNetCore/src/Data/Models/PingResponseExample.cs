using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Data.Models
{
    internal class PingResponseExample : IExamplesProvider<string>
    {
        public string GetExamples()
        {
            return "pong";
        }
    }
}
