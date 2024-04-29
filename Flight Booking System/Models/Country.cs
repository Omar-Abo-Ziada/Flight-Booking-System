using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Country Name is required")]
        public string Name { get; set; }

        //---------------------------------------

        public List<State>? States { get; set; }
    }
}
