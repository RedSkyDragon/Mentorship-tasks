using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServer.Models;
using IdentityServer.Utils;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Data
{
    /// <summary>
    /// Initialize methods for database.
    /// </summary>
    public static class InitializeDB
    {
        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            InitializeConfiguration(app);
            InitializeUsers(app);
        }

        /// <summary>
        /// Initializes the users.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="Exception">
        /// </exception>
        private static void InitializeUsers(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();

                var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var alex = userMgr.FindByNameAsync("Alex").Result;
                if (alex == null)
                {
                    alex = new ApplicationUser
                    {
                        UserName = "Alex"
                    };
                    var result = userMgr.CreateAsync(alex, "Password123!").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alex, new[]{
                            new Claim(JwtClaimTypes.Id, SequentialGuidUtils.CreateGuid().ToString()),
                            new Claim(JwtClaimTypes.Name, "Alex"),
                            new Claim(JwtClaimTypes.GivenName, "Alexandra"),
                            new Claim(JwtClaimTypes.FamilyName, "Klevtsova")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                var jacob = userMgr.FindByNameAsync("Jacob").Result;
                if (jacob == null)
                {
                    jacob = new ApplicationUser
                    {
                        UserName = "Jacob"
                    };
                    var result = userMgr.CreateAsync(jacob, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(jacob, new[]{
                        new Claim(JwtClaimTypes.Id, SequentialGuidUtils.CreateGuid().ToString()),
                        new Claim(JwtClaimTypes.Name, "Jacob Frye"),
                        new Claim(JwtClaimTypes.GivenName, "Jacob"),
                        new Claim(JwtClaimTypes.FamilyName, "Frye")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                var max = userMgr.FindByNameAsync("Maxwell").Result;
                if (max == null)
                {
                    max = new ApplicationUser
                    {
                        UserName = "Maxwell"
                    };
                    var result = userMgr.CreateAsync(max, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(max, new[]{
                        new Claim(JwtClaimTypes.Id, SequentialGuidUtils.CreateGuid().ToString()),
                        new Claim(JwtClaimTypes.Name, "Maxwell Roth"),
                        new Claim(JwtClaimTypes.GivenName, "Maxwell"),
                        new Claim(JwtClaimTypes.FamilyName, "Roth")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the configuration.
        /// </summary>
        /// <param name="app">The application.</param>
        private static void InitializeConfiguration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
