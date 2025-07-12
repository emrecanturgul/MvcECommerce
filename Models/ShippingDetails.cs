using System.ComponentModel.DataAnnotations;

namespace MvcECommerce.Models
{
    public class ShippingDetails
    {
        public string Username { get; set; }    
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter an address name")]

        public string AddressName { get; set; }

        [Required(ErrorMessage = "Please enter the full address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter the city")]
        public string City { get; set; }
        

        [Required(ErrorMessage = "Please enter the district")]
        public string District { get; set; }

        [Required(ErrorMessage = "Please enter the neighborhood")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "Please enter the zip code")]
        public string ZipCode { get; set; }
    }
}
