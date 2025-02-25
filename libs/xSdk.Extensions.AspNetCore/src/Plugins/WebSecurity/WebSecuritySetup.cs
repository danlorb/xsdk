using xSdk.Extensions.Variable;
using xSdk.Extensions.Variable.Attributes;
using xSdk.Plugins.WebSecurity;

namespace xSdk.Plugins.WebSecurity
{
    public sealed class WebSecuritySetup : Setup, IWebSecuritySetup
    {
        [Variable(
            name: Definitions.Cors.Name,
            template: Definitions.Cors.Template,
            helpText: Definitions.Cors.HelpText,
            defaultValue: Definitions.Cors.DefaultValue,
            protect: true
        )]
        public bool IsCorsEnabled
        {
            get => ReadValue<bool>(Definitions.Cors.Name);
            set => SetValue(Definitions.Cors.Name, value);
        }

        [Variable(name: Definitions.Origins.Name, template: Definitions.Origins.Template, helpText: Definitions.Origins.HelpText, protect: true)]
        public string Origins
        {
            get => ReadValue<string>(Definitions.Origins.Name);
            set => SetValue(Definitions.Origins.Name, value);
        }

        public static class Definitions
        {
            public static class Cors
            {
                public const string Name = "cors";
                public const string Template = "--cors";
                public const string HelpText = "Enables cors for the web server";
                public const bool DefaultValue = false;
            }

            public static class Origins
            {
                public const string Name = "origins";
                public const string Template = "--origins <origins>";
                public const string HelpText = "Comma seperated list of origins";
            }
        }
    }
}
