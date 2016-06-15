using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace DebtManager.OAuth
{
    static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read user data",
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("sub", true)
                    }
                }
            };
        }
    }
}