using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HostDy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            CircuitBreakerPolicy breaker = Policy
              .Handle<HttpRequestException>()
              .CircuitBreaker(
                exceptionsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromMinutes(1)
              );

            //// Wait and retry forever
            //Policy
            //  .Handle<SomeExceptionType>()
            //  .WaitAndRetryForever(retryAttempt =>
            //    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            //    );

            //// Wait and retry forever, calling an action on each retry with the
            //// current exception and the time to wait
            //Policy
            //  .Handle<SomeExceptionType>()
            //  .WaitAndRetryForever(
            //    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            //    (exception, timespan) =>
            //    {
            //        // Add logic to be executed before each retry, such as logging       
            //    });

            //            // Wait and retry forever, calling an action on each retry with the
            //            // current exception, time to wait, and context provided to Execute()
            //            Policy
            //              .Handle<SomeExceptionType>()
            //              .WaitAndRetryForever(
            //                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            //                (exception, timespan, context) =>
            //                {
            //        // Add logic to be executed before each retry, such as logging       
            //    });

            services.AddControllers();
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "Mypolicy",
                                  policy =>
                                  {
                                      policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("Mypolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
