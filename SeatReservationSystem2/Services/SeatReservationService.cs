using Microsoft.EntityFrameworkCore;


using SeatReservationSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SeatReservationService : ISeatReservationService
{
    private readonly SeatReservationContext _context;

    public SeatReservationService(SeatReservationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Seat>> GetAvailableSeats()
    {
        return await _context.Seats.Where(s => !s.IsReserved).ToListAsync();
    }

    public async Task<Reservation> ReserveSeat(int employeeId, int seatId)
    {
        var seat = await _context.Seats.FindAsync(seatId);
        if (seat == null || seat.IsReserved) return null;

        seat.IsReserved = true;
        var reservation = new Reservation
        {
            EmployeeId = employeeId,
            SeatId = seatId,
            ReservationDate = DateTime.Now
        };

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return reservation;
    }

    public async Task<IEnumerable<Reservation>> GetReservations()
    {
        return await _context.Reservations
                             .Include(r => r.Employee)
                             .Include(r => r.Seat)
                             .ToListAsync();
    }

    public async Task<Employee> RegisterEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
    public async Task<bool> CancelReservation(int reservationId)
    {
        var reservation = await _context.Reservations.FindAsync(reservationId);
        if (reservation == null) return false;

        var seat = await _context.Seats.FindAsync(reservation.SeatId);
        if (seat != null)
        {
            seat.IsReserved = false;
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<Employee> Authenticate(string email, string password)
    {
        return await _context.Employees
                             .FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
    }
}
