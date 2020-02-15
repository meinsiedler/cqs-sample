using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CqsSample.Domain.Exceptions;

namespace CqsSample.Authorization.AccessControl
{
    internal class AccessControlService : IAccessControlService
    {
        private readonly IPrincipal principal;

        public AccessControlService(IPrincipal principal)
        {
            this.principal = principal ?? throw new ArgumentNullException(nameof(principal));
        }

        public async Task EnsureHasAccessToUserAsync(Guid userId)
        {
            // Let's say that admin users have access to all other users.
            // Regular users are only allowed to access themselfs.

            // Again, we can get as complex as we want here in this access checks.

            if (this.principal.Identity.GetUserRole() == Domain.UserRole.Admin)
            {
                return;
            }

            if (this.principal.Identity.GetUserId() == userId)
            {
                return;
            }

            throw new ForbiddenException($"User is not allowed to access user with id {userId}.");
        }
    }
}
