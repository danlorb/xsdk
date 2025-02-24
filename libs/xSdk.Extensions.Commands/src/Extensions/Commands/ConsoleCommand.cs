using System.ComponentModel;
using Spectre.Console.Cli;

namespace xSdk.Extensions.Commands
{
    [Description(Definitions.HelpText)]
    internal class ConsoleCommand : Command<EmptyCommandSettings>
    {
        internal static class Definitions
        {
            public const string Name = "console";
            public const string HelpText = "Creates a interactive REPL Console";
        }

        public override int Execute(CommandContext context, EmptyCommandSettings settings)
        {
            return 0;
        }
    }
}
