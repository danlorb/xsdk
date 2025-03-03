using xSdk.Extensions.Variable;

namespace xSdk.Data
{
    public sealed class VaultDatabaseSetup : DatabaseSetup
    {
        public VaultDatabaseSetup()
        {
            AuthMethod = VaultAuthenticationMethod.None;
        }

        internal VaultAuthenticationMethod AuthMethod { get; set; }

        public string Host { get; set; }

        public string MountPoint { get; set; }

        public AppRoleAuth AppRoleAuth { get; set; }

        public JwtAuth JwtAuth { get; set; }

        public LdapAuth LdapAuth { get; set; }

        public OidcAuth OidcAuth { get; set; }

        public TokenAuth TokenAuth { get; set; }

        public UsernamePasswordAuth UsernamePasswordAuth { get; set; }

        protected override void ValidateSetup()
        {
            if (AuthMethod == VaultAuthenticationMethod.None && AppRoleAuth != null)
                AuthMethod = VaultAuthenticationMethod.AppRole;

            if (AuthMethod == VaultAuthenticationMethod.None && JwtAuth != null)
                AuthMethod = VaultAuthenticationMethod.Jwt;

            if (AuthMethod == VaultAuthenticationMethod.None && LdapAuth != null)
                AuthMethod = VaultAuthenticationMethod.Ldap;

            if (AuthMethod == VaultAuthenticationMethod.None && OidcAuth != null)
                AuthMethod = VaultAuthenticationMethod.Oidc;

            if (AuthMethod == VaultAuthenticationMethod.None && UsernamePasswordAuth != null)
                AuthMethod = VaultAuthenticationMethod.UsernamePassword;

            if (AuthMethod == VaultAuthenticationMethod.None && TokenAuth != null)
                AuthMethod = VaultAuthenticationMethod.Token;

            this.ValidateMember(x => x.AuthMethod == VaultAuthenticationMethod.None);
            this.ValidateMember(x => string.IsNullOrEmpty(x.Host));
            this.ValidateMember(x => AuthMethod != VaultAuthenticationMethod.Token && string.IsNullOrEmpty(x.MountPoint));            
        }
    }
}
