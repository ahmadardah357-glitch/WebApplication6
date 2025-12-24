using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace canser2.Models
{
    [Table("Attends")]
    public class Attends
    {
 
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }

       
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

   
    }
}
