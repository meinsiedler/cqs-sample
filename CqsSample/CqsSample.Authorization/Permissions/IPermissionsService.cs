using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CqsSample.Authorization.Permissions
{
    public interface IPermissionsService
    {
        Task CheckPermissionAsync(Guid[] permissionIds);
    }
}
