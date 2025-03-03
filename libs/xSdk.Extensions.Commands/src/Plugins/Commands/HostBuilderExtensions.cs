using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using xSdk.Extensions.Commands;
using xSdk.Hosting;

namespace xSdk.Plugins.Commands
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder EnableCommands(this IHostBuilder builder)
        {
            builder.EnablePlugin<CommandPlugin>();

            return builder;
        }

        public static IHostBuilder EnableCommands<TPluginBuilder>(this IHostBuilder builder)
            where TPluginBuilder : ICommandLinePluginBuilder
        {
            builder.EnableCommands().EnablePlugin<TPluginBuilder>();

            return builder;
        }
    }
}
