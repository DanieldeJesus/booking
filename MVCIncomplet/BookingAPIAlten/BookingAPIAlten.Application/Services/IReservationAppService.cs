using BookingAPIAlten.Application.ViewModels;
using BookingAPIAlten.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingAPIAlten.Application.Services
{
    public interface IReservationAppService : IDisposable
    {
        Task<IEnumerable<ReservationViewModel>> GetReservsRooms();
        Task<IEnumerable<RoomViewModel>> GetAvailableRoom(DateTime? from, DateTime? to);
        Task<bool> InsertReserve(Reservation reservation);
        Task<bool> UpdateReserve(Reservation reservation);
        Task<bool> CancelReserve(int id);
    }
}
