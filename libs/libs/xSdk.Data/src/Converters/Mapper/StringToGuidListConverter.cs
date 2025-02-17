using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class StringToGuidListConverter : IValueConverter<string, IEnumerable<Guid>>
    {
        public IEnumerable<Guid> Convert(string sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return sourceMember
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Guid.Parse(x));
            }
            return new List<Guid>();
        }
    }
}
