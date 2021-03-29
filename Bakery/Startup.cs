using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Bakery.Core;
using Bakery.Middlewares;
using Bakery.Services.Application;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Domain.Address;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace Bakery
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            var connectionString = Configuration.GetConnectionString("BakeryDb");
            ServerVersion version = ServerVersion.AutoDetect(connectionString);
            services.AddDbContextPool<AppDbContext>(    
                    dbContextOptions => dbContextOptions
                        .UseMySql(
                            connectionString,
                            mySqlOptions => mySqlOptions
                                .ServerVersion(version)
                                .CharSetBehavior(CharSetBehavior.NeverAppend)
                                .MigrationsAssembly("Bakery"))
                        .UseLoggerFactory(
                            LoggerFactory.Create(
                                logging => logging
                                    .AddConsole()
                                    .AddFilter(level => level >= LogLevel.Information)))
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                );

            services.AddTransient<IAddressRepository, AddressRepository>(provider =>
                new AddressRepository(connectionString));
            
            services.AddAutoMapper(typeof(Mappers));
            
            services.RegisterUserServices();
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(Bootstrapper).Assembly);

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerValidator>());
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseMiddleware<LoggingMiddleware>();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}