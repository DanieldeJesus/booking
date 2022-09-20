using BookingAPIAlten_Core.Data.Utils;
using BookingAPIAlten_Core.Domain;
using BookingAPIAlten_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPIAlten_Core.Services
{
    public class ReservationAppService : IReservationAppService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationAppService(IReservationRepository reservationRepository,
                                     IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public Task<IEnumerable<Room>> GetAvailableRoom(DateTime? from, DateTime? to)
        {
            return _roomRepository.GetAvailableRoom(from, to);
        }

        public Task<bool> InsertReserve(Reservation reservation)
        {
            if(!_roomRepository.GetAvailableRoom(reservation.ReservationRoom.BookedFrom, reservation.ReservationRoom.BookedTo).Result.Any())
                throw new DomainException("Room not available, make a new appointment.");

            if (reservation.LongerThan3Days() || reservation.MoreThan30DaysAdvance())
                throw new DomainException("Stay longer than 3 days or booking more than 30 days in advance.");

            return _reservationRepository.InsertReserve(reservation);
        }

        public Task<bool> UpdateReserve(Reservation reservation)
        {
            var _reservation = _reservationRepository.GetReserveId(reservation.Id).Result;

            if(_reservation == null)
                throw new DomainException("Reservation not found.");

            if (_reservation.ReservationRoom.BookedFrom != reservation.ReservationRoom.BookedFrom)
                if (reservation.MoreThan30DaysAdvance())
                    throw new DomainException("Booking more than 30 days in advance.");

            if (_reservation.ReservationRoom.BookedFrom != reservation.ReservationRoom.BookedFrom ||
                _reservation.ReservationRoom.BookedTo != reservation.ReservationRoom.BookedTo)
                if (reservation.LongerThan3Days())
                    throw new DomainException("Stay longer than 3 days.");

            return _reservationRepository.UpdateReserve(reservation);
        }

        public Task<bool> CancelReserve(int id)
        {
            return _reservationRepository.CancelReserve(id);
        }

        public void Dispose()
        {
            _reservationRepository?.Dispose();
            _roomRepository?.Dispose();
        }
    }
}
