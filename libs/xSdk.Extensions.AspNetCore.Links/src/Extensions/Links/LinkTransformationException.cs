using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public class LinkTransformationException : Exception
    {
        public LinkTransformationContext Context { get; }

        public LinkTransformationException(string message, LinkTransformationContext context)
            : base(message)
        {
            this.Context = context;
        }

        public LinkTransformationException(
            string message,
            Exception innerException,
            LinkTransformationContext context
        )
            : base(message, innerException)
        {
            this.Context = context;
        }
    }
}
