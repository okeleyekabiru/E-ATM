using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.BusinessLogic;
using E_ATM.Data.Entity;
using E_ATM.Data.Infrastructure;
using E_ATM.Data.Models;
using E_ATM.Data.repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EATM
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
            services.AddControllers();
            services.AddDbContext<DataContext>(opt =>
              {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
              });
            services.AddScoped<IAccountGenerator, AccountGenerator>();
            services.AddScoped<Seed>();
            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<IAccount, AccountRepo>();
            services.AddScoped<IAtm, AtmRepo>();
            services.AddControllers();
      services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<DataContext>();
        
            services.AddCors(opt =>
            {
              opt.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
            });
            services.AddMvc(config =>
            {
              var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
              config.Filters.Add(new AuthorizeFilter(policy));
            });
      services.AddScoped<IJwtSecurity, JwtUserVerification>();
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));

      services.AddAuthentication(x =>
        {
          x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
       .AddCookie()
        .AddJwtBearer(opt =>
        {
          opt.SaveToken = true;
          opt.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false
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

      // app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors("CorsPolicy");
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
