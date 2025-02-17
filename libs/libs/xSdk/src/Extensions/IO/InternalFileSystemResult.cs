using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zio;

namespace xSdk.Extensions.IO
{
    internal class InternalFileSystemResult : IFileSystemResult
    {
        public IFileSystem App { get; internal set; }

        public IFileSystem Data { get; internal set; }
    }
}
