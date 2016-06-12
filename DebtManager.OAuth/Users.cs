using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace DebtManager.OAuth
{
    static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "mail@filipeckberg.se",
                    Username = "mail@filipeckberg.se",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.Name, "Filip Eckberg")
                    }
                },
                new InMemoryUser
                {
                    Username = "alice",
                    Password = "secret",
                    Subject = "2"
                },
                new InMemoryUser
                {
                    Username = "adrian.moroi",
                    Subject = "adrian.moroi",
                    Password = "secret"
                },
                new InMemoryUser
                {
                    Username = "bogdan.dolhascu",
                    Subject = "bogdan.dolhascu",
                    Password = "secret"
                },
                new InMemoryUser
                {
                    Username = "ovidiu.petrache",
                    Subject = "ovidiu.petrache",
                    Password = "secret"
                },
                new InMemoryUser
                {
                    Username = "vadim.zabrian",
                    Subject = "vadim.zabrian",
                    Password = "secret"
                }
            };
        }
    }
}