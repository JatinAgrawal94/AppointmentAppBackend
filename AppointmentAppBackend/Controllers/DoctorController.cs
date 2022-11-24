using AppointmentAppBackend.Data;
using AppointmentAppBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DoctorController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Doctor>> Get() => await _context.Doctors.ToListAsync();

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Doctors.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor appointment)
        {
            await _context.Doctors.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = appointment.DoctorId }, appointment);
        }
    }
}
