using System;
using System.IO;
using System.Linq;
using BLL.Services;
using BLL.DTO;
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

            MakeOrder(temp);
            //using(HotelService service = new HotelService(temp))
            //{
                

            //    ClientDTO client = new ClientDTO();
            //    Console.Write("Ваше имя: ");
            //    string input = Console.ReadLine();
            //    client.FirstName = input;
            //    Console.Write("Ваша фамилия: ");
            //    input = Console.ReadLine();
            //    client.LastName = input;
            //    Console.Write("Ваш номер телефона: ");
            //    input = Console.ReadLine();
            //    client.PhoneNumber = input;

            //    var orders = service.FindActiveOrdersClient(client);
            //    if (!orders.Any())
            //        Console.WriteLine("Вы ничего не заказывали");
            //    foreach(var order in orders)
            //        Console.WriteLine(order.HotelRoomId + " " + order.PaymentState.ToString());

            //    var updateOrder = orders.ToList()[0];
            //    updateOrder.PaymentState = PaymentStateEnumDTO.Paid;
            //    service.UpdateActiveOrder(updateOrder);
            //}
        }
        public static void MakeOrder(IUnitOfWork uof)
        {
            using (HotelService service = new HotelService(uof))
            {
                int inputInt;
                Console.WriteLine("Такс заполните форму для поиска нужной комнаты");
                HotelRoomSeachFilterDTO roomFilter = new HotelRoomSeachFilterDTO();

                Console.WriteLine("Какого типа комфорта вы желаете?\n1 - Standart, 2 - Suite, 3 - De_Luxe, 4 - Duplex, 5 - Family_Room, 6 - Honeymoon_Room, 0 - Not important");
                while(true)
                {
                    Console.Write("Введите номер: ");
                    if (int.TryParse(Console.ReadLine(), out inputInt))
                        if (inputInt >= 0 && inputInt <= 6)
                            break;
                    Console.WriteLine("Try again");
                    continue;
                }
                roomFilter.TypeComfort = (TypeComfortEnumDTO)inputInt;

                Console.WriteLine("Какого типа размера вы желаете?\n1 - SGL, 2 - DBL, 3- DBL_TWN, 4 - TRPL, 5 - DBL_EXB, 6 - TRPL_EXB, 0 - Not important");
                while (true)
                {
                    Console.Write("Введите номер: ");
                    if (int.TryParse(Console.ReadLine(), out inputInt))
                        if (inputInt >= 0 && inputInt <= 6)
                            break;
                    Console.WriteLine("Try again");
                    continue;
                }
                roomFilter.TypeSize = (TypeSizeEnumDTO)inputInt;

                Console.WriteLine("На которую дату хотите заселится?");
                Console.Write("Год: ");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Месяц: ");
                int month = int.Parse(Console.ReadLine());
                Console.Write("День: ");
                int day = int.Parse(Console.ReadLine());
                roomFilter.CheckInDate = new DateTime(year, month, day);

                var rooms = service.SearchFreeRooms(roomFilter);
                if (!rooms.Any())
                    Console.WriteLine("К сожалению свободных подобных комнат нет на данную дату");
                else
                {
                    Console.WriteLine("Найденные комнаты:");
                    foreach (var t in rooms)
                        Console.WriteLine("Номер: " + t.Number + " Цена за день: " + t.PricePerDay + " Комфорт: "  + t.TypeComfort.ToString() + " Размер: " + t.TypeSize.ToString() + " Дата вьезда: " + t.CheckInDate + " Макс дата выезда: " + t.MaxCheckOutDate);              
                    
                    FreeHotelRoomDTO room;
                    string inputString;
                    while (true)
                    {
                        Console.Write("Какой номер предпочитаете: ");
                        inputString = Console.ReadLine();
                        room = rooms.FirstOrDefault(p => p.Number == inputString);
                        if (!(room is null))
                            break;
                        Console.WriteLine("У Вас проблемы с цифрами)))  Try Again");              
                        continue;
                    }

                    ActiveOrderDTO order = new ActiveOrderDTO();
                    order.HotelRoomId = room.HotelRoomId;
                    order.ChecknInDate = room.CheckInDate;
                    Console.Write("На которое количество дней: ");
                    int days = int.Parse(Console.ReadLine());
                    order.CheckOutDate = room.CheckInDate.AddDays(days);
                    order.DateRegistration = DateTime.Now;
                    
                    ClientDTO client = new ClientDTO();
                    Console.Write("Ваше имя: ");
                    inputString = Console.ReadLine();
                    client.FirstName = inputString;
                    Console.Write("Ваша фамилия: ");
                    inputString = Console.ReadLine();
                    client.LastName = inputString;
                    Console.Write("Ваш номер телефона: ");
                    inputString = Console.ReadLine();
                    client.PhoneNumber = inputString;
                    order.Client = client;

                    Console.Write("Бронь или оплата: 1 - Paid, 2 - Booked");
                    while (true)
                    {
                        Console.Write("Введите номер: ");
                        if (int.TryParse(Console.ReadLine(), out inputInt))
                            if (inputInt >= 1 && inputInt <= 2)
                                break;
                        Console.WriteLine("Try again");
                        continue;
                    }
                    order.PaymentState = (PaymentStateEnumDTO)inputInt;

                    service.AddActiveOrder(order);
                }
            }

        }
    }
}
