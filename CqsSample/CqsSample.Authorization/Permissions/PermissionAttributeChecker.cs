using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqsSample.Authorization.Permissions
{
    public class PermissionAttributeChecker<TMessage>
    {
        private static readonly Guid[] PermissionIds;
        private readonly IPermissionsService permissionsService;

        /// <summary>
        /// Since the check for the [PermissionAttribute] is done in the static constructor, the InvalidOperationException is thrown on startup of the application
        /// when the decorators are initialized by SimpleInjector. This means, that the applications fails immediatly on startup if a developer forgot to add
        /// the [PermissionAttribute] on a query or command.
        /// </summary>
        static PermissionAttributeChecker()
        {
            var tMessageType = typeof(TMessage);

            var permissionAttribute = tMessageType
                .GetCustomAttributes(typeof(PermissionAttribute), inherit: false)
                .OfType<PermissionAttribute>()
                .SingleOrDefault();

            if (permissionAttribute == null)
            {
                throw new InvalidOperationException($"The type {typeof(TMessage).FullName} is not marked with the [{typeof(PermissionAttribute).Name}]. " +
                                                    $"Please define the permission the user needs to execute this query.");
            }

            PermissionIds = permissionAttribute?.PermissionIds ?? new Guid[0];
        }

        public PermissionAttributeChecker(IPermissionsService permissionsService)
        {
            this.permissionsService = permissionsService;
        }

        public Task CheckPermissionForCurrentUser()
        {
            return this.permissionsService.CheckPermissionAsync(PermissionIds);
        }
    }
}
