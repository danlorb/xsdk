using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Data
{
    internal class InternalDatabaseSetup : DatabaseSetup
    {
        #region Only for Initialization of Repository in Factory needed

        internal IDatabaseSetup Setup { get; set; }

        internal Type DatabaseType { get; set; }

        internal Type ConnectionBuilderType { get; set; }

        internal string Name { get; set; }

        #endregion
    }
}
