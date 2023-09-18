using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace MakeUpAName.Models.PatientIntake
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        // Navigation property to Doctor
        public ICollection<Doctor> Doctors { get; set; }

    }
}
