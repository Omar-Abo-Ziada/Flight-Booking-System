
using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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


            // Registering Repos :
            builder.Services.AddScoped<IAirLineRepository, AirLineRepository>();
            builder.Services.AddScoped<IAirPortRepository, AirPortRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IFlightRepository, FlightRepository>();
            builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
            builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
            builder.Services.AddScoped<IPlaneRepository, PlaneRepository>();
            builder.Services.AddScoped<ISeatRepository, SeatRepository>();
            builder.Services.AddScoped<IStateRepository, StateRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();

            // Registering AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Registering Identiny
            builder.Services.AddIdentity<ApplicationUSer, IdentityRole>(options =>
            {
                // removing some validations for easier testing
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                //options.SignIn.RequireConfirmedAccount = true;        // maybe later
            })
            .AddEntityFrameworkStores<ITIContext>();


            builder.Services.AddAuthentication(options =>
            {
                // adjusting the authorize attr to look for JWT Bearer tokens not schema
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                // in case of failer(challenge) => see the JWT default behaviour which is returing UnAuthorized res
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                // see the other schemas and change them with the JWT default schema
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // the token must be saved not written
                options.SaveToken = true;
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
