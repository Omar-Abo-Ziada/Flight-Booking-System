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

        public AccountController(UserManager<ApplicationUSer> userManager, IConfiguration configuration, IEmailService emailService,
             IGoogleAuthService _googleAuthService)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            _emailService = emailService;
            googleAuthService = _googleAuthService;
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

                // create Account in database
                IdentityResult createAccResult = await _userManager.CreateAsync(user, userDTO.Password);

                if (createAccResult.Succeeded)
                {
                    // Generate email confirmation token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                    // Send email confirmation link
                    await _emailService.SendEmailAsync(userDTO.Email, "Email Confiramtion", $"Please confirm your email by clicking here: <a href='{confirmationLink}'>link</a>", true);

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

    }
}
