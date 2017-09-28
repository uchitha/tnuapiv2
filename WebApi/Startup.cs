using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        private IHostingEnvironment Environment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();

            var origins = GetAllowedOrigins();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",builder => builder.WithOrigins("https://aussiekidscode.com.au").AllowAnyHeader().AllowAnyMethod());
                options.AddPolicy("LocalDevCorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("LocalDevCorsPolicy");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("CorsPolicy");
            }

            app.UseMvc();
        }

        private string[] GetAllowedOrigins()
        {
            var originArray = Configuration.GetSection("AllowedOrigins");
            return originArray.GetChildren().Select(c => c.Value).ToArray();
        }
    }
}
