using Microsoft.EntityFrameworkCore;
using SPMUA.Model.Models;
using System.Reflection.Metadata;
using TinyHelpers.EntityFrameworkCore.Comparers;
using TinyHelpers.EntityFrameworkCore.Converters;

namespace SPMUA.Repository.Data
{
    public class SpmuaDbContext : DbContext
    {
        public SpmuaDbContext(DbContextOptions<SpmuaDbContext> options) : base(options)
        {
      
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set;}
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<WorkingDay> WorkingDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkingDay>(entity =>
            {
                entity.Property(e => e.StartTime)
                      .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
                entity.Property(e => e.EndTime)
                      .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
            });

            modelBuilder.Entity<WorkingDay>()
                        .HasData(new WorkingDay { WorkingDayId = 1, WorkingDayName = "Monday", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 2, WorkingDayName = "Tuesday", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 3, WorkingDayName = "Wednesday", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 4, WorkingDayName = "Thursday", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 5, WorkingDayName = "Friday", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 6, WorkingDayName = "Saturday", StartTime = TimeOnly.Parse("07:00:00"), EndTime = TimeOnly.Parse("18:00:00") },
                                 new WorkingDay { WorkingDayId = 7, WorkingDayName = "Sunday", StartTime = TimeOnly.Parse("07:00:00"), EndTime = TimeOnly.Parse("18:00:00") });

            modelBuilder.Entity<AppointmentStatus>()
                        .HasData(new AppointmentStatus { AppointmentStatusId = 1, AppointmentStatusName = "Confirmation Pending" },
                                 new AppointmentStatus { AppointmentStatusId = 2, AppointmentStatusName = "Confirmed" },
                                 new AppointmentStatus { AppointmentStatusId = 3, AppointmentStatusName = "Rejected" });

            modelBuilder.Entity<ServiceType>()
                        .HasData(new ServiceType { ServiceTypeId = 1, ServiceTypeName = "Makeup", ServiceTypePrice = 2400.00m, ServiceTypeDuration = 60, IsAvailableOnMonday = true, IsAvailableOnTuesday = true, IsAvailableOnWednesday = true, IsAvailableOnThursday = true, IsAvailableOnFriday = true, IsAvailableOnSaturday = true, IsAvailableOnSunday = true },
                                 new ServiceType { ServiceTypeId = 2, ServiceTypeName = "Brow lift", ServiceTypePrice = 1500.00m, ServiceTypeDuration = 60, IsAvailableOnMonday = true, IsAvailableOnTuesday = true, IsAvailableOnWednesday = true, IsAvailableOnThursday = true, IsAvailableOnFriday = true, IsAvailableOnSaturday = false, IsAvailableOnSunday = false },
                                 new ServiceType { ServiceTypeId = 3, ServiceTypeName = "Lash lift", ServiceTypePrice = 1700.00m, ServiceTypeDuration = 90, IsAvailableOnMonday = true, IsAvailableOnTuesday = true, IsAvailableOnWednesday = true, IsAvailableOnThursday = true, IsAvailableOnFriday = true, IsAvailableOnSaturday = false, IsAvailableOnSunday = false },
                                 new ServiceType { ServiceTypeId = 4, ServiceTypeName = "Brow & Lash lift", ServiceTypePrice = 3000.00m, ServiceTypeDuration = 90, IsAvailableOnMonday = true, IsAvailableOnTuesday = true, IsAvailableOnWednesday = true, IsAvailableOnThursday = true, IsAvailableOnFriday = true, IsAvailableOnSaturday = false, IsAvailableOnSunday = false });
        }
    }
}
