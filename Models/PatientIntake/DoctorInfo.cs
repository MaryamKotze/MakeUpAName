using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeUpAName.Models.PatientIntake
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Please enter full name.")]
        public string Name { get; set; }

        
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

       
        [Range(12, 13, ErrorMessage = "ID should be 12 digits in lenght.")]

        public string IdentificationNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string CellphoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter office number.")]
        public string OfficeNumber { get; set; }
        [Required(ErrorMessage = "Please enter Qualifications.")]
        public string Qualifications { get; set; }
        // Navigation property to Department
        //public Department Department { get; set; }
        // Navigation property to Patient
        public ICollection<Patient> Patients { get; set; }
        
    }
}