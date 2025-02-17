using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Shared
{
    public static class StringHelper
    {
        public static string RemoveSpecialChars(string value)
        {
            // Remove special Characters (see https://github.com/cloudevents/spec/blob/v1.0.1/spec.md)
            var sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
