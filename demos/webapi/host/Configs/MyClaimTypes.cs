using xSdk.Security.Claims;

namespace xSdk.Demos.Configs
{
    internal static class MyClaimTypes
    {
        internal static class MyTableA
        {
            public static string Permission = ClaimCreator.CreateClaimType("mytablea", "permission");
        }
    }
}
