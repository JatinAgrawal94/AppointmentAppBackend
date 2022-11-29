using System.ComponentModel.DataAnnotations;
namespace AppointmentAppBackend.Model
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        public string? DoctorName { get; set; }
        [Required]
        public string? DoctorEmail { get; set; }
        [Required]
        public string? DateOfBirth { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string? Bloodgroup { get; set; }
        [Required]
        public string? Designation { get; set; }
        [Required]
        public string? Timings { get; set; }
        public string? Address { get; set; }
    }
}
