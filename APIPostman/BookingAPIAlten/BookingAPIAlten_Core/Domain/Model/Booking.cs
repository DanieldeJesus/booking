using System;

namespace BookingAPIAlten_Core.Domain.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
    }
}
