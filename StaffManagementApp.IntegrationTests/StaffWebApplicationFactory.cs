using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StaffManagementApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagementApp.IntegrationTests
{
    internal class StaffWebApplicationFactory : WebApplicationFactory<Program>
    {
        private IMapper _mapper = A.Fake<IMapper>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                services.RemoveAll(typeof(IMapper));

                var connString = GetConnectionString();
                services.AddSqlServer<ApplicationDbContext>(connString);

                var dbContext = CreateDbContext(services);
                dbContext.Database.EnsureDeleted();
                dbContext.Database.Migrate();

                //Seed data
                SeedDataDbContext(dbContext);

                services.AddAutoMapper(mc =>
                {
                    mc.AddProfile(new StaffManagementApp.Infrastructure.AutoMapperConfigureProfiles());
                });

            });
        }

        private static string? GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connString = configuration.GetConnectionString("TestConnection");
            return connString;
        }

        private static ApplicationDbContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return dbContext;
        }

        private static void SeedDataDbContext(ApplicationDbContext dbContext)
        {
            var rand = new Random();
            if (dbContext.Staffs.Count() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    dbContext.Staffs.Add(
                    new Domain.Entities.Staff()
                    {
                        Id = 0,
                        StaffId = $"ID{i}",
                        FullName = $"NAME{i}",
                        Birthday = new DateTime(2000, 1, 1),
                        Gender = rand.Next(1, 2)
                    }
                    );
                    dbContext.SaveChanges();
                }

                //Seed data for advanced search
                dbContext.Staffs.Add(
                    new Domain.Entities.Staff()
                    {
                        Id = 0,
                        StaffId = $"ID10",
                        FullName = $"NAME10",
                        Birthday = DateTime.Now,
                        Gender = 1
                    }
                    );
                dbContext.SaveChanges();
            }
        }
        

        //public static IServiceCollection AddAutoMapper(this IServiceCollection services,
        //                                       IEnumerable<Assembly> assemblies)
        //{
        //    Type baseType = typeof(Profile);
        //    var profiles = assemblies.SelectMany(a => a.ExportedTypes).Where(baseType.IsAssignableFrom);

        //    // create a new config with all those profiles registered
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        foreach (Type profile in profiles)
        //        {
        //            cfg.AddProfile(profile);
        //        }
        //    });

        //    services.TryAddSingleton<IMapper>(sp => new Mapper(config, sp.GetService));

        //    return services;
        //}
    }
}
