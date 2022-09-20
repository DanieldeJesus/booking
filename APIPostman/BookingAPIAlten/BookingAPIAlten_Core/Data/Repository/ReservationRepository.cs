using BookingAPIAlten_Core.Domain;
using BookingAPIAlten_Core.Domain.Enum;
using BookingAPIAlten_Core.Domain.Model;
using Dapper;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPIAlten_Core.Data.Repository
{
    public class ReservationRepository: IReservationRepository
    {
        private readonly ReservationDBService _reservationDB;
        public ReservationRepository(ReservationDBService reservationDB)
        {
            _reservationDB = reservationDB;
        }

        public async Task<Reservation> GetReserveName(string name)
        {
            var sql = $@" SELECT
	                             tr.id,
	                             tr.guest_name AS Name,
	                             tr.guest_email AS Email,
	                             tr.guest_phone AS Phone,
                            FROM tb_reservation tr
                           WHERE tr.guest_name like '%{name}%'";

            return AppendBookings(await _reservationDB.Connection.QueryFirstOrDefaultAsync<Reservation>(sql));
        }

        public async Task<Reservation> GetReserveId(int id)
        {
            var sql = $@" SELECT
	                             tr.id,
	                             tr.guest_name AS Name,
	                             tr.guest_email AS Email,
	                             tr.guest_phone AS Phone,
                            FROM tb_reservation tr
                           WHERE tr.id = {id}";

            return AppendBookings(await _reservationDB.Connection.QueryFirstOrDefaultAsync<Reservation>(sql));
        }

        public async Task<bool> InsertReserve(Reservation reservation)
        {
            using (var scope = TransactionFactory.Create(TimeSpan.MaxValue))
            {
                try
                {
                    using var connection = this._reservationDB.GetConnection();
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var queryBuilder = new StringBuilder();
                    queryBuilder.Append("  INSERT INTO tb_reservation (guest_name, guest_email, guest_phone, created_at, updated_at, status)");
                    queryBuilder.Append($" VALUES ('{reservation.Name}', '{reservation.Email}', '{reservation.Phone}', NOW(), NOW(), {(int)StatusReservationEnum.Reserved});");
                    queryBuilder.Append("  SELECT LAST_INSERT_ID();");

                    reservation.Id = await connection.ExecuteScalarAsync<int>(queryBuilder.ToString(), commandType: CommandType.Text);

                    queryBuilder.Clear();
                    queryBuilder.Append("  INSERT INTO tb_bookings (room_id, reservation_id, booked_from, booked_to, created_at, updated_at, status)");
                    queryBuilder.Append($" VALUES ({reservation.ReservationRoom.Room.Id}, {reservation.Id}, '{reservation.ReservationRoom.BookedFrom:yyyy-MM-dd}', '{reservation.ReservationRoom.BookedTo:yyyy-MM-dd 23:59:59}', NOW(), NOW(), {(int)StatusReservationEnum.Reserved})");

                    connection.Execute(queryBuilder.ToString(), commandType: CommandType.Text);

                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return false;
                }                
            }

            return true;
        }

        public async Task<bool> UpdateReserve(Reservation reservation)
        {
            using (var scope = TransactionFactory.Create(TimeSpan.MaxValue))
            {
                try
                {
                    using var connection = this._reservationDB.GetConnection();
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var queryBuilder = new StringBuilder();
                    queryBuilder.Append("  UPDATE tb_reservation SET ");
                    queryBuilder.Append($"        guest_name='{reservation.Name}', ");
                    queryBuilder.Append($"        guest_email='{reservation.Email}', ");
                    queryBuilder.Append($"        guest_phone='{reservation.Phone}', ");
                    queryBuilder.Append($"        updated_at=NOW() ");
                    queryBuilder.Append($"  WHERE id = {reservation.Id};");

                    await connection.ExecuteScalarAsync<int>(queryBuilder.ToString(), commandType: CommandType.Text);

                    queryBuilder.Clear();
                    queryBuilder.Append("  UPDATE tb_bookings SET ");
                    queryBuilder.Append($"        room_id={reservation.ReservationRoom.Room.Id}, ");
                    queryBuilder.Append($"        booked_from='{reservation.ReservationRoom.BookedFrom:yyyy-MM-dd}', ");
                    queryBuilder.Append($"        booked_to='{reservation.ReservationRoom.BookedTo:yyyy-MM-dd 23:59:59}', ");
                    queryBuilder.Append($"        updated_at=NOW() ");
                    queryBuilder.Append($"  WHERE id = {reservation.ReservationRoom.Id};");

                    connection.Execute(queryBuilder.ToString(), commandType: CommandType.Text);

                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> CancelReserve(int id)
        {
            using (var scope = TransactionFactory.Create(TimeSpan.MaxValue))
            {
                try
                {
                    using var connection = this._reservationDB.GetConnection();
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var queryBuilder = new StringBuilder();
                    queryBuilder.Append("  UPDATE tb_reservation SET ");
                    queryBuilder.Append($"        status={(int)StatusReservationEnum.Canceled}, ");
                    queryBuilder.Append($"        updated_at=NOW() ");
                    queryBuilder.Append($"  WHERE id = {id};");

                    await connection.ExecuteScalarAsync<int>(queryBuilder.ToString(), commandType: CommandType.Text);

                    queryBuilder.Clear();
                    queryBuilder.Append("  UPDATE tb_bookings SET ");
                    queryBuilder.Append($"        status={(int)StatusReservationEnum.Canceled}, ");
                    queryBuilder.Append($"        updated_at=NOW() ");
                    queryBuilder.Append($"  WHERE reservation_id = {id};");

                    connection.Execute(queryBuilder.ToString(), commandType: CommandType.Text);

                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return false;
                }
            }

            return true;
        }

        public void Dispose()
        {
            _reservationDB?.Dispose();
        }

        #region Private Methods
        private Reservation AppendBookings(Reservation reservation)
        {
            if (reservation != null)
            {
                var sql = $@" SELECT
	                                 tb.booked_from AS BookedFrom,
	                                 tb.booked_to AS BookedTo
                                FROM tb_bookings tb
                               WHERE tb.reservation_id = {reservation.Id}
                                 AND tb.status IN(1,2)";

                reservation.ReservationRoom = _reservationDB.Connection.QueryFirstOrDefault<Booking>(sql);
            }

            return reservation;
        }
        #endregion
    }
}
