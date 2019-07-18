using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using VehicleTracking.Data.Vehicle;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VehicleTracking.Data;
using VehicleTracking.Data.Repository;
using VehicleTracking.Data.TrackingHistory;
using VehicleTracking.Gateway.GoogleGateway;
using VehicleTracking.GateWay.GoogleGateway;
using VehicleTracking.Service;
using VehicleTracking.Service.Interface;
using VehicleTracking.WebApi.Middleware;

namespace VehicleTracking.WebApi
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
            //Configuration for BD
            services.AddDbContext<VehicleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("VehicleDatabase")));

            services.AddDbContext<TrackingHistoryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TrackingHistoryDatabase")));

            //Configuration for Identity
            services.AddIdentity<VehicleTrackingUser, IdentityRole<Guid>>(cfg =>
                {
                    cfg.User.RequireUniqueEmail = true;
                    cfg.Password.RequireDigit = true;
                    cfg.Password.RequiredLength = 5;
                    cfg.Password.RequireUppercase = false;
                    cfg.Password.RequireLowercase = false;
                    cfg.Password.RequireNonAlphanumeric = false;
                    cfg.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<VehicleContext>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<ITrackingService, TrackingService>();
            services.AddTransient<IGeoCodingService, GeoCodingService>();

            services.AddScoped(typeof(IReponsitory<>), typeof(VehicleRepository<>));
            services.AddScoped(typeof(IReponsitory<>), typeof(TrackingHistoryRepository<>));

            //Configuration for token 
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<VehicleTrackingUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            IdentityDataInitializer.SeedData(userManager, roleManager).Wait();
            app.UseMvc();
        }
    }
}
