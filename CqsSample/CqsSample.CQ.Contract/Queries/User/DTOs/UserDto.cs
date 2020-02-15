using System;
using System.Collections.Generic;
using System.Text;
using CqsSample.Domain;

namespace CqsSample.CQ.Contract.Queries.User.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public UserRole Role { get; set; }
    }
}
