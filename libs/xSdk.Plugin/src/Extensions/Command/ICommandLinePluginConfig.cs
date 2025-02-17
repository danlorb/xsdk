using Spectre.Console.Cli;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Command
{
    public interface ICommandLinePluginConfig : IPlugin
    {
        void ConfigureCommandLine(IConfigurator configurator);
    }
}
