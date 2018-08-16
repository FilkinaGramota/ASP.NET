using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebAttempt
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // на всякий пожарный (ну и раз Task => async/await)
            // public delegate Task RequestDelegate (HttpContext context)

            app.Map("/index", Index);
            app.MapWhen(context =>
                {
                    return (context.Request.Query.ContainsKey("id") && context.Request.Query["id"] == "1");
                }, appBuilder =>
                {
                    appBuilder.Run(async (contextHttp) => await contextHttp.Response.WriteAsync("Ta-dam, id = 1."));
                });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello world, id != 1!");
            });
        }

        private void Index(IApplicationBuilder app)
        {
            app.Use(async (contex, next) =>
            {
                await contex.Response.WriteAsync("I am in Index (first Use).\n");
                await next();

                await contex.Response.WriteAsync("I am in Index (first Use). Again.\n");
            });

            app.Use(async (context, next) => { await context.Response.WriteAsync("I am in Index (second Use).\n"); });
        }
    }
}
