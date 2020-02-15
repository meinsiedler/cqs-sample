using System;
using System.Collections.Generic;
using System.Text;
using CqsSample.Authorization.Permissions;
using CqsSample.CQ.Contract.Queries.User.DTOs;
using softaware.Cqs;

namespace CqsSample.CQ.Contract.Queries.User
{
    /// <summary>
    /// Here, we return a specific DTO for the list of users (<see cref="UserListEntryDto"/>), since we don't want to return all
    /// properties a user has when querying a list of users.
    /// 
    /// With the approach that each query returns it's own DTOs, we have the maximal flexibility of returning customized DTOs for each use case
    /// and we don't break any other queries. Queries can evolve independently from each other.
    /// </summary>
    [Permission(Permissions.User.Get)]
    public class GetUsers : IQuery<IReadOnlyCollection<UserListEntryDto>>
    {
    }
}
