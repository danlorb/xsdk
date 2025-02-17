using xSdk.Extensions.Variable;
using xSdk.Extensions.Variable.Attributes;

namespace xSdk.Extensions.DataProtection
{
    [VariablePrefix("encryption")]
    public sealed class DataProtectionSetup : Setup, IDataProtectionSetup
    {
        [Variable(
            name: Definitions.ApplicationDiscriminator.Name,
            template: Definitions.ApplicationDiscriminator.Template,
            helpText: Definitions.ApplicationDiscriminator.HelpText
        )]
        public string ApplicationDiscriminator
        {
            get => this.ReadValue<string>(Definitions.ApplicationDiscriminator.Name);
            set => this.SetValue(Definitions.ApplicationDiscriminator.Name, value);
        }

        [Variable(
            name: Definitions.ApplicationName.Name,
            template: Definitions.ApplicationName.Template,
            helpText: Definitions.ApplicationName.HelpText
        )]
        public string ApplicationName
        {
            get => this.ReadValue<string>(Definitions.ApplicationName.Name);
            set => this.SetValue(Definitions.ApplicationName.Name, value);
        }

        [Variable(
            name: Definitions.KeyLifetime.Name,
            template: Definitions.KeyLifetime.Template,
            helpText: Definitions.KeyLifetime.HelpText
        )]
        public string KeyLifetime
        {
            get => this.ReadValue<string>(Definitions.KeyLifetime.Name);
            set => this.SetValue(Definitions.KeyLifetime.Name, value);
        }

        public static class Definitions
        {
            public static class ApplicationDiscriminator
            {
                public const string Name = "discriminator";
                public const string Template = "--discriminator <discriminator>";
                public const string HelpText =
                    "An identifier that uniquely discriminates this application from all other applications on the machine. The discriminator value is implicitly included in all protected payloads generated by the data protection system to isolate multiple logical applications that all happen to be using the same key material. If two different applications need to share protected payloads, they should ensure that this property is set to the same value across both applications.";
            }

            public static class ApplicationName
            {
                public const string Name = "name";
                public const string Template = "--name <name>";
                public const string HelpText =
                    "Sets the unique name of this application within the data protection system.";
            }

            public static class KeyLifetime
            {
                public const string Name = "lifetime";
                public const string Template = "--lifetime <lifetime>";
                public const string HelpText =
                    "Sets the default lifetime of keys created by the data protection system.";
            }
        }
    }
}
