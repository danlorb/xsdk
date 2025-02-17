using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class IntListToStringConverter : IValueConverter<IEnumerable<int>, string>
    {
        public string Convert(IEnumerable<int> sourceMember, ResolutionContext context)
        {
            if (sourceMember != null && sourceMember.Any())
            {
                return sourceMember.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b);
            }
            return null;
        }
    }
}
