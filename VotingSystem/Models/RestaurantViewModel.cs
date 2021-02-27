using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Models
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Maximum limit of 200 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Maximum limit of 200 characters")]
        public string Address { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Maximum limit of 200 characters")]
        public string Neighborhood { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum limit of 100 characters")]
        public string City { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Maximum limit of 2 characters")]
        public string State { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Maximum limit of 10 characters")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Maximum limit of 200 characters")]
        public string Complement { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum limit of 50 characters")]
        public string Telephone { get; set; }
    }
}
