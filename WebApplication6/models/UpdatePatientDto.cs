using System.ComponentModel.DataAnnotations;

namespace canser2.Models   // ⚠️ لازم نفس الـ namespace
{
    public class UpdatePatientDto
    {
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
