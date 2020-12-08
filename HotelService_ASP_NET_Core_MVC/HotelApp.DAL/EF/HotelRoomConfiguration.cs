using HotelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.DAL.EF
{
    class HotelRoomConfiguration: IEntityTypeConfiguration<HotelRoom>
    {
        public void Configure(EntityTypeBuilder<HotelRoom> builder)
        {
            builder.HasMany(t => t.Clients).WithMany(t => t.HotelRooms)
                .UsingEntity<ActiveOrder>(
                    t => t.HasOne(p => p.Client).WithMany(p => p.ActiveOrders).HasForeignKey(p => p.ClientId),
                    t => t.HasOne(p => p.HotelRoom).WithMany(p => p.ActiveOrders).HasForeignKey(p => p.HotelRoomId),
                    t =>
                    {
                        t.Property(p => p.DateRegistration).HasDefaultValueSql("CURRENT_TIMESTAMP");                        
                    });

            builder.HasData(
                new HotelRoom[]
                {
                    new HotelRoom {HotelRoomId = 1, Number = "10", PricePerDay = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelRoomId = 2, Number = "11", PricePerDay = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelRoomId = 3, Number = "12", PricePerDay = 200, TypeComfortId = 2, TypeSizeId = 1},
                    new HotelRoom {HotelRoomId = 4, Number = "13", PricePerDay = 200, TypeComfortId = 2, TypeSizeId = 2},
                    new HotelRoom {HotelRoomId = 5, Number = "20", PricePerDay = 250, TypeComfortId = 2, TypeSizeId = 3},
                    new HotelRoom {HotelRoomId = 6, Number = "21", PricePerDay = 250, TypeComfortId = 2, TypeSizeId = 5},
                    new HotelRoom {HotelRoomId = 7, Number = "22", PricePerDay = 300, TypeComfortId = 3, TypeSizeId = 1},
                    new HotelRoom {HotelRoomId = 8, Number = "30", PricePerDay = 400, TypeComfortId = 3, TypeSizeId = 2},
                    new HotelRoom {HotelRoomId = 9, Number = "31", PricePerDay = 400, TypeComfortId = 4, TypeSizeId = 3},
                    new HotelRoom {HotelRoomId = 10, Number = "40", PricePerDay = 600, TypeComfortId = 5, TypeSizeId = 6},
                    new HotelRoom {HotelRoomId = 11, Number = "50", PricePerDay = 800, TypeComfortId = 6, TypeSizeId = 2}
                });
        }
    }
}
