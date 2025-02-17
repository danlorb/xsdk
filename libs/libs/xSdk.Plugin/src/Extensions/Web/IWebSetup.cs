using xSdk.Extensions.Variable;

namespace xSdk.Extensions.Web
{
    public interface IWebSetup : ISetup
    {
        bool AllowSystemPorts { get; set; }
        string Bind { get; set; }
        int Grpc { get; set; }
        int Http { get; set; }
        int Https { get; set; }
        bool IsHttpsEnabled { get; }
        string TlsCertFile { get; set; }
        string TlsKeyFile { get; set; }
    }
}
