using Microsoft.AspNetCore.Mvc;


using SeatReservationSystem2.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly ISeatReservationService _service;

    public EmployeeController(ISeatReservationService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var registeredEmployee = await _service.RegisterEmployee(employee);
            return Ok(registeredEmployee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] EmployeeLoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var loggedInEmployee = await _service.Authenticate(loginDto.Email, loginDto.Password);
            if (loggedInEmployee == null) return Unauthorized();
            return Ok(loggedInEmployee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
