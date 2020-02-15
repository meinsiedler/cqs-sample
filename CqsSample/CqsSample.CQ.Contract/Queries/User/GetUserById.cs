using System;
using System.Collections.Generic;
using System.Text;
using CqsSample.CQ.Contract.Queries.User.DTOs;
using softaware.Cqs;

namespace CqsSample.CQ.Contract.Queries.User
{
    public class GetUserById : IQuery<UserDto>
    {
        public GetUserById(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}
