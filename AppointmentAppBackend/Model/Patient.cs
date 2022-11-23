using System.ComponentModel.DataAnnotations;

namespace AppointmentAppBackend.Model
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string? PatientName { get; set; }
        [Required]
        public string? PatientEmail { get; set; }
        [Required]
        public string? DateOfBirth { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public int Contact { get; set; }
        public string? Bloodgroup { get; set; }
        public string? Address { get; set; }

        public ICollection<Appointment>? Appointments { get; set; } = null!;
    }
}
