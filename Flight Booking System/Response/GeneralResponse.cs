namespace Flight_Booking_System.Response
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }

        // Omar : made it dynamic not generic so that if was case has to return differnt types in the same controller based on some condition
        public dynamic? Data { get; set; }  // Omar :  u can use it to send failer message if failed.. or to send the DTO if success or to send the object itself when added or edited

        public string? Message { get; set; } = string.Empty; // Omar : use it if u want to add any notes to the consumer in case success with notes or add or edit or delete
    }
}