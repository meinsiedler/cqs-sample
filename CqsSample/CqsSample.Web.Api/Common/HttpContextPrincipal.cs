using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CqsSample.Web.Api.Common
{
    public class HttpContextPrincipal : IPrincipal
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextPrincipal(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsInRole(string role) => this.httpContextAccessor.HttpContext?.User.IsInRole(role) ?? false;

        public IIdentity Identity => this.httpContextAccessor.HttpContext?.User.Identity;
    }
}
