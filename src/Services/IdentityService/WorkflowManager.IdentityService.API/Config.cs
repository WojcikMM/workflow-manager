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
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "My API #1"),
                new ApiResource("Processes_Service", "Access to Processes Service API"),
                new ApiResource("Statuses_Service", "Access to Statuses Service API"),
                new ApiResource("Operations_Service", "Access to Operations Service API"),
                new ApiResource("Notyfications_Service", "Access to Notyfications Service API"),
            };


        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser()
                {
                    Username = "user1",
                    Password = "pwd1",
                    IsActive = true,
                    Claims = new List<Claim>()
                    {
                        new Claim(JwtClaimTypes.Name, "Michael Test"),
                        new Claim(JwtClaimTypes.GivenName, "Michael"),
                        new Claim(JwtClaimTypes.FamilyName, "Test"),
                        new Claim(JwtClaimTypes.Email, "michael.test@identity.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://great.developers.com/michael.test"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Developer Street', 'locality': 'Katowice', 'postal_code': '41-100', 'country': 'Poland' }",IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }

                }
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },
                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "openid", "profile", "api1" }
                },
                new Client
                {
                    ClientId = "swagger",
                    ClientName = "swagger",
                    ClientUri = "http://identityserver.io",
                    Enabled = true,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    RedirectUris =
                    {
                        "http://localhost:8000/index.html",
                        "http://localhost:8000/oauth2-redirect.html"
                    },
                    PostLogoutRedirectUris = { "http://localhost:8000/index.html" },
                    AllowedCorsOrigins = { "http://localhost:8000" },

                    AllowedScopes = { "openid", "profile", "api1" }

                },
                 new Client
                {
                    ClientId = "swagger2",
                    ClientName = "swagger2",
                    ClientUri = "http://identityserver.io",
                    Enabled = true,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    RedirectUris =
                    {
                        "http://localhost:9000/oauth2-redirect.html",
                        "http://localhost:8001/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = { "http://localhost:9000/index.html" },
                    AllowedCorsOrigins = { "http://localhost:9000" },
                    AllowedScopes = { 
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         "api1",
                         "Processes_Service",
                         "Statuses_Service",
                         "Operations_Service",
                         "Notyfications_Service"
                     },
                    RequireConsent = false

                }
            };
    }
}