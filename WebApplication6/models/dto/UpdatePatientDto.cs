using System.ComponentModel.DataAnnotations;

namespace WebApplication6.models.dto   // ⚠️ لازم نفس الـ namespace
{
    public class UpdatePatientDto
    {
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
