using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CqsSample.Authorization.AccessControl
{
    public interface IAccessControlService
    {
        Task EnsureHasAccessToUserAsync(Guid userId);
    }
}
