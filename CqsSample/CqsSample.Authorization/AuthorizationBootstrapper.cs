﻿using System;
using CqsSample.Authorization.AccessControl;
using CqsSample.Authorization.Permissions;
using SimpleInjector;

namespace CqsSample.Authorization
{
    public static class AuthorizationBootstrapper
    {
        public static void Bootstrap(Container container)
        {
            container.Register<IPermissionsService, PermissionsService>();
            container.Register<IAccessControlService, AccessControlService>();
        }
    }
}
