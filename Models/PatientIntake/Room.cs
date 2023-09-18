using System;
using System.ComponentModel.DataAnnotations;
namespace MakeUpAName.Models.PatientIntake
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomNo { get; set; }

        public ICollection<Location> Locations { get; set; }
        public ICollection<Bed> Beds { get; set; }

        // Navigation property to patients
        public ICollection<Patient> Patients { get; set; }
    }
}
