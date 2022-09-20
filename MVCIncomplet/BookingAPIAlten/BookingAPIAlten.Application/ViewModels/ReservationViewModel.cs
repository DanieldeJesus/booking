using BookingAPIAlten.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingAPIAlten.Application.ViewModels
{
    public class ReservationViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public BookingViewModel Booking { get; set; }

        protected ReservationViewModel() { }

        public ReservationViewModel(Reservation reservations)
        {
            Id = reservations.Id;
            Name = reservations.Name;
            Email = reservations.Email;
            Phone = reservations.Phone;
            Booking = new BookingViewModel(reservations.ReservationRoom);
        }

        internal static IEnumerable<ReservationViewModel> FromTo(IEnumerable<Reservation> reservations)
        {
            var result = new List<ReservationViewModel>();

            foreach (var item in reservations)
            {
                result.Add(new ReservationViewModel(item));
            }

            return result;
        }
    }
}
