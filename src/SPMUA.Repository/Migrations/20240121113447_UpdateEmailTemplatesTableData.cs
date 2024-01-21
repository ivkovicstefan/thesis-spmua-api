using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmailTemplatesTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: 1,
                column: "EmailTemplateHtml",
                value: "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Zahtev za rezervaciju uspešno poslat</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr><tr> <th>Cena</th> <td>{{ServiceTypePrice}}</td> </tr> </table></body></html>");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: 2,
                column: "EmailTemplateHtml",
                value: "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Novi zahtev za rezervaciju</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Email adresa</th> <td>{{CustomerEmail}}</td> </tr> <tr> <th>Broj telefona</th> <td>{{CustomerPhone}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr><tr> <th>Cena</th> <td>{{ServiceTypePrice}}</td> </tr> </table></body></html>");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: 3,
                column: "EmailTemplateHtml",
                value: "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Rezervacija je odobrena</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr><tr> <th>Cena</th> <td>{{ServiceTypePrice}}</td> </tr> <tr> <th>Komentar</th> <td>{{ResponseComment}}</td> </tr> </table></body></html>");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: 4,
                column: "EmailTemplateHtml",
                value: "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Rezervacija je odbijena</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr><tr> <th>Cena</th> <td>{{ServiceTypePrice}}</td> </tr> <tr> <th>Komentar</th> <td>{{ResponseComment}}</td> </tr> </table></body></html>");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: 1,
                column: "EmailTemplateHtml",
                value: "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Zahtev za rezervaciju uspešno poslat</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr> </table></body></html>");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: 2,
                column: "EmailTemplateHtml",
                value: "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Novi zahtev za rezervaciju</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Email adresa</th> <td>{{CustomerEmail}}</td> </tr> <tr> <th>Broj telefona</th> <td>{{CustomerPhone}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr> </table></body></html>");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: 3,
                column: "EmailTemplateHtml",
                value: "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Rezervacija je odobrena</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr> <tr> <th>Komentar</th> <td>{{ResponseComment}}</td> </tr> </table></body></html>");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: 4,
                column: "EmailTemplateHtml",
                value: "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; } table { text-align: left; } </style></head><body> <h1>Rezervacija je odbijena</h1> <table> <tr> <th>Broj rezervacije</th> <td>{{AppointmentId}}</td> </tr> <tr> <th>Tip usluge</th> <td>{{ServiceTypeName}}</td> </tr> <tr> <th>Ime i prezime</th> <td>{{CustomerFullName}}</td> </tr> <tr> <th>Datum</th> <td>{{AppointmentDate}}</td> </tr> <tr> <th>Termin</th> <td>{{AppointmentTimeInterval}}</td> </tr> <tr> <th>Komentar</th> <td>{{ResponseComment}}</td> </tr> </table></body></html>");
        }
    }
}
