using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using CqsSample.Domain;

namespace CqsSample.Authorization
{
    public static class IdentityExtensions
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            // Since we used the simple basic auth, the UserId is stored in the Identity.Name property.
            // Usually, we'd access the UserId by storing a custom claim in the principal.
            var id = identity.Name;
            return Guid.Parse(id);
        }

        public static UserRole GetUserRole(this IIdentity identity)
        {
            // In this case and just for simplicity and for demo purposes, we hard-code the user role here.
            // We say, that user with id 00000000-0000-0000-0000-000000000001 is the admin and all other users are regular users.
            // Usually, the role would be stored as claim in the identity. The claim would be set when user loggs in.

            if (identity.GetUserId() == new Guid("00000000-0000-0000-0000-000000000001"))
            {
                return UserRole.Admin;
            }

            return UserRole.Regular;
        }
    }
}
