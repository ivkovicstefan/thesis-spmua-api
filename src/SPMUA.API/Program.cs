using Microsoft.EntityFrameworkCore;
using SPMUA.API.Middlewares;
using SPMUA.Repository.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddDbContext<SpmuaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SPMUADB"));
});

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
