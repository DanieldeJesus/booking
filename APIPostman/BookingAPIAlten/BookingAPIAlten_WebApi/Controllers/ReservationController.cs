using BookingAPIAlten_Core.Domain.Model;
using BookingAPIAlten_Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookingAPIAlten_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationAppService _reservationAppService;

        public ReservationController(IReservationAppService reservationAppService)
        {
            _reservationAppService = reservationAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("available-room/{from:datetime}/{to:datetime}")]
        public async Task<IActionResult> Index(DateTime? from, DateTime? to)
        {
            return Ok(await _reservationAppService.GetAvailableRoom(from, to));
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertReserve(Reservation reservation)
        {
            return Ok(await _reservationAppService.InsertReserve(reservation));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateReserve(Reservation reservation)
        {
            return Ok(await _reservationAppService.UpdateReserve(reservation));
        }

        [HttpPut]
        [Route("cancel/{id}")]
        public async Task<IActionResult> CancelReserve(int id)
        {
            return Ok(await _reservationAppService.CancelReserve(id));
        }
    }
}
