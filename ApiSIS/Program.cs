using Microsoft.EntityFrameworkCore;
using ApiSIS.Data;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Controllers
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger - aktifkan untuk testing
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "./v1/swagger.json",
        "API SIS V1"
    );
    options.RoutePrefix = "swagger";
});

// CATATAN: UseHttpsRedirection() dimatikan (di-comment) karena di Docker/InstaBox 
// HTTPS sudah ditangani oleh reverse proxy InstaBox. 
// Jika dinyalakan, akan muncul warning "Failed to determine the https port for redirect".
// app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();