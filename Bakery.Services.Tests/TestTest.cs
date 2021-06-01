using System;
using Bakery.Core;
using System.Linq;
using Bakery.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Bakery.Services.Tests
{
    public class TestHere
    {
        protected DbContextOptions<AppDbContext> ContextOptions { get; }
        
        protected TestHere(DbContextOptions<AppDbContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
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