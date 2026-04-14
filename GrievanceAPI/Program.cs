using GrievanceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// DATABASE CONNECTION
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();

app.MapControllers();

// OPENAPI (for Scalar)
app.MapOpenApi();

// SCALAR UI
app.MapScalarApiReference();

app.Run();