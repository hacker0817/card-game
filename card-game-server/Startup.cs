using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CardGame.Server;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;

namespace CardGame
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(x =>
            {
                //swagger自定义忽略过滤器
                x.Conventions.Add(new Filters.SwaggerApiExplorer());
            });

            services.AddCors(options =>
            {
                //o.AddDefaultPolicy(b =>
                //{
                //    b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                //});

                options.AddPolicy("any",
                      builder =>
                      {
                          builder.SetIsOriginAllowed(_ => true)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                      });
            });

            services.AddSignalR();

            //注册swagger服务
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("client", new OpenApiInfo { Title = "client", Version = "v1.0" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                x.IncludeXmlComments(xmlPath, true);

                x.OperationFilter<AddResponseHeadersFilter>();

                x.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                x.OperationFilter<SecurityRequirementsOperationFilter>();

                x.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    Description = "如接口需授权，请输入token（无需bearer）\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("any");

            app.UseRouting();

            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<SignalRHub>("/mj");
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/client/swagger.json", "client");

                    x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                });
            }
        }
    }
}
