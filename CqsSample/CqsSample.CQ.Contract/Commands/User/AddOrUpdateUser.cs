using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CqsSample.Authorization.Permissions;
using CqsSample.Domain;
using softaware.Cqs;

namespace CqsSample.CQ.Contract.Commands.User
{
    [Permission(Permissions.User.AddOrUpdate)]
    public class AddOrUpdateUser : ICommand
    {
        public Guid? Id { get; set; }

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
