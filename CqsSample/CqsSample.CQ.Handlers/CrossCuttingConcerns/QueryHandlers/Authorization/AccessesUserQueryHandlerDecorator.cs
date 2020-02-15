using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CqsSample.Authorization.AccessControl;
using CqsSample.CQ.Contract.Common;
using softaware.Cqs;

namespace CqsSample.CQ.Handlers.CrossCuttingConcerns.QueryHandlers.Authorization
{
    internal class AccessesUserQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>, IAccessesUser
    {
        private readonly IAccessControlService accessControlService;
        private readonly IQueryHandler<TQuery, TResult> decoratee;

        public AccessesUserQueryHandlerDecorator(
            IAccessControlService accessControlService,
            IQueryHandler<TQuery, TResult> decoratee)
        {
            this.accessControlService = accessControlService;
            this.decoratee = decoratee;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            await this.accessControlService.EnsureHasAccessToUserAsync(query.UserId);
            return await this.decoratee.HandleAsync(query);
        }
    }
}
