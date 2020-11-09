using System;
using System.IO;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace lab3_DAL_BLL_PL_ConsoleVersion_
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

            IUnitOfWork temp = new UnitOfWork(optionsBuilder.Options);
            var temp2 = temp.HotelRooms.ReadAll();
            foreach (var t in temp2)
            {
                Console.WriteLine(t.Number + " " + t.Price + " " + t.TypeComfort.Comfort + " " + t.TypeSize.Size);
            }
            var temp3 = temp.HotelRooms.Read(1);
            Console.WriteLine(temp3.Number + " " + temp3.Price + " " + temp3.TypeComfort.Comfort + " " + temp3.TypeSize.Size);
        }
    }
}
