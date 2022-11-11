﻿using AppointmentAppBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentAppBackend.Model;
namespace AppointmentAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Create(Patient appointment)
        {
            await _context.Patients.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = appointment.PatientId }, appointment);
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
    }
}