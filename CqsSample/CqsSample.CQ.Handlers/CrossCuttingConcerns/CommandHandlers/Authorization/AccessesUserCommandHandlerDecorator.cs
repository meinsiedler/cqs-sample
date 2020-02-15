using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CqsSample.Authorization.AccessControl;
using CqsSample.CQ.Contract.Common;
using softaware.Cqs;

namespace CqsSample.CQ.Handlers.CrossCuttingConcerns.CommandHandlers.Authorization
{
    internal class AccessesUserCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand, IAccessesUser
    {
        private readonly IAccessControlService accessControlService;
        private readonly ICommandHandler<TCommand> decoratee;

        public AccessesUserCommandHandlerDecorator(
            IAccessControlService accessControlService,
            ICommandHandler<TCommand> decoratee)
        {
            this.accessControlService = accessControlService ?? throw new ArgumentNullException(nameof(accessControlService));
            this.decoratee = decoratee ?? throw new ArgumentNullException(nameof(decoratee));
        }

        public async Task HandleAsync(TCommand command)
        {
            await this.accessControlService.EnsureHasAccessToUserAsync(command.UserId);
            await this.decoratee.HandleAsync(command);
        }
    }
}
