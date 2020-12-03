using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab3_DAL_BLL_PL_ConsoleVersion_
{
    public class HotelServiceProvider
    {
        private static IServiceProvider serviceProvider;
        private HotelServiceProvider() { }
        public static IServiceProvider GetServiceProvider()
        {
            if (serviceProvider == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();

                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                IConfigurationRoot config = builder.Build();

                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

                var provider = new ServiceCollection()
                    .AddTransient<IUnitOfWork>(p => new UnitOfWork(optionsBuilder.Options))
                    .AddAutoMapper(p => p.AddProfile(new HotelServiceMapperProfile()))
                    .AddTransient<IHotelService>(p => new HotelService(p.GetRequiredService<IUnitOfWork>(), p.GetRequiredService<IMapper>()));                    
                serviceProvider = provider.BuildServiceProvider();                 
            }
            return serviceProvider;
        }
    }
}
