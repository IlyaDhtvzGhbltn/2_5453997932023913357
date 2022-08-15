using DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SavingAPI.v1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SavingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
#if DEBUG
            services.AddSingleton<IStorageService>(new MoqStorageService());
#else
#endif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(exHandlerApp =>
            {
                exHandlerApp.Run(async context =>
                {
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    var response = new ResponseBase(false, ex.Error.Message);

                    context.Response.ContentType = "text/json";
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(response.ToString());
                });
            });

            app.UseHttpsRedirection();
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
