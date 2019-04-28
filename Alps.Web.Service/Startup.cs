using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Alps.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Alps.Web.Service.Auth;

namespace Alps.Web.Service
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
            var jwtOption=new AlpsJwtOption();
            jwtOption.SecurityKey=Configuration["JwtOption:SecurityKey"];
            jwtOption.Audience=Configuration["JwtOption:Audience"];
            jwtOption.Issuer=Configuration["JwtOption:Issuer"];
            services.AddSingleton(jwtOption);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience =jwtOption.Audience,//Audience
                        ValidIssuer = jwtOption.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecurityKey)),//拿到SecurityKey
                        NameClaimType="idName"
                    };
                    
                });
            services.AddMvc(o=>{o.Filters.Add(typeof(AlpsAuthorizationFilter));}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<AlpsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AlpsContext"), b => b.MigrationsAssembly("Alps.Web.Service"));
            });
            services.AddScoped<Alps.Domain.Service.StockService>();
            services.AddSpaStaticFiles(
                Configuration => { Configuration.RootPath = "wwwroot/alpsWebAngular"; }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            // app.UseHttpsRedirection();
            // app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();
            app.UseSpa(spa =>
            {

            });
        }
    }
}
