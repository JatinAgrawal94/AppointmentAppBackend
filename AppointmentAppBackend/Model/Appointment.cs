using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentAppBackend.Model
{
    public class Appointment
    {
        // patientname, patientemail,doctorname,doctoremail,date,appointmenttype,reason,timing,visittype,paymentamount,paymentstatus.
        [Key]
        public int AppointmentId { get; set; }

        public string? PatientName { get; set; }
        public string? PatientEmail { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        //public Patient? Patient { get; set; } = null!;
        
        

        //public string Doctorname { get; set; }
        //public string Doctoremail { get; set; }
        //public string Date { get; set; }
        //public string AppointmentType { get; set; }
        //public string Reason { get; set; }
        //public string Timing { get; set; }
        //public string VisitType { get; set; }
        //public string PaymentAmount { get; set; }
        //public string PaymentStatus { get; set; }

    }
}
