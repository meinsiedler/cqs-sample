using System;
using System.Collections.Generic;
using System.Text;
using CqsSample.CQ.Contract.Queries.User.DTOs;
using softaware.Cqs;

namespace CqsSample.CQ.Contract.Queries.User
{
    public class GetUsers : IQuery<IReadOnlyCollection<UserListEntryDto>>
    {
    }
}
