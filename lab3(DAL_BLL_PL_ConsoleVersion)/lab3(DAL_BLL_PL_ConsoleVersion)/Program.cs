using System;
using System.IO;
using System.Linq;
using BLL;
using BLL.Models;
using DAL.EF;
using DAL.Entities;
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

            using(HotelService service = new HotelService(temp))
            {
                Console.WriteLine("Такс впишите какую комнату Вы хотите найти");
                HotelRoomSeachFilter roomFilter = new HotelRoomSeachFilter();
                Console.WriteLine("Какого типа комфорта вы желаете? (Standart, Suite, De_Luxe, Duplex, Family_Room, Honeymoon_Room");
                string input = Console.ReadLine();
                roomFilter.TypeComfort = Enum.Parse<TypeComfortEnumBLL>(input);
                Console.WriteLine("Какого типа размера вы желаете? (SGL, DBL, DBL_TWN, TRPL, DBL_EXB, TRPL_EXB");
                input = Console.ReadLine();
                roomFilter.TypeSize = Enum.Parse<TypeSizeEnumBLL>(input);
                Console.WriteLine("Какая дата?");
                Console.Write("Год: ");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Месяц: ");
                int month = int.Parse(Console.ReadLine());
                Console.Write("День: ");
                int day = int.Parse(Console.ReadLine());
                roomFilter.CheckInDate = new DateTime(year, month, day);

                var rooms = service.SearchFreeRooms(roomFilter);
                if (rooms == null)
                    Console.WriteLine("К сожалению свободных подобных комнат нет на данную дату");
                else
                {
                    Console.WriteLine("Найденные комнаты");
                    foreach (var t in rooms)
                        Console.WriteLine("Номер: " + t.Number + "  Цена за день:" + t.PricePerDay + " Дата вьезда:" + t.CheckInDate + " Макс дата выезда" + t.MaxCheckOutDate ?? "не занятая ближайшее время");

                    Console.Write("Какой номер предпочитаете: ");
                    string num = Console.ReadLine();
                    HotelRoomBL room = rooms.First(p => p.Number == num);

                    ActiveOrderBL order = new ActiveOrderBL();
                    order.HotelRoom = room;
                    order.ChecknInDate = room.CheckInDate;
                    Console.Write("На которое количество дней: ");
                    int days = int.Parse(Console.ReadLine());
                    order.CheckOutDate = room.CheckInDate.AddDays(days);
                    order.DateRegistration = DateTime.Now;

                    ClientBL client = new ClientBL();
                    Console.Write("Ваше имя: ");
                    input = Console.ReadLine();
                    client.FirstName = input;
                    Console.Write("Ваша фамилия: ");
                    input = Console.ReadLine();
                    client.LastName = input;
                    Console.Write("Ваш номер телефона: ");
                    input = Console.ReadLine();
                    client.PhoneNumber = input;
                    order.Client = client;

                    Console.Write("Бронь или оплата (Paid, Booked): ");
                    input = Console.ReadLine();
                    order.PaymentState = Enum.Parse<PaymentStateEnumBL>(input);

                    service.AddActiveOrder(order);
                }               
            }

            //Client client = new Client { FirstName = "Serhii", LastName = "Yanchuk", PhoneNumber = "06342557585" };
            //ActiveOrder order = new ActiveOrder { Client = client, HotelRoomId = 1, Discount = 0, PaymentState = PaymentStateEnum.Booked, ChecknInDate = new DateTime(2020, 1, 20), CheckOutDate = new DateTime(2020, 1, 25), DateRegistration = new DateTime(2020, 1, 19) };
            //service.AddActiveOrder(order);

            //HotelRoomSeachFilter roomFilter = new HotelRoomSeachFilter();
            //roomFilter.TypeSize = TypeSizeEnumBLL.SGL;
            //roomFilter.TypeComfort = TypeComfortEnumBLL.Standart;
            //roomFilter.CheckInDate = new DateTime(2020, 1, 1);

            //var res = service.SearchFreeRooms(roomFilter);
            //foreach (var t in res)
            //{
            //    Console.WriteLine(t.Number + " " + t.PricePerDay + " " + t.CheckInDate + " - " + t.MaxCheckOutDate);
            //}
        }
    }
}
