using MakeUpAName.Models.PatientIntake;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MakeUpAName.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<Models.PatientIntake.SelectListGroup> SelectListGroups { get; set; }

        // DbSet for other model classes as needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
        .HasKey(p => p.PatientId); // Set PatientId as the primary key

            modelBuilder.Entity<Patient>()
                .Property(p => p.Name)
                .IsRequired(); // Full name of patient is required

            modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .IsRequired()
                .HasMaxLength(13); // ID should be a length of 12 digits

            modelBuilder.Entity<Patient>()
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(255) // Adjust the maximum length as needed
                .HasAnnotation("RegularExpression", "[email regex pattern]"); // Add the appropriate email regex pattern

            modelBuilder.Entity<Patient>()
                .Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(20); // Adjust the maximum length as needed

            modelBuilder.Entity<Patient>()
                .Property(p => p.Diagnosis)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.Medications)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Bed)
                .WithMany(b => b.Patients)
                .HasForeignKey(p => p.BedId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the Room entity
            modelBuilder.Entity<Room>()
                .HasKey(r => r.RoomId); // Set RoomId as the primary key

            modelBuilder.Entity<Room>()
                .Property(r => r.RoomNo)
                .IsRequired(); // RoomNo is required

            // Configure the relationship between Room and Bed entities
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Beds)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade); // Specify the delete behavior (Cascade if needed)

            modelBuilder.Entity<Room>()
               .HasMany(r => r.Patients)
               .WithOne(p => p.Room)
               .HasForeignKey(p => p.RoomId); // Use RoomId as the foreign key

            // Add any other configuration specific to the Room entity here

            modelBuilder.Entity<Models.PatientIntake.SelectListGroup>()
    .HasKey(slg => slg.SelectListGroupId);



            // Configure the Doctor entity
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.DoctorId);

            modelBuilder.Entity<Doctor>()
                .Property(d => d.DoctorId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Doctor>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<Doctor>()
                .Property(d => d.IdentificationNumber)
                .IsRequired()
                .HasMaxLength(13); // Assuming it's supposed to be 13 characters

            modelBuilder.Entity<Doctor>()
                .Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(255); // You can adjust the max length as needed

            modelBuilder.Entity<Doctor>()
                .Property(d => d.CellphoneNumber)
                .IsRequired()
                .HasMaxLength(20); // You can adjust the max length as needed

            modelBuilder.Entity<Doctor>()
                .Property(d => d.OfficeNumber)
                .IsRequired();

            modelBuilder.Entity<Doctor>()
                .Property(d => d.Qualifications)
                .IsRequired();

            // Configure the relationship between Doctor and Department
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Department)
                .WithMany(d => d.Doctors)
                .HasForeignKey(d => d.DepartmentId);

            // Configure the Department entity (if not already done)
            modelBuilder.Entity<Department>()
                .HasKey(d => d.DepartmentId);

            modelBuilder.Entity<Department>()
                .Property(d => d.DepartmentId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<Bed>(entity =>
            {
                entity.HasKey(e => e.BedId); // Define the primary key
                entity.Property(e => e.BedNo).IsRequired(); // Make BedNo required
                entity.Property(e => e.IsOccupied).IsRequired(); // Make IsOccupied required

                // Define the foreign key relationship with the Room entity
                entity.HasOne(e => e.Room)
                    .WithMany(room => room.Beds)
                    .HasForeignKey(e => e.RoomId)
                    .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed
            });

            // Add other entity configurations if needed

            // Example of seeding data
            modelBuilder.Entity<Bed>().HasData(
                new Bed { BedId = 1, BedNo = "Bed001", IsOccupied = false, RoomId = 1 },
                new Bed { BedId = 2, BedNo = "Bed002", IsOccupied = true, RoomId = 1 }
            // Add more seed data as needed
            );

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LocationId); // Define the primary key
                entity.Property(e => e.locationName).IsRequired(); // Make LocationName required
                entity.Property(e => e.LeftCoordinate).IsRequired(); // Make LeftCoordinate required
                entity.Property(e => e.TopCoordinate).IsRequired(); // Make TopCoordinate required

               
                
                // Define the foreign key relationship with the Patient entity
                modelBuilder.Entity<Room>()
                    .HasMany(room => room.Locations)
                    .WithOne(location => location.Room)
                    .HasForeignKey(location => location.RoomId);
            });

            modelBuilder.Entity<Location>()
        .HasMany(l => l.Patients)       // Location has many Patients
        .WithOne(p => p.Location)       // Patient has one Location
        .HasForeignKey(p => p.LocationId); 

            // Add other entity configurations if needed

            // Example of seeding data
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, locationName = "Location1", LeftCoordinate = 10, TopCoordinate = 20, PatientId = 1 },
                new Location { LocationId = 2, locationName = "Location2", LeftCoordinate = 30, TopCoordinate = 40, PatientId = 2 }
            // Add more seed data as needed
            );

            // Add any other configurations for your entities here


            base.OnModelCreating(modelBuilder);
        }

       


    }
}