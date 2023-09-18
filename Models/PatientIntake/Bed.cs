using System;
namespace MakeUpAName.Models.PatientIntake
{
    public class Bed
    {
        public int BedId { get; set; }
        public string BedNo { get; set; }
        public bool IsOccupied { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public ICollection<Patient> Patients { get; set; }


    }
}