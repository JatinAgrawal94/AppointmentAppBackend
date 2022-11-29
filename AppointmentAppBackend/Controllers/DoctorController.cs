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


        // get all doctor
        [HttpGet]
        public async Task<IEnumerable<Doctor>> GetDoctors() => await _context.Doctors.ToListAsync();

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Doctors.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor profile)
        {
            await _context.Doctors.AddAsync(profile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = profile.DoctorId }, profile);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.DoctorId)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!doctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // delete a doctor
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var appDelete = await _context.Doctors.FindAsync(id);
            if (appDelete == null) return NotFound();
            _context.Doctors.Remove(appDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /*[HttpGet("id/appointments")]
        public async Task<IActionResult> getAppointments(int id)
        {
            var app = await _context.Appointments.ToListAsync();
            List<Appointment> appointment = new List<Appointment>();
            // we have patient id we just need to loop throught appointment table for that id
            foreach (Appointment a in app)
            {
                if (a.PatientId == id)
                {
                    appointment.Add(a);
                }
            }
            List<Patient> jatin = new List<Patient>();
            return appointment == null ? NotFound() : Ok(appointment);
        }*/

        private bool doctorExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
