using System;
using System.ComponentModel.DataAnnotations;
namespace MakeUpAName.Models.PatientIntake
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string locationName { get; set; }
        public int LeftCoordinate { get; set; }
        public int TopCoordinate { get; set; }
        public int PatientId { get; set; }
        //Navigate to patient
        public Patient Patient { get; set; }

        public ICollection<Patient> Patients { get; set; }
        public int RoomId { get; set; } // Foreign key to Room
        public Room Room { get; set; }
    }
}