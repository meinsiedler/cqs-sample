using System;
using System.ComponentModel.DataAnnotations;

namespace CqsSample.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Street { get; set; }

        [MaxLength(200)]
        public string City { get; set; }

        public UserRole Role { get; set; }
    }
}
