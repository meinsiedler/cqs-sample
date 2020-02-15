using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CqsSample.Authorization.Permissions;
using softaware.Cqs;

namespace CqsSample.CQ.Handlers.CrossCuttingConcerns.CommandHandlers.Authorization
{
    internal class PermissionCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly PermissionAttributeChecker<TCommand> permissionAttributeChecker;
        private readonly ICommandHandler<TCommand> decoratee;

        public PermissionCommandHandlerDecorator(
            PermissionAttributeChecker<TCommand> permissionAttributeChecker,
            ICommandHandler<TCommand> decoratee)
        {
            this.permissionAttributeChecker = permissionAttributeChecker;
            this.decoratee = decoratee;
        }

        public async Task HandleAsync(TCommand command)
        {
            await this.permissionAttributeChecker.CheckPermissionForCurrentUser();
            await this.decoratee.HandleAsync(command);
        }
    }
}
