using System.Security.Claims;
using xSdk.Hosting;

namespace xSdk.Security.Claims
{
    public class ClaimCreator
    {
        public static string CreateClaimType(string claim) => CreateClaimType(null, null, claim);

        public static string CreateClaimType(string context, string claim) =>
            CreateClaimType(null, context, claim);

        public static string CreateClaimType(string url, string context, string claim)
        {
            if (string.IsNullOrEmpty(url))
                url = $"https://{SlimHost.Instance.AppCompany}.de";

            if (!url.StartsWith("http"))
                url = $"https://{url}";

            if (string.IsNullOrEmpty(context))
                context = "vap";

            return $"{url}/{context}/claims/{claim}";
        }

        public static Claim CreateClaim(string claimType, string value) =>
            CreateClaim(claimType, value, null, null, null);

        public static Claim CreateClaim(string claimType, string value, string claimValueType) =>
            CreateClaim(claimType, value, claimValueType, null, null);

        public static Claim CreateClaim(
            string claimType,
            string value,
            string claimValueType,
            string issuer
        ) => CreateClaim(claimType, value, claimValueType, issuer, null);

        public static Claim CreateClaim(
            string claimType,
            string value,
            string claimValueType,
            string issuer,
            string originalIssuer
        )
        {
            Claim claim = default;

            if (
                !string.IsNullOrEmpty(value)
                && !string.IsNullOrEmpty(claimValueType)
                && !string.IsNullOrEmpty(issuer)
                && !string.IsNullOrEmpty(originalIssuer)
            )
                claim = new Claim(claimType, value, claimValueType, issuer, originalIssuer);
            if (
                !string.IsNullOrEmpty(value)
                && !string.IsNullOrEmpty(claimValueType)
                && !string.IsNullOrEmpty(issuer)
            )
                claim = new Claim(claimType, value, claimValueType, issuer);
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(claimValueType))
                claim = new Claim(claimType, value, claimValueType);
            if (!string.IsNullOrEmpty(value))
                claim = new Claim(claimType, value);

            return claim;
        }
    }
}
