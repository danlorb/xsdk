using xSdk.Extensions.Variable;

namespace xSdk.Plugins.WebSecurity
{
    public interface IWebSecuritySetup : ISetup
    {
        bool IsCorsEnabled { get; set; }

        string Origins { get; set; }
    }
}
