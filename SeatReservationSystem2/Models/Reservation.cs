
using SeatReservationSystem2.Models;

namespace SeatReservationSystem2.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int EmployeeId { get; set; }
        public int SeatId { get; set; }
        public DateTime ReservationDate { get; set; }
        public Employee Employee { get; set; }
        public Seat Seat { get; set; }
    }
}
