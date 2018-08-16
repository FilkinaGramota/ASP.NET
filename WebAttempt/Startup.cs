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
            int x = 2;
            int y = 3;
            int z = 0;

            app.Use(async (context, next) =>
            {
                z = x * y; // z = 6
                await next(); // go to Run because it is next
                // from Run
                z = z + x + y; // z = 17
                await context.Response.WriteAsync($"Hi, I am from Run and z = {z}");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hi, I am from Use and z = {z}.\n");
                z = z * 2;  // z = 12 and go back to Use
            });
        }
    }
}
