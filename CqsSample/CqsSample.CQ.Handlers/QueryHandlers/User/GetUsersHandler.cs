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
                    Id = new Guid("00000000-0000-0000-0000-000000000001"), // Just for testing, usually, we would generate the Guid when creating the user with Guid.NewGuid().
                    Email = "sherlock@holmes.com",
                    FirstName = "Sherlock",
                    LastName = "Holmes"
                },
                new UserListEntryDto
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"), // Just for testing, usually, we would generate the Guid when creating the user with Guid.NewGuid().
                    Email = "john@watson.com",
                    FirstName = "John",
                    LastName = "Watson"
                },
                new UserListEntryDto
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"), // Just for testing, usually, we would generate the Guid when creating the user with Guid.NewGuid().
                    FirstName = "James",
                    Email = "james@moriarty.com",
                    LastName = "Moriarty"
                }
            };

            return users;
        }
    }
}
