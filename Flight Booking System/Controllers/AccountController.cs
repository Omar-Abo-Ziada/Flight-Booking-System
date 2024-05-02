using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUSer> userManager;

        public AccountController(UserManager<ApplicationUSer> userManager)
        {
            this.userManager = userManager;
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
                };

                // create Account in database
                IdentityResult createAccResult = await userManager.CreateAsync(user, userDTO.Password);

                if (createAccResult.Succeeded)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = null,
                        Message = "Account Created Successfully"
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

        [HttpPost("login")]
        public async Task<ActionResult<GeneralResponse>> Login(LoginUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUSer? userFromDB = await userManager.FindByNameAsync(userDTO.UserName);

                if (userFromDB == null)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "can't find this user name"
                    };
                }
                else
                {
                    bool IsPasswordMatched = await userManager.CheckPasswordAsync(userFromDB, userDTO.Password);

                    if (IsPasswordMatched)
                    {
                        // create token steps : 
                        List<Claim> myClaims = new List<Claim>();
                        myClaims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));
                        myClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                        //myClaims.Add(new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())); // if u want for the same user => his token be unique for each login => uncomment this

                        // claim roles
                        IList<string> roles = await userManager.GetRolesAsync(userFromDB);

                        foreach (string role in roles)
                        {
                            myClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        // security key 
                        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ASDPKfsdomfASD@#$SDpsdja2dspe#$DSF,SADlpsd<DPFSwq,xfv,ofsfa"));

                        // credentials : key + ALgorithm
                        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                         

                        // JwtSecurityToken is a class that design the token
                        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                            (
                            issuer: "http://localhost:40640/", // the povider API who is responsible for creating the token
                            audience: "http://localhost:3000/",  // the consumer (React domain)
                            expires: DateTime.Now.AddHours(1),
                            claims: myClaims ,
                            signingCredentials: null 
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
