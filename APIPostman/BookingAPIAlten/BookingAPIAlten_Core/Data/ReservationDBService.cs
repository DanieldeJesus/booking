using BookingAPIAlten_Core.Data.Utils;
using Dapper.FastCrud;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookingAPIAlten_Core.Data
{
    public class ReservationDBService : IDisposable
    {
        private readonly AppSettings _appSettings;
        public MySqlConnection Connection { get; set; }

        public ReservationDBService(AppSettings appSettings)
        {
            _appSettings = appSettings;
            OrmConfiguration.DefaultDialect = SqlDialect.MySql;
            Connection = new MySqlConnection(appSettings.DefaultConnection);

            if (Connection.State == ConnectionState.Open)
                Connection.Close();

            Connection.Open();
        }

        public MySqlConnection GetConnection()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MySql;
            var connection = new MySqlConnection(_appSettings.DefaultConnection);
            connection.Open();
            return connection;
        }

        public void Dispose()
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();

            GC.SuppressFinalize(this);
        }
    }
}
