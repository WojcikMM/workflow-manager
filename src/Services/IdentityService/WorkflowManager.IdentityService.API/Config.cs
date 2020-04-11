// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

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


        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("Processes_Service",
                    "Access to Processes Service API",
                    new List<string>{
                        ClaimTypes.Role
                    }),

                new ApiResource("Statuses_Service",
                    "Access to Statuses Service API",
                    new List<string>{
                        ClaimTypes.Role
                    }),

                new ApiResource("Operations_Service",
                    "Access to Operations Service API",
                    new List<string>{
                        ClaimTypes.Role
                    }),

                new ApiResource("Notifications_Service",
                    "Access to Notifications Service API",
                    new List<string>{
                        ClaimTypes.Role
                    }),

                new ApiResource("Identity_Service",
                    "Access to Identity Service API",
                    new List<string>{
                        ClaimTypes.Role
                    }),
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Code Client",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 330,
                    IdentityTokenLifetime = 60,

                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequirePkce = true,

                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:4200"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                         "http://localhost:4200"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:4200"
                    },
                    AllowedScopes = new List<string>
                    {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         "role",
                         "Processes_Service",
                         "Statuses_Service",
                         "Operations_Service",
                         "Notifications_Service",
                         "Identity_Service"
                    },
                    RequireConsent = false

                },
                 new Client
                {
                    ClientId = "swagger",
                    ClientName = "Swagger Client",
                    Enabled = true,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    RedirectUris =
                    {
                        "https://workflow-manager-identity-service-api-dev.azurewebsites.net/oauth2-redirect.html",
                        "http://localhost:5000/oauth2-redirect.html",
                        "http://localhost:8000/oauth2-redirect.html",
                        "http://localhost:8000/swagger/oauth2-redirect.html",
                        "http://localhost:8001/oauth2-redirect.html",
                        "http://localhost:8002/oauth2-redirect.html",
                        "http://localhost:8003/oauth2-redirect.html"
                    },
                    PostLogoutRedirectUris = {
                        "https://workflow-manager-identity-service-api-dev.azurewebsites.net/index.html",
                         "http://localhost:5000/index.html",
                         "http://localhost:8000/index.html",
                         "http://localhost:8001/index.html",
                         "http://localhost:8002/index.html",
                         "http://localhost:8003/index.html"
                     },
                    AllowedCorsOrigins = {
                         "https://workflow-manager-identity-service-api-dev.azurewebsites.net",
                         "http://localhost:5000",
                         "http://localhost:8000",
                         "http://localhost:8001",
                         "http://localhost:8002",
                         "http://localhost:8003",
                     },
                    AllowedScopes = {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         "api1",
                         "Processes_Service",
                         "Statuses_Service",
                         "Operations_Service",
                         "Notifications_Service",
                         "Identity_Service"
                     },
                    RequireConsent = false
                }
            };
    }
}