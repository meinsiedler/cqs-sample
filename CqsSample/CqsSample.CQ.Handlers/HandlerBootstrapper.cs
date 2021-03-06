﻿using System;
using System.Reflection;
using CqsSample.CQ.Handlers.CrossCuttingConcerns.CommandHandlers.Authorization;
using CqsSample.CQ.Handlers.CrossCuttingConcerns.QueryHandlers.Authorization;
using SimpleInjector;
using softaware.Cqs;
using softaware.Cqs.Decorators.Transaction;
using softaware.Cqs.Decorators.Validation;
using softaware.Cqs.SimpleInjector;

namespace CqsSample.CQ.Handlers
{
    public static class HandlerBootstrapper
    {
        private static Assembly[] handlerAssemblies = new[] { Assembly.GetExecutingAssembly() };

        public static void Bootstrap(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.RegisterInstance<IQueryProcessor>(new DynamicQueryProcessor(container));
            container.RegisterInstance<ICommandProcessor>(new DynamicCommandProcessor(container));

            // Used by ValidationDecorators below.
            container.RegisterInstance<IValidator>(new DataAnnotationsValidator());

            /* Information for developers: How are decorators applied?
             * 
             * 1) When multiple decorators are applied on the same concrete handler instance, the decorators will be stacked above each other in the order they are registered.
             *    E.g.:
             *        Register(concreteHandler)
             *        RegisterDecorator(decorator1)
             *        RegisterDecorator(decorator2)
             *    This means that first, "decorator2" is executed, then "decorator1", and then "concreteHandler".
             * 
             * 2) Some decorators do have additional generic type constraints, e.g. the "AccessesUserCommandHandlerDecorator" has a generic type constraint
             *    for the `TCommand` type, which defines that this type must implement `IAccessesUser`.
             *    
             *    This information is used by SimpleInjector, so that the "AccessesUserCommandHandlerDecorator" is ONLY applied for command handlers which handle
             *    `TCommand` types which implement the `IAccessesUser` interface.
             *    
             * 3) The decision, which decorators must be applied for which concrete handlers is made by SimpleInjector
             *    only once for each type on application startup, when the DI container is created.
             * 
             */

            // Command handler registrations for all handlers in this assembly which implement ICommandHandler<>.
            container.Register(typeof(ICommandHandler<>), handlerAssemblies);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(AccessesUserCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(PermissionCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionAwareCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(PublicCommandHandlerDecorator<>)); // This must be the last registered decorator, see https://github.com/softawaregmbh/library-cqs/releases/tag/v2.0.0

            // Query handler registrations for all handlers in this assembly which implement IQueryHandler<,>.
            container.Register(typeof(IQueryHandler<,>), handlerAssemblies);

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(AccessesUserQueryHandlerDecorator<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(PermissionQueryHandlerDecorator<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ValidationQueryHandlerDecorator<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(PublicQueryHandlerDecorator<,>)); // This must be the last registered decorator, see https://github.com/softawaregmbh/library-cqs/releases/tag/v2.0.0
        }
    }
}
