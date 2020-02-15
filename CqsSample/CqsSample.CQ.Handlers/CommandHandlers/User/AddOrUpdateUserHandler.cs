using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CqsSample.CQ.Contract.Commands.User;
using softaware.Cqs;

namespace CqsSample.CQ.Handlers.CommandHandlers.User
{
    internal class AddOrUpdateUserHandler: ICommandHandler<AddOrUpdateUser>
    {
        public Task HandleAsync(AddOrUpdateUser command)
        {
            if (command.Id == null)
            {
                command.Id = Guid.NewGuid();
                // We know it's a new user: create user
                // TODO: create user
                
            }
            else
            {
                // We know it's an already existing user: update user
            }

            return Task.CompletedTask;
        }
    }
}
