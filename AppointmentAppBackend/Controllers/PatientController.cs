using AppointmentAppBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentAppBackend.Model;
using System.Diagnostics.Metrics;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PatientController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        
        public async Task<IEnumerable<Patient>> Get() => await _context.Patients.ToListAsync();

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
           var issue = await _context.Patients.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = patient.PatientId }, patient);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient) {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!patientExists(id))
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

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var appDelete = await _context.Patients.FindAsync(id);
            if (appDelete == null) return NotFound();
            _context.Patients.Remove(appDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("id/appointments")]
        public async Task<IActionResult> getAppointments(int id)
        {
           var app = await _context.Appointments.ToListAsync();
            List<Appointment> appointment = new List<Appointment>();
            // we have patient id we just need to loop throught appointment table for that id
            foreach(Appointment a in app)
            {
                if (a.PatientId == id)
                {
                    appointment.Add(a);
                }
            }
            List<Patient> jatin = new List<Patient>();            
            return appointment == null ? NotFound() : Ok(appointment);
        }

        private bool patientExists(int id){   
            return _context.Patients.Any(e => e.PatientId == id);
        }
       

    }
}
