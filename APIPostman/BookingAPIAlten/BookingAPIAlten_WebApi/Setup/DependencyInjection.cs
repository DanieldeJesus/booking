using BookingAPIAlten_Core.Data;
using BookingAPIAlten_Core.Data.Repository;
using BookingAPIAlten_Core.Domain;
using BookingAPIAlten_Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingAPIAlten_WebApi.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ReservationDBService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationAppService, ReservationAppService>();
        }
    }
}
