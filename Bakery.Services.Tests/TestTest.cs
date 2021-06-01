using System;
using Bakery.Core;
using System.Linq;
using Bakery.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bakery.Services.Tests
{
    public class TestHere
    {
        protected DbContextOptions<AppDbContext> ContextOptions { get; }
        protected IServiceProvider ServiceProvider { get; private set; }
        protected TestHere(DbContextOptions<AppDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            ConfigureServices();
            Seed();
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(Mappers));
            ServiceProvider = services.BuildServiceProvider();
        }

        protected virtual void Seed()
        {
            using (var context = new AppDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}