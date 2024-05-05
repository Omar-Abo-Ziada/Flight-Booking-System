using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Response;
using Flight_Booking_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Infobip.Api.Client;
using System.Net.Mail;
using RestSharp;
using Infobip.Api.Client.Model;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUSer> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUSer> userManager, IConfiguration configuration, IEmailService emailService)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            _emailService = emailService;
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

                    EmailConfirmed =false,
                    PhoneNumberConfirmed = false,
                };

                // create Account in database
                IdentityResult createAccResult = await _userManager.CreateAsync(user, userDTO.Password );

                if (createAccResult.Succeeded)
                {
                    // Generate email confirmation token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                    // Send email confirmation link
                    await _emailService.SendEmailAsync(userDTO.Email, "Email Confiramtion" , $"Please confirm your email by clicking here: <a href='{confirmationLink}'>link</a>", true);

                    #region Phone confiramtion (not done yet)
                    // // Generate phone number confirmation token
                    // var phoneToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, userDTO.PhoneNumber);

                    // // Send phone number confirmation token via SMS or other means
                    // var infobipClientOptions = new RestClientOptions("https://qy2pew.api.infobip.com")
                    // {
                    //     MaxTimeout = -1,
                    // };
                    // var infobipClient = new RestClient(infobipClientOptions);

                    // var infobipRequest = new RestRequest("/2fa/2/applications", RestSharp.Method.Get);

                    // infobipRequest.AddHeader("Authorization", "{authorization}"); // Replace {authorization} with your actual authorization token
                    // infobipRequest.AddHeader("Accept", "application/json");
                    // var infobipResponse = await infobipClient.ExecuteAsync(infobipRequest);
                    // // Log the response content
                    // // Example using Serilog: Log.Information(infobipResponse.Content);

                    // //----------------------------------

                    // var infobipApiBaseUrl = _configuration["Infobip:ApiBaseUrl"];
                    // var qy2pewApiBaseUrl = _configuration["Infobip:Qy2pewApiBaseUrl"];
                    // var apiKey = _configuration["Infobip:ApiKey"];
                    // var apiKeyPrefix = _configuration["Infobip:ApiKeyPrefix"];
                    // var userAgent = _configuration["Infobip:UserAgent"];

                    // // Initialize Infobip SMS client
                    // var infobipConfiguration = new Configuration() 
                    // { 
                    //     BasePath = infobipApiBaseUrl ,
                    //     ApiKey = apiKey ,
                    //     //UserAgent = userAgent ,
                    //     //ApiKeyPrefix = apiKeyPrefix ,
                    // };

                    // var infobipSmsClient = new SmsClient(infobipConfiguration);

                    // // Create SMS message
                    // var smsTextualRequest = new SmsTextualRequest
                    // {
                    //     From = "SenderName", // Your sender name or number
                    //     To = userDTO.PhoneNumber, // Recipient phone number
                    //     Text = "Your confirmation code is: " + phoneToken // Your confirmation message
                    // };

                    // //Send SMS message
                    //var smsResponse = await infobipSmsClient.SendSmsTextualAsync(smsTextualRequest);

                    // //Handle response(e.g., check if the message was sent successfully)
                    // if (smsResponse != null && smsResponse.Messages != null && smsResponse.Messages.Count > 0)
                    // {
                    //     // SMS sent successfully
                    //     return new GeneralResponse()
                    //     {
                    //         IsSuccess = true,
                    //         Data = smsResponse.Messages,
                    //         Message = "SMS Sent Successfully"
                    //     };
                    // }
                    // else
                    // {
                    //     // Failed to send SMS
                    //     return new GeneralResponse()
                    //     {
                    //         IsSuccess = false,
                    //         Data = null,
                    //         Message = "Failed to send SMS"
                    //     };
                    // } 
                    #endregion

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
                catch(Exception ex)
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
                else if(!userFromDB.EmailConfirmed)
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
                        myClaims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName??"Not Available"));
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
                            issuer: _configuration["JWT:ValidIss"] , // the povider API who is responsible for creating the token
                            audience: _configuration["JWT:ValidAud"],  // the consumer (React domain)
                            expires: DateTime.Now.AddHours(1),
                            claims: myClaims ,
                            signingCredentials: signingCredentials
                            );

                        //return the token
                        return new GeneralResponse()
                        {
                            IsSuccess = true ,
                            Data = null ,
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
