using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class StringListToStringConverter : IValueConverter<IEnumerable<string>, string>
    {
        public string Convert(IEnumerable<string> sourceMember, ResolutionContext context)
        {
            if (sourceMember != null && sourceMember.Any())
                return sourceMember.Aggregate((a, b) => a + ", " + b);

            return null;
        }
    }
}
