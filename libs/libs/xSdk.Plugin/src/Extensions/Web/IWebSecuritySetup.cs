using xSdk.Extensions.Variable;

namespace xSdk.Extensions.Web
{
    public interface IWebSecuritySetup : ISetup
    {
        bool IsCorsEnabled { get; set; }
        string Origins { get; set; }
    }
}
