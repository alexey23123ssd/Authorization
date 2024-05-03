using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Authorization.IndentityServer
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes =
                    {
                        "OrdersAPI",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource("SwaggerAPI");
            yield return new ApiResource("OrdersAPI");
        }      
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
        }
            

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            yield return new ApiScope("SwaggerAPI", "SwaggerAPI");
            yield return new ApiScope("blazor", "Blazor WebAssembly");
            yield return new ApiScope("OrdersAPI", "Orders API");
        }
    }
}

