using Microsoft.EntityFrameworkCore;

using SeatReservationSystem2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<SeatReservationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NzWalk"))); // Ensure "NzWalk" is correctly set up in appsettings.json

// Register custom services
builder.Services.AddScoped<ISeatReservationService, SeatReservationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();
app.MapControllers();

app.Run();
