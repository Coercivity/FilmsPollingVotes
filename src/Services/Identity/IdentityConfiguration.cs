using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityService
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope("Users", "IdentityAPI")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>()
            { 
                new ApiResource("Users", "IdentityAPI", new []{JwtClaimTypes.Name})
                {
                    Scopes = { "Users" }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>()
            {
                new Client
                {
                    ClientId = "first-client",
                    ClientName = "FirstClient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = 
                    {
                        "http://.../wip"
                    },
                    AllowedCorsOrigins = 
                    {
                        "http://.../wip"
                    },
                    PostLogoutRedirectUris = 
                    {
                        "http://.../wip"
                    },
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    AllowAccessTokensViaBrowser = true

                }
            };



    }
}
