using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CqsSample.CQ.Contract.Queries.User;
using CqsSample.CQ.Contract.Queries.User.DTOs;
using softaware.Cqs;

namespace CqsSample.CQ.Handlers.QueryHandlers.User
{
    internal class GetUserByIdHandler : IQueryHandler<GetUserById, UserDto>
    {
        public async Task<UserDto> HandleAsync(GetUserById query)
        {
            return new UserDto
            {
                Id = query.UserId,
                Email = "sherlock@holmes.com",
                FirstName = "Sherlock",
                LastName = "Holmes",
                Street = "221B Baker Street",
                City = "London",
                Role = Domain.UserRole.Admin
            };
        }
    }
}
