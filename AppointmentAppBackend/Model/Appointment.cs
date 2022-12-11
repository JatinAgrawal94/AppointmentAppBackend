using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentAppBackend.Model
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public string? Timings { get; set; }
        public string? AppointmentType  { get; set; }
        public string? Date { get; set; }
        public string? Reason { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        //public Patient? Patient     { get; set; } = null!;

        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }
        //public Doctor Doctor { get; set; } = null!;

    }
}
