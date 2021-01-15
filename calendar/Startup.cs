using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using jwt_authentication_lib.Utils;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace calendar
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(config =>
                {
                    //config.Filters.Add(new AuthorizationFilter());
                })
                .AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var fieldErrors = ErrorUtils.GetFieldErrors(actionContext.ModelState);
                        var error = new
                        {
                            fieldErrors
                        };
                        return new BadRequestObjectResult(error);
                    };
                });

            // Add OpenAPI v3 document
            services.AddOpenApiDocument(config =>
            {
                // �]�w���W�� (���n) (�w�]��: v1)
                config.DocumentName = "v1";

                // �]�w���� API ������T
                config.Version = "0.0.1";

                // �]�w�����D (����� Swagger/ReDoc UI ���ɭԷ|��ܦb�e���W)
                config.Title = "od";

                // �]�w���²�n����
                config.Description = "od description";

                var apiScheme = new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Copy this into the value field: Bearer {token}"
                };

                config.AddSecurity("JWT Token", Enumerable.Empty<string>(), apiScheme);

                config.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT Token"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
