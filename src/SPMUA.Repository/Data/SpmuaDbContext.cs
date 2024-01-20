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

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set;}
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ServiceTypePriceHistory> ServiceTypePriceHistory { get; set; }
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
                        .HasData(new WorkingDay { WorkingDayId = 1, WorkingDayName = "Ponedeljak", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 2, WorkingDayName = "Utorak", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 3, WorkingDayName = "Sreda", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 4, WorkingDayName = "Četvrtak", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 5, WorkingDayName = "Petak", StartTime = TimeOnly.Parse("10:00:00"), EndTime = TimeOnly.Parse("16:00:00") },
                                 new WorkingDay { WorkingDayId = 6, WorkingDayName = "Subota", StartTime = TimeOnly.Parse("07:00:00"), EndTime = TimeOnly.Parse("18:00:00") },
                                 new WorkingDay { WorkingDayId = 7, WorkingDayName = "Nedelja", StartTime = TimeOnly.Parse("07:00:00"), EndTime = TimeOnly.Parse("18:00:00") });

            modelBuilder.Entity<AppointmentStatus>()
                        .HasData(new AppointmentStatus { AppointmentStatusId = 1, AppointmentStatusName = "Confirmation Pending" },
                                 new AppointmentStatus { AppointmentStatusId = 2, AppointmentStatusName = "Confirmed" },
                                 new AppointmentStatus { AppointmentStatusId = 3, AppointmentStatusName = "Rejected" });

            modelBuilder.Entity<ServiceType>()
                        .HasData(new ServiceType { ServiceTypeId = 1, ServiceTypeName = "Makeup", ServiceTypeDuration = 60, IsAvailableOnMonday = true, IsAvailableOnTuesday = true, IsAvailableOnWednesday = true, IsAvailableOnThursday = true, IsAvailableOnFriday = true, IsAvailableOnSaturday = true, IsAvailableOnSunday = true },
                                 new ServiceType { ServiceTypeId = 2, ServiceTypeName = "Brow lift", ServiceTypeDuration = 60, IsAvailableOnMonday = true, IsAvailableOnTuesday = true, IsAvailableOnWednesday = true, IsAvailableOnThursday = true, IsAvailableOnFriday = true, IsAvailableOnSaturday = false, IsAvailableOnSunday = false },
                                 new ServiceType { ServiceTypeId = 3, ServiceTypeName = "Lash lift", ServiceTypeDuration = 90, IsAvailableOnMonday = true, IsAvailableOnTuesday = true, IsAvailableOnWednesday = true, IsAvailableOnThursday = true, IsAvailableOnFriday = true, IsAvailableOnSaturday = false, IsAvailableOnSunday = false },
                                 new ServiceType { ServiceTypeId = 4, ServiceTypeName = "Brow & Lash lift", ServiceTypeDuration = 90, IsAvailableOnMonday = true, IsAvailableOnTuesday = true, IsAvailableOnWednesday = true, IsAvailableOnThursday = true, IsAvailableOnFriday = true, IsAvailableOnSaturday = false, IsAvailableOnSunday = false });
            
            modelBuilder.Entity<ServiceTypePriceHistory>()
                        .HasData(new ServiceTypePriceHistory { ServiceTypePriceHistoryId = 1, ServiceTypeId = 1, ServiceTypePrice = 2400.00m, CreatedDate = new DateTime() },
                                 new ServiceTypePriceHistory { ServiceTypePriceHistoryId = 2, ServiceTypeId = 2, ServiceTypePrice = 1500.00m, CreatedDate = new DateTime() },
                                 new ServiceTypePriceHistory { ServiceTypePriceHistoryId = 3, ServiceTypeId = 3, ServiceTypePrice = 1700.00m, CreatedDate = new DateTime() },
                                 new ServiceTypePriceHistory { ServiceTypePriceHistoryId = 4, ServiceTypeId = 4, ServiceTypePrice = 3000.00m, CreatedDate = new DateTime() });
            
            modelBuilder.Entity<EmailTemplate>()
                        .HasData(new EmailTemplate() { EmailTemplateId = 1, EmailTemplateName = "Appointment Request Confirmation Pending", EmailTemplateTitle = "Zahtev za rezervaciju {{AppointmentId}} uspešno poslat", EmailTemplateHtml = "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Zahtev za rezervaciju uspešno poslat</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr> </table></body></html>" },
                                 new EmailTemplate() { EmailTemplateId = 2, EmailTemplateName = "Appointment Request Arrived", EmailTemplateTitle = "Novi zahtev za rezervaciju {{AppointmentId}}", EmailTemplateHtml = "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Novi zahtev za rezervaciju</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Email adresa</th> <td>{{CustomerEmail}}</td> </tr> <tr> <th>Broj telefona</th> <td>{{CustomerPhone}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr> </table></body></html>" },
                                 new EmailTemplate() { EmailTemplateId = 3, EmailTemplateName = "Appointment Request Confirmed", EmailTemplateTitle = "Rezervacija {{AppointmentId}} je odobrena", EmailTemplateHtml = "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Rezervacija je odobrena</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr> <tr> <th>Komentar</th> <td>{{ResponseComment}}</td> </tr> </table></body></html>" },
                                 new EmailTemplate() { EmailTemplateId = 4, EmailTemplateName = "Appointment Request Rejected", EmailTemplateTitle = "Rezervacija {{AppointmentId}} je odbijena", EmailTemplateHtml = "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Rezervacija je odbijena</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr> <tr> <th>Komentar</th> <td>{{ResponseComment}}</td> </tr> </table></body></html>" });
        }
    }
}
