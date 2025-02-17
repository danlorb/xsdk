using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class StringToStringListConverter : IValueConverter<string, IEnumerable<string>>
    {
        public IEnumerable<string> Convert(string sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
                return sourceMember
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim());

            return new List<string>();
        }
    }
}
