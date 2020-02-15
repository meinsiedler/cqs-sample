using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqsSample.Authorization.Permissions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PermissionAttribute : Attribute
    {
        public PermissionAttribute(params string[] permissionIds)
        {
            this.PermissionIds = permissionIds.Select(p => Guid.Parse(p)).ToArray();
        }

        public Guid[] PermissionIds { get; set; }
    }
}
