using System;

namespace BookingAPIAlten.Domain
{
    public class Booking
    {
        private Booking reservationRoom;

        public int Id { get; set; }
        public Room Room { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }

        protected Booking() { }

        public Booking(int id, Room room, DateTime from, DateTime to)
        {
            Id = id;
            Room = room;
            BookedFrom = from;
            BookedTo = to;
        }
    }
}
