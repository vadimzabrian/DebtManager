﻿using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(DebtManager.OAuth.Startup))]

namespace DebtManager.OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),

                SigningCertificate = new X509Certificate2(ConfigurationManager.AppSettings["CertificatePath"], ConfigurationManager.AppSettings["CertificatePassword"]),

                RequireSsl = false
            };

            app.UseIdentityServer(options);
        }
    }
}