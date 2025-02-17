using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace xSdk.Extensions.Command
{
    internal class DefaultCommand : Command<DefaultCommandSettings>
    {
        public override int Execute(CommandContext context, DefaultCommandSettings settings)
        {
            return 0;
        }
    }
}
