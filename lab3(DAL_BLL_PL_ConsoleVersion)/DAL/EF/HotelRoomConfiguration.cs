using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF
{
    class HotelRoomConfiguration: IEntityTypeConfiguration<HotelRoom>
    {
        public void Configure(EntityTypeBuilder<HotelRoom> builder)
        {
            builder.HasData(
                new HotelRoom[]
                {
                    new HotelRoom {HotelRoomId = 1, Number = "10", Price = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelRoomId = 2, Number = "11", Price = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelRoomId = 3, Number = "12", Price = 200, TypeComfortId = 2, TypeSizeId = 1},
                    new HotelRoom {HotelRoomId = 4, Number = "13", Price = 200, TypeComfortId = 2, TypeSizeId = 2},
                    new HotelRoom {HotelRoomId = 5, Number = "20", Price = 250, TypeComfortId = 2, TypeSizeId = 3},
                    new HotelRoom {HotelRoomId = 6, Number = "21", Price = 250, TypeComfortId = 2, TypeSizeId = 5},
                    new HotelRoom {HotelRoomId = 7, Number = "22", Price = 300, TypeComfortId = 3, TypeSizeId = 1},
                    new HotelRoom {HotelRoomId = 8, Number = "30", Price = 400, TypeComfortId = 3, TypeSizeId = 2},
                    new HotelRoom {HotelRoomId = 9, Number = "31", Price = 400, TypeComfortId = 4, TypeSizeId = 3},
                    new HotelRoom {HotelRoomId = 10, Number = "40", Price = 600, TypeComfortId = 5, TypeSizeId = 6},
                    new HotelRoom {HotelRoomId = 11, Number = "50", Price = 800, TypeComfortId = 6, TypeSizeId = 2}
                });
        }
    }
}
