// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("DagAir.Facilities", "Facilities")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "WebAdminApp",
                    
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    
                    AllowedScopes =
                    {
                        "DagAir.Facilities"
                    }
                },
                new Client
                {
                    ClientId = "TestMvcApp",

                    RedirectUris           = { "https://localhost:5011/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5011/signout-callback-oidc" },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    
                    RequirePkce = true,
                    
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
    }
}