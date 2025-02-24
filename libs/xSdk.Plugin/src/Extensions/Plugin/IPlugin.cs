namespace xSdk.Extensions.Plugin
{
    public interface IPlugin
    {
        string? Name { get; }

        Version? Version { get; }

        bool IsEnabled { get; }

        //string? Description { get; }

        //string? ProductVersion { get; }

        //string? Tag { get; }

        //Type? Type { get; }

        //List<string> Tags { get; }
    }
}
