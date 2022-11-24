using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppointmentAppBackend.Data;
using AppointmentAppBackend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AppointmentController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Appointment>> Get(){ 
            var app= await _context.Appointments.ToListAsync();
            /*foreach (Appointment appointment in app)
            {
                Console.WriteLine(appointment.AppointmentId);
            }*/
            return app;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Appointments.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = appointment.AppointmentId }, appointment);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var appDelete = await _context.Appointments.FindAsync(id);  
            if (appDelete == null) return NotFound();
            _context.Appointments.Remove(appDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
