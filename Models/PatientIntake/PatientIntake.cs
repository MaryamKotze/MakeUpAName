using System;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MakeUpAName.Models.PatientIntake
{
    public class Patient
    {
        [Key] // This marks PatientId as the primary key
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Full name of patient required.")]

        public string Name { get; set; }
        [Range(12, 13, ErrorMessage = "ID should be a lenght of 12 digits.")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Please select a bloodtype.")]
        public string Bloodtype { get; set; }
        public bool IsBloodTypeA { get; set; }
        public bool IsBloodTypeB { get; set; }
        public bool IsBloodTypeAB { get; set; }
        public bool IsBloodTypeO { get; set; }
        public int? BedId { get; set; }
        public Bed Bed { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter official diagnosis.")]
        public string Diagnosis { get; set; }
        [Required(ErrorMessage = "Please enter prescribed medications.")]
        public string Medications { get; set; }
        public string AdditionalNotes { get; set; }
        public int DoctorId { get; set; }
        // Navigation property to Doctor
        public Doctor Doctor { get; set; }
        public int SelectedRoomId { get; set; }
        public IEnumerable<SelectListItem> RoomOptions { get; set; } // Dropdown list for rooms
        public int SelectedBedId { get; set; }
        public IEnumerable<SelectListItem> BedOptions { get; set; } // Dropdown list for beds
                                                                    // Other properties specific to the view, if needed
        public int SelectedDoctorId { get; set; }
        public IEnumerable<SelectListItem> DoctorOptions { get; set; } // Dropdown list for rooms
        public int SelectedDepartmentId { get; set; }
        public IEnumerable<SelectListItem> DepartmentOptions { get; set; } // Dropdown list for 
        //beds
        public int SelectedLocationId { get; set; }
        public IEnumerable<SelectListItem> LocationOptions { get; set; } // Dropdown list for beds
        public int LocationId { get; set; }
        public Location Location { get; set; }
        // Other properties specific to the view, if needed

        public int RoomId {get;set;}
        public Room Room { get; set; }

       

    }
}