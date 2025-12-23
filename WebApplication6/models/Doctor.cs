using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace canser2.Models
{
    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("specialization")]
        public string Specialization { get; set; }

        // FK → AspNetUsers.Id
        [Required]
        [StringLength(450)]
        [Column("aspnet_user_id")]
        public string AspNetUserId { get; set; }
    }
}
