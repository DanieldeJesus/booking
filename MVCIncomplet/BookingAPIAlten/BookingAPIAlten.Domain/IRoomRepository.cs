using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingAPIAlten.Core.Data;

namespace BookingAPIAlten.Domain
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetAvailableRoom(DateTime? from, DateTime? to);
    }
}
