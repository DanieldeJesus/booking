using BookingAPIAlten.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAPIAlten.Application.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public RoomViewModel Room { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }

        protected BookingViewModel() { }

        public BookingViewModel(Booking reservationRoom)
        {
            Id = reservationRoom.Id;
            Room = new RoomViewModel();
            BookedFrom = reservationRoom.BookedFrom;
            BookedTo = reservationRoom.BookedTo;
        }
    }
}
