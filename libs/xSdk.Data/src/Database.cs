using System.Collections.Concurrent;
using NLog;
using xSdk.Shared;

namespace xSdk.Data
{
    public abstract class Database : IDatabase
    {
        private static ConcurrentDictionary<string, object> _connections;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IDatabaseSetup _setup;
        private string _name;
        private IConnectionBuilder _connectionStringBuilder;

        private object _syncObject = new object();

        public Database()
        {
            _connections = new ConcurrentDictionary<string, object>();

            AppDomain.CurrentDomain.ProcessExit += (sender, args) =>
            {
                Close();
            };
        }

        #region Dispose Handling

        ~Database() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Close();
        }

        #endregion

        public void Close()
        {
            logger.Trace("Try to close Database");

            lock (_syncObject)
            {
                if (_connections.Any())
                {
                    var keys = _connections.Keys;
                    foreach (var key in keys)
                    {
                        var connection = _connections[key];
                        _connections.Remove(key, out object dummy);

                        Disconnect(connection);
                    }
                }
                else
                    Disconnect();
            }
        }

        public TConnection Open<TConnection>(bool persistConnection = false)
            where TConnection : class
        {
            object connection = default;

            logger.Trace("Try to open Database for Connection '{0}'", typeof(TConnection));

            lock (_syncObject)
            {
                ConnectionBuilder builder = _connectionStringBuilder as ConnectionBuilder;

                if (persistConnection)
                {
                    var uniqueKey = Base64Helper.ConvertToBase64(_name);

                    if (_connections.ContainsKey(uniqueKey))
                        connection = _connections[uniqueKey];

                    connection = Open<TConnection>(connection, () => builder.InitializeConnection(_setup));
                    _connections.AddOrNew(uniqueKey, connection);
                }
                else
                    connection = Open<TConnection>(() => builder.InitializeConnection(_setup));
            }

            return connection as TConnection;
        }

        internal void Configure(IConnectionBuilder connectionStringBuilder, InternalDatabaseSetup setup)
        {
            logger.Trace("Configure new Database");

            _connectionStringBuilder = connectionStringBuilder;
            _setup = setup.Setup;
            _name = setup.Name;
        }

        protected virtual void Disconnect() { }

        protected virtual void Disconnect(object connection) { }

        protected virtual TConnection Open<TConnection>(Func<object> connectionStringBuilder)
            where TConnection : class
        {
            return default;
        }

        protected virtual TConnection Open<TConnection>(object? connection, Func<object> connectionStringBuilder)
            where TConnection : class
        {
            return default;
        }
    }
}
