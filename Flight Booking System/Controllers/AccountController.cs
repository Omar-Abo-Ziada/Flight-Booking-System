using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Flight_Booking_System.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUSer> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IGoogleAuthService googleAuthService;
        private readonly IPassengerRepository _passengerRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IPhoneConfirmationService phoneConfirmationService;


        public AccountController(UserManager<ApplicationUSer> userManager, IConfiguration configuration, IEmailService emailService,
             IGoogleAuthService _googleAuthService, IPassengerRepository passengerRepository,
             ITicketRepository ticketRepository, IFlightRepository flightRepository, ISeatRepository seatRepository, IPhoneConfirmationService phoneConfirmationService)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            _emailService = emailService;
            googleAuthService = _googleAuthService;
            this._passengerRepository = passengerRepository;
            this._ticketRepository = ticketRepository;
            this._flightRepository = flightRepository;
            this._seatRepository = seatRepository;
            this.phoneConfirmationService = phoneConfirmationService;

        }

        [HttpPost("register")]
        public async Task<ActionResult<GeneralResponse>> Register(RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
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

                    Ticket = null,
                };

                _passengerRepository.Insert(userPassenger);

                _passengerRepository.Save();

                ApplicationUSer user = new ApplicationUSer()
                {
                    UserName = userDTO.UserName,
                    PasswordHash = userDTO.Password,
                    Email = userDTO.Email,
                    PhoneNumber = userDTO.PhoneNumber,

                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,

                    Passenger = userPassenger,
                    PassengerId = userPassenger.Id,
                };

                userPassenger.User = user;


                // create Account in database
                IdentityResult createAccResult = await _userManager.CreateAsync(user, userDTO.Password);

                if (createAccResult.Succeeded)
                {
                    // Adding user Role by default
                    IdentityResult addRoleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (!addRoleResult.Succeeded)
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Message = "Couldn't Add the default Role to this user ,, check that you already added Roles first"
                        };
                    }

                    // Generate email confirmation token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                    string mailBody = $"<!DOCTYPE html>\r\n<html>\r\n<head>\r\n  <title>Email Confirmation</title>\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n  <style>\r\n    body {{\r\n      background: #f9f9f9;\r\n      margin: 0;\r\n      padding: 0;\r\n    }}\r\n    .container {{\r\n      max-width: 640px;\r\n      margin: 0 auto;\r\n      background: #ffffff;\r\n      box-shadow: 0px 1px 5px rgba(0, 0, 0, 0.1);\r\n      border-radius: 4px;\r\n      overflow: hidden;\r\n    }}\r\n  </style>\r\n</head>\r\n<body>\r\n  <div class=\"container\">\r\n    <div style=\"background-color: #7289da; padding: 57px; text-align: center;\">\r\n      <div style=\"cursor: auto; color: white; font-family: Arial, sans-serif; font-size: 36px; font-weight: 600;\">\r\n        Welcome to SkyLink!\r\n      </div>\r\n    </div>\r\n    \r\n    <div style=\"padding: 40px 70px;\">\r\n      <div style=\"color: #737f8d; font-family: Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n        <h2 style=\"font-weight: 500; font-size: 20px; color: #4f545c;\">Hey {user.UserName},</h2>\r\n        <p>\r\n          Welcome aboard SkyLink! 🚀 Thanks for signing up! We're thrilled to have you join our community.\r\n        </p>\r\n        <p>\r\n          To get started, we just need to confirm your email address to ensure everything runs smoothly.\r\n        </p>\r\n        <p>\r\n          Click the button below to verify your email and unlock all the amazing features SkyLink has to offer:\r\n        </p>\r\n      </div>\r\n      <div style=\"text-align: center; padding: 20px;\">\r\n        <a href=\"{confirmationLink}\" style=\"display: inline-block; background-color: #7289da; color: white; text-decoration: none; padding: 15px 30px; border-radius: 3px;\">Verify Email</a>\r\n      </div>\r\n      <div style=\"color: #737f8d; font-family: Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n        <p>If you have any questions or need assistance, feel free to reach out to our support team.</p>\r\n        <p>Omar<br>SkyLink Team</p>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</body>\r\n</html>\r\n";
                    // Send email confirmation link
                    await _emailService.SendEmailAsync(userDTO.Email, "Email Confiramtion", mailBody, true);

                    //----------------------------------

                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = userPassenger.Id,
                        Message = "Account Created Successfully and Confiramtion mail has been sent , " +
                        "and there is the Passenger ID => save it and send it when he wants to Add a ticket"
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

                        //  security key 
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


        // saeed : google login 
        [HttpPost("googleLogin")]
        public async Task<ActionResult<GeneralResponse>> googleLogin()
        {
            GoogleSignInDTO googleSignInDTO = new GoogleSignInDTO()
            {
                IdToken = Request.Headers["IdToken"]
            };

            // string idToken = Request.Headers["Authorization"];
            //googleSignInDTO.IdToken = idToken;
            return await googleAuthService.GoogleSignIn(googleSignInDTO);
        }

        //ibrahim forget password

        [HttpPost("ChnagePassword")]
        public async Task<ActionResult<GeneralResponse>> ChnagePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }
            else
            {
                var resulst = await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);

                if (!resulst.Succeeded)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Message = $"{resulst.Errors}"
                    };
                }
                else
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Message = "password changed sucessfully"
                    };
                }
            }
        }

        [HttpDelete]
        public async Task<ActionResult<GeneralResponse>> DeleteUser(string userName)
        {
            ApplicationUSer? user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Message = "There is no user with this  username ."
                };
            }

            #region deleting user references first if found

            //Passenger passenger = _passengerRepository.GetById(user.PassengerId);
            Passenger? passenger = _passengerRepository.GetWithTicket(user.PassengerId);

            if (passenger != null)
            {

                // checking if there is a flight realted to this passenger 
                Flight? flight = _flightRepository.GetWithPlane_Passengers(passenger.FlightId);

                if (flight != null)
                {
                    // checking if there is a flight contains this passenger on it 
                    if (flight.Passengers.Contains(passenger))
                    {
                        flight.Passengers.Remove(passenger);
                    }

                    // checking if there is a ticket related to this passenger
                    Ticket? ticket = _ticketRepository.GetWithSeat_Passenger(passenger?.Ticket?.Id);

                    if (ticket != null)
                    {
                        // checking if there is a flight contains this ticket on it 
                        if (flight.Tickets.Contains(ticket))
                        {
                            flight.Tickets.Remove(ticket);
                        }

                        ticket.Passenger = null;
                        ticket.PassengerId = null;

                        ticket.FlightId = null;
                        ticket.Flight = null;

                        if (ticket.Seat != null)
                        {
                            Seat? seat = _seatRepository.GetWithTicket(ticket.Seat.Id);

                            if (seat != null)
                            {
                                seat.Ticket = null;
                                seat.TicketId = null;

                                try
                                {
                                    _seatRepository.Delete(seat);

                                    _seatRepository.Save();

                                    ticket.Seat = null;
                                }
                                catch (Exception ex)
                                {
                                    return new GeneralResponse()
                                    {
                                        IsSuccess = false,
                                        Data = ex.Message,
                                        Message = "Couldn't delete the seat related to this passenger"
                                    };
                                }
                            }
                        }

                        try
                        {
                            _ticketRepository.Delete(ticket);

                            _ticketRepository.Save();
                        }
                        catch (Exception ex)
                        {
                            return new GeneralResponse()
                            {
                                IsSuccess = false,
                                Data = ex.Message,
                                Message = "Couldn't delete the ticket related to this passenger "
                            };
                        }
                    }
                }

                passenger.User = null;

                passenger.Flight = null;
                passenger.FlightId = null;

                passenger.Ticket = null;

                try
                {
                    _passengerRepository.Delete(passenger);

                    _passengerRepository.Save();
                }
                catch (Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = ex.Message,
                        Message = "Couldn't delete the passenger due to this error"
                    };
                }
            }

            #endregion

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                // don't forget to remove his role first
                IdentityResult removeRoleResult = await _userManager.RemoveFromRoleAsync(user, role);

                if (!removeRoleResult.Succeeded)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Message = $"Couldn't Remove the ({role}) role from the user before deleting the user"
                    };
                }
            }

            IdentityResult deleteResult = await _userManager.DeleteAsync(user);

            if (deleteResult.Succeeded)
            {
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Message = $"the user : {user.UserName} deleted successfully ."
                };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Message = $"Failed to delete the user : {user.UserName}"
                };
            }
        }

        //ibraihim send smsm(PhoneConfirmationService)from(ibrahim(sendSMS) branch)

        [HttpPost("PhoneConfirmation_Sendsms")]
        public ActionResult<GeneralResponse> PhoneConfirmation_Sendsms(PhoneConfirmationDto phoneConfirmationDto)
        {
            var result = phoneConfirmationService.SendVerificationCode(phoneConfirmationDto.PhoneNumber, phoneConfirmationDto.Body);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Message = $"{result.ErrorMessage}"
                };
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Message = $"confirmation sended"
            };
        }
    }
}
