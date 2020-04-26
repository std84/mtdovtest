using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using missiontest.DATA;
using missiontest.REPOSITORY;
using NLog;
namespace missiontest.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
             LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "./nlog.config"));  
           
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.a
        public void ConfigureServices(IServiceCollection services)
        {
            
                services.AddControllers();
                services.AddDbContext<dataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("missiontest.API")));
                services.AddScoped<IAuthRepository, AuthRepository>();
                services.AddScoped<ImissionRepository, missionRepository>();
                services.AddScoped<IMailRepository, mailRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddSingleton<ILog, LogNLog>();  
                
                services.AddCors();

               //Authentication 
            //var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:Secret"));
          services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction())
            {
                    app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();
           
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
