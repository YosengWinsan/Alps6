using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Diagnostics;

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
            var jwtOption = new AlpsJwtOption();
            jwtOption.SecurityKey = Configuration["JwtOption:SecurityKey"];
            jwtOption.Audience = Configuration["JwtOption:Audience"];
            jwtOption.Issuer = Configuration["JwtOption:Issuer"];
            services.AddSingleton(jwtOption);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    /* options.Events = new JwtBearerEvents
                     {
                         OnTokenValidated = ct =>
                         {
                             //Console.WriteLine(ct.HttpContext.Request.Path.Value);
                             var auth=ct.HttpContext.RequestServices.GetRequiredService<AlpsAuthorizationFilter>();
                             auth.

                             return Task.CompletedTask;
                         }
                     };*/
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = jwtOption.Audience,//Audience
                        ValidIssuer = jwtOption.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecurityKey)),//拿到SecurityKey
                        NameClaimType = "idName"
                    };

                });
            services.AddControllers();//.AddJsonOptions();//o => o.Filters.Add(typeof(AlpsAuthorizationFilter)));
            //services.AddMvc(o=>{o.Filters.Add(typeof(AlpsAuthorizationFilter));}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<AlpsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AlpsContext"), b => b.MigrationsAssembly("Alps.Web.Service"));

            });
            //services.AddSingleton<AlpsAuthorizationFilter>();
            services.AddScoped<Alps.Domain.Service.StockService>();

            services.AddSpaStaticFiles(
                Configuration => { Configuration.RootPath = "wwwroot"; }
            );
            services.AddHttpsRedirection(o => o.HttpsPort = 443);
            ConfigModelInvalid(services);
            //services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errapp =>
                {
                    errapp.Run(async context =>
                    {
                        Exception ex = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                        string errorMsg = string.Empty;
                        if (ex is DomainException)
                        {
                            errorMsg = "领域错误:" + ex.Message;
                        }
                        else
                            errorMsg = "系统错误：" + ex.Message;
                        context.Response.StatusCode = 200;
                        context.Response.Headers.Add("content-type", "application/json; charset=utf-8");
                        var errMsg = Encoding.UTF8.GetBytes("{\"resultCode\":-1,\"messages\":[\"" + errorMsg + "\"],\"data\":null}");
                        await context.Response.Body.WriteAsync(errMsg, 0, errMsg.Length);
                        await context.Response.CompleteAsync();
                    });
                });
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AlpsRoleAuthorizationMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");
            });

        }
        private void ConfigModelInvalid(IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    if (context.ModelState.IsValid)
                        return null;
                    var error = "";
                    foreach (var item in context.ModelState)
                    {
                        var state = item.Value;
                        var message = state.Errors.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.ErrorMessage))?.ErrorMessage;
                        if (string.IsNullOrWhiteSpace(message))
                        {
                            message = state.Errors.FirstOrDefault(o => o.Exception != null)?.Exception.Message;
                        }
                        if (string.IsNullOrWhiteSpace(message))
                            continue;
                        error = message;
                        break;
                    }
                    return new JsonResult(new { resultCode = 400, messages = error.ToString() });
                };
            });
        }
    }
}
