using BookingAPIAlten_Core.Domain.Enum;
using System;

namespace BookingAPIAlten_Core.Domain.Model
{
    public class Reservation : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Booking ReservationRoom { get; set; }

        //The stay can’t be longer than 3 days
        public bool LongerThan3Days()
        {
            TimeSpan date = ReservationRoom.BookedTo - ReservationRoom.BookedFrom;

            return date.Days > 3;
        }

        //Can’t be reserved more than 30 days in advance.
        public bool MoreThan30DaysAdvance()
        {
            TimeSpan date = ReservationRoom.BookedFrom - DateTime.UtcNow;

            return date.Days > 30;
        }
    }
}
