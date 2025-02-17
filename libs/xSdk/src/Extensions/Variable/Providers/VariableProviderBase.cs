using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Variable.Providers
{
    internal abstract class VariableProviderBase : VariableProvider
    {
        protected Variable Cast(IVariable variable) => (Variable)variable;

        internal virtual void Reset() { }
    }
}
