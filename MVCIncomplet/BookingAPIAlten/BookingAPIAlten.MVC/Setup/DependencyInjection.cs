using BookingAPIAlten.Application.Services;
using BookingAPIAlten.Data;
using BookingAPIAlten.Data.Repository;
using BookingAPIAlten.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace BookingAPIAlten.MVC.Setup
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