﻿using System.IdentityModel.Tokens.Jwt;

namespace Flight_Booking_System.Response
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Omar : made it dynamic not generic so that if was case has to return differnt types in the same controller based on some condition
        /// Omar :  u can use it to send failer message if failed.. or to send the DTO if success or to send the object itself when added or edited
        /// </summary>
        public dynamic? Data { get; set; }  

        /// <summary>
        /// Omar : use it if u want to add any notes to the consumer in case success with notes or add or edit or delete
        /// </summary>
        public string? Message { get; set; } = string.Empty;

        public string? Token { get; set; } = null;

        public DateTime? Expired { get; set; } = null;
    }
}