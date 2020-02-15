using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CqsSample.Domain;
using CqsSample.Domain.Exceptions;

namespace CqsSample.Authorization.Permissions
{
    internal class PermissionsService : IPermissionsService
    {
        /// <summary>
        /// Permissions for all users.
        /// </summary>
        private readonly string[] regularUserPermissions = new string[]
        {
            Permissions.User.Get
        };

        /// <summary>
        /// Permissions for admins. Permissions for regular users are included too.
        /// </summary>
        private readonly string[] adminUserPermissions = new string[]
        {
            Permissions.User.AddOrUpdate
        };

        private readonly IPrincipal principal;

        public PermissionsService(IPrincipal principal)
        {
            this.principal = principal;
        }

        public async Task CheckPermissionAsync(Guid[] permissionIds)
        {
            if (permissionIds == null)
            {
                throw new ArgumentNullException(nameof(permissionIds));
            }

            if (!permissionIds.Any())
            {
                throw new ForbiddenException("Trying to call a message as user without a specified permission.");
            }

            var permissionsForUser = await this.GetPermissionsForCurrentUserAsync();
            if (!permissionsForUser.Any(p => permissionIds.Contains(p)))
            {
                throw new ForbiddenException(
                    $"User with Name \"{this.principal.Identity.Name}\" does not have one of the required permissions [{string.Join(",", permissionIds)}] to execute this action.");
            }
        }

        private async Task<IEnumerable<Guid>> GetPermissionsForCurrentUserAsync()
        {
            // Usually, we'd access our data store here and receive further user information so that we can decide which permissions the user has.
            // We use some unique identification for the user, which is stored in the IPrincipal.

            // We could be as complex as we want. For example we could even load the permissions which are assigned to a user from the database.
            // This would allow dynamic configuration of permissions for users via the UI for example.

            var permissions = ParseGuids(this.regularUserPermissions);

            if (this.principal.Identity.GetUserRole() == UserRole.Admin)
            {
                permissions = permissions.Union(ParseGuids(this.adminUserPermissions)).ToList();
            }

            return permissions;
        }

        private static IList<Guid> ParseGuids(params string[] strGuids)
        {
            return strGuids.Select(s => Guid.Parse(s)).ToList();
        }
    }
}
