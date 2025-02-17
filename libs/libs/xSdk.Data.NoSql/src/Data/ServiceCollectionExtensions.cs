using System;

namespace xSdk.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IDatalayerBuilder UseNoSql(
            this IDatalayerBuilder builder,
            string name,
            Action<NoSqlDatabaseSetup> configure
        ) =>
            builder.UseDatabase<NoSqlDatabase, NoSqlDatabaseSetup, NoSqlConnectionBuilder>(
                name,
                configure
            );
    }
}
