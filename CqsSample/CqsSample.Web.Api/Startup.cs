using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqsSample.CQ.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SimpleInjector;
using softaware.Authentication.Basic;
using softaware.Authentication.Basic.AspNetCore;
using softaware.Authentication.Basic.AspNetCore.AuthorizationProvider;
using Swashbuckle.AspNetCore.Filters;

namespace CqsSample.Web.Api
{
    public class Startup
    {
        private readonly Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // This basic authentication with in-memory username/passwords is just for easy testing via Swagger.
            // Usually, we would have Cookie or Token authenticationi
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = BasicAuthenticationDefaults.AuthenticationScheme;
            })
            .AddBasicAuthentication(BasicAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.AuthorizationProvider = new MemoryBasicAuthenticationProvider(new Dictionary<string, string>()
                {
                    ["sherlock@holmes.com"] = "sherlock",
                    ["james@watson.com"] = "james"
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQS Sample", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "basic"
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddSimpleInjector(this.container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQS Sample V1");
                c.RoutePrefix = string.Empty;
            });

            this.InitializeContainer();
            this.container.Verify();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitializeContainer()
        {
            // this.container.RegisterSingleton<IPrincipal, HttpContextPrincipal>();

            // PermissionsBootstrapper.Bootstrap(this.container);
            HandlerBootstrapper.Bootstrap(this.container);
        }

        public class MemoryBasicAuthenticationProvider : IBasicAuthorizationProvider
        {
            private readonly IReadOnlyDictionary<string, string> credentials;

            public MemoryBasicAuthenticationProvider(IReadOnlyDictionary<string, string> credentials)
            {
                this.credentials = credentials.ToDictionary(c => c.Key, c => c.Value);
            }

            public Task<bool> IsAuthorizedAsync(string username, string password)
            {
                return Task.FromResult(this.credentials.TryGetValue(username, out var secureString) && password == secureString);
            }
        }
    }
}
