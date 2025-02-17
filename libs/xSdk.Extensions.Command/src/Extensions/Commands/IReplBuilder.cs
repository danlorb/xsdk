namespace xSdk.Extensions.Command
{
    public interface IReplBuilder
    {
        string Prompt { get; set; }

        Func<string> PromptFactory { get; set; }
    }
}
