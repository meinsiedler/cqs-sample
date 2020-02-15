using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqsSample.CQ.Contract.Commands.User;
using CqsSample.CQ.Contract.Queries.User;
using CqsSample.CQ.Contract.Queries.User.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using softaware.Cqs;

namespace CqsSample.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public UserController(
            ICommandProcessor commandProcessor,
            IQueryProcessor queryProcessor)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        [HttpGet]
        public async Task<IEnumerable<UserListEntryDto>> GetUsers()
        {
            return await this.queryProcessor.ExecuteAsync(new GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUsers(Guid id)
        {
            return await this.queryProcessor.ExecuteAsync(new GetUserById(id));
        }

        [HttpPost]
        public async Task<Guid> AddOrUpdateUser([FromBody] AddOrUpdateUser command)
        {
            /* Two things are interesting here:
             * 
             * 1) We expose the whole command in the API, which means we don't have a separate model here.
             *    This means the API layer is just an very thin layer which just delegates everything to the CQ "Contract", i.e. the commands and queries.
             *    For example, all data annotation attributes on the commands are considered in the API (required fields, max lengths, ...)
             *    
             * 2) Sometimes when creating entities, we need to know the ID of the newly created entity.
             *    In that case, we defined the AddOrUpdateUser.Id property as Guid? which means that we can either add a new user or update an existing one.
             *    We can be sure, that after the AddOrUpdateUserHandler has been executed, the AddOrUpdateUser.Id must have a value in either case.
             *    
             *    This is a little trick to expose "output properties" in the commands if we really need a result from commands.
             *    This is a trade-off to the purity of the CQS pattern, but sometimes this small trade-off is acceptable.
             */

            await this.commandProcessor.ExecuteAsync(command);
            return command.Id.Value;
        }
    }
}
