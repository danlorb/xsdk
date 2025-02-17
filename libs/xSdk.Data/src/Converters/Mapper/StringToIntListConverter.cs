using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class StringToIntListConverter : IValueConverter<string, IEnumerable<int>>
    {
        public IEnumerable<int> Convert(string sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return sourceMember
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => System.Convert.ToInt32(x));
            }
            return new List<int>();
        }
    }
}
