using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingAPIAlten.Core.Data;

namespace BookingAPIAlten.Domain
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservsRooms();
        Task<Reservation> GetReserveName(string name);
        Task<Reservation> GetReserveId(int id);
        Task<bool> InsertReserve(Reservation reservation);
        Task<bool> UpdateReserve(Reservation reservation);
        Task<bool> CancelReserve(int id);
    }
}
