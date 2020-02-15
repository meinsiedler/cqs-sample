using System;
using System.Collections.Generic;
using System.Text;

namespace CqsSample.CQ.Contract.Common
{
    public interface IAccessesUser
    {
        Guid UserId { get; }
    }
}
