
using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Flight_Booking_System.Controllers;
using Flight_Booking_System.Services;
using Flight_Booking_System.ExternalLogin;

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
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
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
            builder.Services.AddScoped<IAirPortRepository, AirPortRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IFlightRepository, FlightRepository>();
            builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
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

                options.SignIn.RequireConfirmedEmail = true; // You can adjust this based on your requirements

            })
            .AddEntityFrameworkStores<ITIContext>()
            .AddDefaultTokenProviders(); // This line registers the default token providers
           

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

                // validate the token itself
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    // check that the issuer is this wep API
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIss"],

                    // check that the audience is target React App
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAud"],// note : don't write any sapces

                    // check the signature resulting from : key + payload 
                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
                };
            });


            /// <summary>
            /// disapling checking for authorize by default because of the [Api Controller] attribute
            /// if the model state was Invalid => the default was not to enter the action nad return directly Badrequest with arr of Errors
            /// So I disaple this default so it enters the action and returns my customized response
            /// </summary>
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
            
            //-----------------------------------------------------

            /// <summary>
            /// swager configuration to deal with register and login tokens
            /// <summary>
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });
              
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6 IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                      {
                      new OpenApiSecurityScheme
                      {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                      }
                  },
                      new string[] {}
                      }
                  });
            });

            builder.Services.AddScoped<IEmailService, EmailService>();


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
