using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace ShiftsLoggerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ShiftLoggerDbContext>(options =>
        {
            // Retrieve the connection string from the configuration file
            var connectionString = ConfigurationManager.ConnectionStrings["ShiftLoggerDbContext"].ConnectionString;

            // Configure the DbContext to use SQL Server with the retrieved connection string
            options.UseSqlServer(connectionString);
        })
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
