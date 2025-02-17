using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace xSdk.Extensions.Command
{
    internal class ServiceResolver(IServiceProvider provider) : ITypeResolver
    {
        public object? Resolve(Type type)
        {
            if (type == null)
            {
                return null;
            }

            return provider.GetService(type);
        }
    }
}
