using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFDataAccessLibrary.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        public int Number { get; set; }

        [Required]
        [MaxLength(200)]
        public string Neighborhood { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)] //SP
        public string State { get; set; }

        [Required]
        [MaxLength(10)] //14340-0000
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Complement { get; set; }

        [Required]
        [MaxLength(50)]
        public string Telephone { get; set; }
    }
}
