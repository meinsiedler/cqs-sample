using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CqsSample.Authorization.Permissions;
using softaware.Cqs;

namespace CqsSample.CQ.Handlers.CrossCuttingConcerns.QueryHandlers.Authorization
{
    internal class PermissionQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly PermissionAttributeChecker<TQuery> permissionAttributeChecker;
        private readonly IQueryHandler<TQuery, TResult> decoratee;

        public PermissionQueryHandlerDecorator(
            PermissionAttributeChecker<TQuery> permissionAttributeChecker,
            IQueryHandler<TQuery, TResult> decoratee)
        {
            this.permissionAttributeChecker = permissionAttributeChecker;
            this.decoratee = decoratee;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            await this.permissionAttributeChecker.CheckPermissionForCurrentUser();
            return await this.decoratee.HandleAsync(query);
        }
    }
}
