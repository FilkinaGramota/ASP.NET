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
            
            async Task AboutContext (HttpContext context)
            {
                string host = context.Request.Host.Value;
                string path = context.Request.Path.Value;
                string protocol = context.Request.Protocol;
                string query = context.Request.QueryString.Value;
                string method = context.Request.Method;

                await context.Response.WriteAsync($"Host: {host}\nPath: {path}\nProtocol: {protocol}\nQuery: {query}\nMethod: {method}");
            }

            app.Run(AboutContext);
        }
    }
}
