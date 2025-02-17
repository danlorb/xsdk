using xSdk.Extensions.Variable;

namespace xSdk.Extensions.DataProtection
{
    public interface IDataProtectionSetup : ISetup
    {
        string ApplicationDiscriminator { get; set; }
        string ApplicationName { get; set; }
        string KeyLifetime { get; set; }
    }
}
