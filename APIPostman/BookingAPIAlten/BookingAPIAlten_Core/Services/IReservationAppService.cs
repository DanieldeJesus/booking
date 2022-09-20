using BookingAPIAlten_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingAPIAlten_Core.Services
{
    public interface IReservationAppService : IDisposable
    {
        Task<IEnumerable<Room>> GetAvailableRoom(DateTime? from, DateTime? to);
        Task<bool> InsertReserve(Reservation reservation);
        Task<bool> UpdateReserve(Reservation reservation);
        Task<bool> CancelReserve(int id);
    }
}
