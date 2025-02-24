using System.Collections;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using xSdk.Extensions.Commands;
using xSdk.Hosting;
using xSdk.Shared;

namespace xSdk.Extensions.Variable
{
    internal partial class VariableService : IVariableService
    {
        private readonly IConfiguration? _config;

        public VariableService(IConfiguration? config)
        {
            this._config = config;

            InitProviders();
        }

        public Dictionary<string, object> ToDictionary()
        {
            var result = new Dictionary<string, object>();

            foreach (var variable in this.Variables)
            {
                try
                {
                    if (TryReadVariableValue<object>(variable.Name, out object value))
                    {
                        result.AddOrNew(variable.Name, value);
                    }
                }
                catch
                {
                    // Nothing to tell
                }
            }

            return result;
        }

        internal void AddEnvironmentVariables()
        {
            var items = Environment.GetEnvironmentVariables();

            // GetPrimaryKey Items to Dictionary
            var dic = new ConcurrentDictionary<string, object>();
            foreach (DictionaryEntry item in items)
            {
                dic.AddOrNew(item.Key.ToString(), item.Value);
            }

            // Execute in Parallel
            Parallel.ForEach(
                dic,
                item =>
                {
                    var value = item.Value?.ToString();
                    var valueType = TypeConverter.GetValueType(value);

                    if (valueType != null)
                    {
                        var name = item.Key.ToString();
                        var variable = this.LoadVariableInternal(name);
                        if (variable == null)
                        {
                            variable = Variable
                                .Create(name, valueType)
                                .Protect()
                                .DisablePrefix()
                                .Hide();

                            NewVariable(variable);
                        }
                    }
                }
            );
        }
    }
}
