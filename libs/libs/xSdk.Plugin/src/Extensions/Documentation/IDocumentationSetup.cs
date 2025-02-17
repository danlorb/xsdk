using xSdk.Extensions.Variable;

namespace xSdk.Extensions.Documentation
{
    public interface IDocumentationSetup : ISetup
    {
        string RoutePrefix { get; set; }
        bool ShowVariableDocumentation { get; set; }
    }
}
