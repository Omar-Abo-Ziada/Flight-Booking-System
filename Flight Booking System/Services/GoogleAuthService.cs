using Flight_Booking_System.Context;
using Flight_Booking_System.DTOs;
using Flight_Booking_System.ExternalLogin;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Google;
using Google.Apis.Auth;
using log4net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Flight_Booking_System.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly UserManager<ApplicationUSer> _userManager;
        private readonly ITIContext _context;
        private readonly GoogleAuthConfig _googleAuthConfig;
        //  private readonly ILog _logger;
        private readonly IConfiguration _configuration;
        //   private readonly PassengerRepository _passengerRepository;


        public GoogleAuthService(
            UserManager<ApplicationUSer> userManager,
            ITIContext context,
            IOptions<GoogleAuthConfig> googleAuthConfig,
             IConfiguration configuration
            //   PassengerRepository passengerRepository
            )
        {
            _userManager = userManager;
            _context = context;
            _googleAuthConfig = googleAuthConfig.Value;
            //  _logger = LogManager.GetLogger(typeof(GoogleAuthService));
            _configuration = configuration;
            //  _passengerRepository = passengerRepository;
        }

        public async Task<GeneralResponse> GoogleSignIn(GoogleSignInDTO model) 
        {
            ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new string[] { _googleAuthConfig.ClientId }
            };

            Payload payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings); // validate that aud of token matches clienId of my project on google cloud api
            if (payload == null)
            {
                return new GeneralResponse
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Google login failed"
                };
            }

            List<Claim> myClaims = new List<Claim>();
            myClaims.Add(new Claim(ClaimTypes.Email, payload.Email));
            myClaims.Add(new Claim(ClaimTypes.Name, payload.Name));


            // security key 
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            // in the JWT header =>  credentials : key + ALgorithm
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // in the JWT payload => JwtSecurityToken is a class that design the token
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken    // design token
                (
                issuer: _configuration["JWT:ValidIss"], // the povider API who is responsible for creating the token
                audience: _configuration["JWT:ValidAud"],  // the consumer (React domain)
                expires: DateTime.Now.AddHours(1),
                claims: myClaims,                       // how access claims in front??? use data sent in general response
                signingCredentials: signingCredentials
                );


            // needed????????? + err on adding passenger repo ??????

            //Passenger passenger = new Passenger
            //{
            //    Name = payload.Name,
            //};
            //_passengerRepository.Insert( passenger ); 
            //_passengerRepository.Save();

            //return the token
            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = new { name = payload.Name, email = payload.Email },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),  // create token
                Expired = jwtSecurityToken.ValidTo,
                Message = "successful external login"
            };

        }
    }
}



