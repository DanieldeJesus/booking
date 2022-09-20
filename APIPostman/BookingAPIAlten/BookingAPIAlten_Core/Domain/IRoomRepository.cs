using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingAPIAlten_Core.Data;
using BookingAPIAlten_Core.Domain.Model;

namespace BookingAPIAlten_Core.Domain
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetAvailableRoom(DateTime? from, DateTime? to);
    }
}
