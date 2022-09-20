using BookingAPIAlten.Domain;
using BookingAPIAlten.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingAPIAlten.Application.ViewModels
{
    public class RoomViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Beds { get; set; }
        public StatusRoomEnum Status { get; set; }

        public RoomViewModel() { }

        public RoomViewModel(Room room)
        {
            Id = room.Id;
            Name = room.Name;
            Beds = room.Beds;
            Status = room.Status;
        }

        internal static IEnumerable<RoomViewModel> FromTo(IEnumerable<Room> rooms)
        {
            var result = new List<RoomViewModel>();

            foreach (var item in rooms)
            {
                result.Add(new RoomViewModel(item));
            }

            return result;
        }
    }
}
