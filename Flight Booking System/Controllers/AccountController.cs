using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Response;
using Flight_Booking_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Infobip.Api.Client;
using System.Net.Mail;
using RestSharp;
using Infobip.Api.Client.Model;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Flight_Booking_System.Repositories;


namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUSer> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IPassengerRepository _passengerRepository;

        public AccountController(UserManager<ApplicationUSer> userManager, IConfiguration configuration,
            IEmailService emailService, IPassengerRepository passengerRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            _passengerRepository = passengerRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GeneralResponse>> Register(RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUSer user = new ApplicationUSer()
                {
                    UserName = userDTO.UserName,
                    PasswordHash = userDTO.Password,
                    Email = userDTO.Email,
                    PhoneNumber = userDTO.PhoneNumber,

                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,

                };

                // Also Creating a new passenger with flightId = null , flight = null , ticket = null
                Passenger userPassenger = new Passenger()
                {
                    Name = userDTO.UserName,
                    Age = userDTO.Age,
                    Gender = userDTO.Gender,
                    PassportNum = userDTO.PassportNum,
                    NationalId = userDTO.NationalId,

                    Flight = null,
                    FlightId = null,
                    
                    Ticket = null ,
                };

                _passengerRepository.Insert(userPassenger);

                _passengerRepository.Save();

                // create Account in database
                IdentityResult createAccResult = await _userManager.CreateAsync(user, userDTO.Password);

                if (createAccResult.Succeeded)
                {
                    // Adding user Role by default
                    await _userManager.AddToRoleAsync(user, "User");

                    // Generate email confirmation token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                    string mailBody = $"<!DOCTYPE html>\r\n<html>\r\n<head>\r\n  <title>Email Confirmation</title>\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n  <style>\r\n    body {{\r\n      background: #f9f9f9;\r\n      margin: 0;\r\n      padding: 0;\r\n    }}\r\n    .container {{\r\n      max-width: 640px;\r\n      margin: 0 auto;\r\n      background: #ffffff;\r\n      box-shadow: 0px 1px 5px rgba(0, 0, 0, 0.1);\r\n      border-radius: 4px;\r\n      overflow: hidden;\r\n    }}\r\n  </style>\r\n</head>\r\n<body>\r\n  <div class=\"container\">\r\n    <div style=\"background-color: #7289da; padding: 57px; text-align: center;\">\r\n      <div style=\"cursor: auto; color: white; font-family: Arial, sans-serif; font-size: 36px; font-weight: 600;\">\r\n        Welcome to SkyLink!\r\n      </div>\r\n    </div>\r\n    \r\n    <div style=\"padding: 40px 70px;\">\r\n      <div style=\"color: #737f8d; font-family: Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n        <h2 style=\"font-weight: 500; font-size: 20px; color: #4f545c;\">Hey SmilesDavis,</h2>\r\n        <p>\r\n          Welcome aboard SkyLink! 🚀 Thanks for signing up! We're thrilled to have you join our community.\r\n        </p>\r\n        <p>\r\n          To get started, we just need to confirm your email address to ensure everything runs smoothly.\r\n        </p>\r\n        <p>\r\n          Click the button below to verify your email and unlock all the amazing features SkyLink has to offer:\r\n        </p>\r\n      </div>\r\n      <div style=\"text-align: center; padding: 20px;\">\r\n        <a href=\"{confirmationLink}\" style=\"display: inline-block; background-color: #7289da; color: white; text-decoration: none; padding: 15px 30px; border-radius: 3px;\">Verify Email</a>\r\n      </div>\r\n      <div style=\"color: #737f8d; font-family: Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n        <p>If you have any questions or need assistance, feel free to reach out to our support team.</p>\r\n        <p>Omar<br>SkyLink Team</p>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</body>\r\n</html>\r\n";
                    // Send email confirmation link
                    await _emailService.SendEmailAsync(userDTO.Email, "Email Confiramtion", mailBody, true);

                    //----------------------------------

                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = null,
                        Message = "Account Created Successfully and Confiramtion mail has been sent"
                    };
                }
                else
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = createAccResult.Errors,
                        Message = "Couldn't create Account due to these errors"
                    };
                }
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = ModelState,
                    Message = "Invalid Model State"
                };
            }
        }

        [HttpGet("confirm-email")]
        public async Task<ActionResult<GeneralResponse>> ConfirmEmail(string userId, string token)
        {
            int maxRetryAttempts = 3;
            int retryAttempt = 0;

            while (retryAttempt < maxRetryAttempts)
            {
                try
                {
                    if (userId == null || token == null)
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Data = null,
                            Message = "User ID or token is null"
                        };
                    }

                    var user = await _userManager.FindByIdAsync(userId);

                    if (user == null)
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Data = null,
                            Message = $"Unable to load user with ID '{userId}'."
                        };
                    }

                    //  Validates that an email confirmation token matches the specified user.
                    var result = await _userManager.ConfirmEmailAsync(user, token);

                    if (result.Succeeded)
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = true,
                            Data = null,
                            Message = "Email confirmed successfully"
                        };
                    }
                    else
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Data = result.Errors,
                            Message = "Error confirming email"
                        };
                    }
                }
                catch (Exception ex)
                {
                    // Handle concurrency exception
                    // Log the exception for debugging purposes
                    // Optionally, wait for a short duration before retrying
                    await Task.Delay(TimeSpan.FromSeconds(1)); // Wait for 1 second before retrying
                    retryAttempt++;
                }
            }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "Error confirming email after multiple retries"
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<GeneralResponse>> Login(LoginUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUSer? userFromDB = await _userManager.FindByNameAsync(userDTO.UserName);

                if (userFromDB == null)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "can't find this user name"
                    };
                }
                else if (!userFromDB.EmailConfirmed)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "this user Email is not confirmed"
                    };
                }
                else
                {
                    bool IsPasswordMatched = await _userManager.CheckPasswordAsync(userFromDB, userDTO.Password);

                    if (IsPasswordMatched)
                    {
                        // create token steps : 
                        List<Claim> myClaims = new List<Claim>();
                        myClaims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName ?? "Not Available"));
                        myClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                        //myClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // if u want for the same user => his token be unique for each login => uncomment this

                        // claim roles
                        IList<string> roles = await _userManager.GetRolesAsync(userFromDB);

                        foreach (string role in roles)
                        {
                            myClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        // security key 
                        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

                        // in the JWT header =>  credentials : key + ALgorithm
                        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        // in the JWT payload => JwtSecurityToken is a class that design the token
                        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                            (
                            issuer: _configuration["JWT:ValidIss"], // the povider API who is responsible for creating the token
                            audience: _configuration["JWT:ValidAud"],  // the consumer (React domain)
                            expires: DateTime.Now.AddHours(1),
                            claims: myClaims,
                            signingCredentials: signingCredentials
                            );

                        //return the token
                        return new GeneralResponse()
                        {
                            IsSuccess = true,
                            Data = null,
                            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                            Expired = jwtSecurityToken.ValidTo,
                            Message = "Token Created Successfully"
                        };
                    }
                    else
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Data = null,
                            Message = "Wrong Password"
                        };
                    }
                }
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = ModelState,
                    Message = "Invalid Model State"
                };
            }
        }
    }
}
