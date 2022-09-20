using System;
using System.Threading.Tasks;
using BookingAPIAlten_Core.Data;
using BookingAPIAlten_Core.Domain.Model;

namespace BookingAPIAlten_Core.Domain
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<Reservation> GetReserveName(string name);
        Task<Reservation> GetReserveId(int id);
        Task<bool> InsertReserve(Reservation reservation);
        Task<bool> UpdateReserve(Reservation reservation);
        Task<bool> CancelReserve(int id);
    }
}
