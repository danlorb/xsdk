using System;

namespace xSdk.Data
{
    public static class IDatalayerBuilderExtensions
    {
        public static IDatalayerBuilder UseVault(this IDatalayerBuilder builder, string name, Action<VaultDatabaseSetup> configure)
        {
            builder.UseDatabase<VaultDatabase, VaultDatabaseSetup, VaultConnectionBuilder>(name, configure);

            builder.MapRepository<IVaultRepository, VaultRepository>();
            return builder;
        }
    }
}
