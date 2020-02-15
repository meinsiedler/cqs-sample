using System;
using System.Collections.Generic;
using System.Text;

namespace CqsSample.CQ.Contract.Queries.User.DTOs
{
    public class UserListEntryDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
