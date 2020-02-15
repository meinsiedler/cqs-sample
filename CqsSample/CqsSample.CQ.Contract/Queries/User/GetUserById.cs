using System;
using System.Collections.Generic;
using System.Text;
using CqsSample.Authorization.Permissions;
using CqsSample.CQ.Contract.Common;
using CqsSample.CQ.Contract.Queries.User.DTOs;
using softaware.Cqs;

namespace CqsSample.CQ.Contract.Queries.User
{
    [Permission(Permissions.User.Get)]
    public class GetUserById : IQuery<UserDto>, IAccessesUser
    {
        public GetUserById(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}
