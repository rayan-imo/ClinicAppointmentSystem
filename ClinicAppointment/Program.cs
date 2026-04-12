using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Service.IServices;
using ClinicAppointment.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClinicAppointmentDbcontext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
b => b.MigrationsAssembly(typeof(ClinicAppointmentDbcontext).Assembly.FullName))
);

builder.Services.AddScoped<IDoctorService,DoctorService>();
// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("OpenCors");
   
app.Run();
