using Microsoft.EntityFrameworkCore;
using SPMUA.API.Middlewares;
using SPMUA.Model.Queues;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;
using SPMUA.Repository.Implementations;
using SPMUA.Service.Contracts;
using SPMUA.Service.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddDbContext<SpmuaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SPMUADB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IWorkingDayService, WorkingDayService>();
builder.Services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddSingleton<IEmailRepository, EmailRepository>();
builder.Services.AddSingleton<EmailQueue>();
builder.Services.AddHostedService<EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
