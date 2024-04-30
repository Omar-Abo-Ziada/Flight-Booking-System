
using Flight_Booking_System.Context;
using Flight_Booking_System.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //************************************************************************************************

            //  Registering context
            builder.Services.AddDbContext<ITIContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=Flight_Booking_System;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            });

            //registering services 
            builder.Services.AddScoped<IAirLineRepository, AirLineRepository>();
            builder.Services.AddScoped<AirPortRepository, AirPortRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IFlightRepository, FlightRepository>();
            builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
            builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
            builder.Services.AddScoped<IPlaneRepository, PlaneRepository>();
            builder.Services.AddScoped<ISeatRepository, SeatRepository>();
            builder.Services.AddScoped<IStateRepository, StateRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();




            // to make the provider able to serve any consumer from other domains
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    //policy.WithOrigins("http://our react app domain") // or just limited on our react app domain

                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();  // used to allow the authorization tokens / cookies 
                });
            });

            //************************************************************************************************

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //************************************************************************************************

            app.UseStaticFiles(); // to make it able to read static pages in wwwroot if needed

            app.UseCors("MyPolicy"); // to make the provider able to serve consumer from other domains

            //************************************************************************************************

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
