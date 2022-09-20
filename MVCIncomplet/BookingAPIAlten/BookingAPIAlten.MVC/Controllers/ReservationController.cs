using BookingAPIAlten.Application.Services;
using BookingAPIAlten.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookingAPIAlten.MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationAppService _reservationAppService;

        public ReservationController(IReservationAppService reservationAppService)
        {
            _reservationAppService = reservationAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("Reservations")]
        public async Task<IActionResult> Index()
        {
            return View(await _reservationAppService.GetReservsRooms());
        }

        [HttpGet]
        [Route("AvailableRoom")]
        public async Task<IActionResult> AvailableRoom(DateTime? from, DateTime? to)
        {
            return View(await _reservationAppService.GetAvailableRoom(from, to));
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
