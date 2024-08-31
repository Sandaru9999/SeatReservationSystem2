using Microsoft.AspNetCore.Mvc;
using SeatReservationSystem2.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly ISeatReservationService _service;

    public ReservationController(ISeatReservationService service)
    {
        _service = service;
    }

    [HttpGet("available-seats")]
    public async Task<IActionResult> GetAvailableSeats()
    {
        var seats = await _service.GetAvailableSeats();
        return Ok(seats);
    }

    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveSeat(int employeeId, int seatId)
    {
        var reservation = await _service.ReserveSeat(employeeId, seatId);
        if (reservation == null) return BadRequest("Seat is already reserved or not found.");
        return Ok(reservation);
    }

    [HttpGet("reservations")]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _service.GetReservations();
        return Ok(reservations);
    }
    [HttpDelete("cancel/{reservationId}")]
    public async Task<IActionResult> CancelReservation(int reservationId)
    {
        var result = await _service.CancelReservation(reservationId);
        if (!result) return NotFound("Reservation not found or already canceled.");
        return Ok("Reservation canceled successfully.");
    }

}
