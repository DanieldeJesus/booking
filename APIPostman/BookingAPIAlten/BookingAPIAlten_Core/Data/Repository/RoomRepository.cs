using BookingAPIAlten_Core.Domain;
using BookingAPIAlten_Core.Domain.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingAPIAlten_Core.Data.Repository
{
    public class RoomRepository: IRoomRepository
    {
        private readonly ReservationDBService _reservationDB;
        public RoomRepository(ReservationDBService reservationDB)
        {
            _reservationDB = reservationDB;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoom(DateTime? from, DateTime? to)
        {
            var sql = $@" SELECT
	                            tr.id,
	                            tr.name,
	                            tr.beds,
	                            tr.status
                            FROM tb_room tr
                            LEFT JOIN tb_bookings tb ON tb.room_id = tr.id 
                                  AND tb.status IN(1,2) 
                                  AND (tb.booked_from BETWEEN '{from:yyyy-MM-dd}' AND '{to:yyyy-MM-dd}'
                                       OR tb.booked_to BETWEEN '{from:yyyy-MM-dd}' AND '{to:yyyy-MM-dd}') 
                            WHERE tr.status = 1 
                              AND isnull(tb.id) ";

            return await _reservationDB.Connection.QueryAsync<Room>(sql);
        }

        public void Dispose()
        {
            _reservationDB?.Dispose();
        }
    }
}
