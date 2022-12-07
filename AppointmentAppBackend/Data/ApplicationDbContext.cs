using Microsoft.EntityFrameworkCore;
using AppointmentAppBackend.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace AppointmentAppBackend.Data
{
    public class ApplicationDbContext: IdentityDbContext<HospitalUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            
        }
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Appointment>? Appointments { get; set;}
        public DbSet<Doctor>? Doctors { get; set; }
    
    }
}
