using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CqsSample.CQ.Contract.Queries.User;
using CqsSample.CQ.Contract.Queries.User.DTOs;
using softaware.Cqs;

namespace CqsSample.CQ.Handlers.QueryHandlers.User
{
    internal class GetUsersHandler : IQueryHandler<GetUsers, IReadOnlyCollection<UserListEntryDto>>
    {
        public async Task<IReadOnlyCollection<UserListEntryDto>> HandleAsync(GetUsers query)
        {
            var users = new List<UserListEntryDto>
            {
                new UserListEntryDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Sherlock",
                    LastName = "Holmes"
                },
                new UserListEntryDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Watson"
                },
                new UserListEntryDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "James",
                    LastName = "Moriarty"
                }
            };

            return users;
        }
    }
}
