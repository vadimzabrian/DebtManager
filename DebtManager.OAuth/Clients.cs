using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace DebtManager.OAuth
{
    static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "socialnetwork",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName="SocialNetwork",
                    Flow = Flows.ResourceOwner,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        "read"
                    },
                    Enabled = true
                },
                
                new Client
                {
                    ClientId = "socialnetwork_implicit",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName="SocialNetwork",
                    Flow = Flows.Implicit,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:22250/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:22250/"
                    },
                    Enabled = true
                }
            };
        }
    }
}