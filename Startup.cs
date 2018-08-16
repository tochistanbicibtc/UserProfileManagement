using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using StanbicIBTC.UserMgtProfile.Domain.DTOS;
using StanbicIBTC.UserMgtProfile.Data;
using StanbicIBTC.UserMgtProfile.Services.Interface;
using StanbicIBTC.UserMgtProfile.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;
using System.Reflection;
using System.Text;

namespace StanbicIBTC.UserMgtProfile.WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration iTems
            AppSettings.ConnectionString = Configuration["Data:DbConnection:ConnectionString"];
            AppSettings.ErrorLogFile = Configuration["Appsettings:ErrorLogFile"];

            AppSettings.url = Configuration["Appsettings:url"];
            AppSettings.SOAPAction = Configuration["Appsettings:SOAPAction"];
            AppSettings.Authorization = Configuration["Appsettings:Authorization"];
            AppSettings.moduleid = Configuration["Appsettings:moduleid"];
            AppSettings.token_clientid = Configuration["Appsettings:token_clientid"];

            services.AddScoped(provider =>
            {                
                return new UserMgtProfileContext(AppSettings.ConnectionString);
            });
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            // JWT Authentication
            ////services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = (ctx) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = (ctx) =>
                    {
                        ctx.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            });

            // Well Well Well

            services.AddMvc();

            //set request lenght Asp.net Core
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    // Set a short timeout for easy testing.
            //    //options.IdleTimeout = TimeSpan.FromSeconds(1200);
            //    options.CookieHttpOnly = true;
            //});

            //// Well Well Well

            services.AddScoped<ICryptoSource, CryptoSource>();
            services.AddScoped<IRolePages, RolePagesManagement>();
            services.AddScoped<ICustomerProfile, CustomerProfileManagement>();
            services.AddScoped<ITwoFactor, TwoFactorAuthentication>();
            services.AddScoped<ILogin, LoginImplementaion>();
            services.AddScoped<IActiveDirectoryUtility, ActiveDirectoryUtility>();

            services.AddSingleton<IConfiguration>(Configuration);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Stanbic IBTC User Profile Management",
                    Description = "Management of User Profiles (Active Directory, Finacle etc.)",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Tochukwu Okafor",
                        Email = "tochukwu.okafor@tenece.com",
                        Url = "https://tochukwuokafor.com/"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    //template: "{controller=Home}/{action=Index}/{id?}");
                     template: "{controller=CustomerProfile}/{action=GetAllUsers}/{id?}");
        });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
