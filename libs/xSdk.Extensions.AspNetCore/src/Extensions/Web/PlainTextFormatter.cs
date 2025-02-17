using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace xSdk.Extensions.Web
{
    internal class PlainTextFormatter : TextInputFormatter
    {
        public PlainTextFormatter()
        {
            SupportedMediaTypes.Add("text/plain");
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }

        public override async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            string data = await ReadInternalAsync(context);

            return InputFormatterResult.Success(data);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context,
            Encoding encoding
        )
        {
            string data = await ReadInternalAsync(context);

            return InputFormatterResult.Success(data);
        }

        private async Task<string> ReadInternalAsync(InputFormatterContext context)
        {
            string data = null;
            using (var streamReader = new StreamReader(context.HttpContext.Request.Body))
            {
                data = await streamReader.ReadToEndAsync();
            }

            return data;
        }
    }
}
