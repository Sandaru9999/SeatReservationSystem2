using System.Collections.Generic;
using System.Threading.Tasks;
using SeatReservationSystem2.Models;



public interface ISeatReservationService
{
    Task<IEnumerable<Seat>> GetAvailableSeats();
    Task<Reservation> ReserveSeat(int employeeId, int seatId);
    Task<IEnumerable<Reservation>> GetReservations();
    Task<bool> CancelReservation(int reservationId);
    Task<Employee> RegisterEmployee(Employee employee);
    Task<Employee> Authenticate(string email, string password);

}
