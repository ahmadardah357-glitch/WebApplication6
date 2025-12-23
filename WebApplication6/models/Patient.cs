using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace canser2.Models
{
    [Table("Patient")]
    public class Patient
    {

        [Key]
        [Column("patient_id")]
        public int PatientId { get; set; }

        [Required]
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        [Column("gender")]
        public string Gender { get; set; }

        // FK → AspNetUsers.Id
        [Required]
        [StringLength(450)]
        [Column("aspnet_user_id")]
        public string AspNetUserId { get; set; }    
    }
}
