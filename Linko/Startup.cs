using AutoMapper;
using Linko.Domain;
using Linko.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Linko
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.SaveToken = true;
                    cfg.RequireHttpsMetadata = false;
                    cfg.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.FromHours(10),
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Key.SecretKey))
                    };
                });

            services.AddDbContext<LinkoContext>(option => option.UseSqlServer(DBConn.ConnectionString),
                ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            //services.AddSingleton(typeof(IDapperRepository<>), typeof(DapperRepository<>));
            //services.AddSingleton<ILoggerRepository, LoggerRepository>();
            //services.AddScoped<IAccountService, AccountService>();

            // Auto Mapper Configurations
            IMapper mapper = new MapperConfiguration
                (mc => { mc.AddProfile(new MappingProfile()); })
                .CreateMapper();

            services.AddSingleton(mapper);

            RegisterServices<IRegisterScopped>(services);
            RegisterServices<IRegisterSingleton>(services);

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1", new OpenApiInfo { Title = "Linko", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Linko v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices<ServiceType>(IServiceCollection services)
        {
            var myServices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes());

            myServices.Where(service => typeof(ServiceType).IsAssignableFrom(service) && service != typeof(ServiceType))
                .ToList().ForEach((service) =>
                {
                    Type interfaceType = myServices.FirstOrDefault(x => x.Name == "I" + service.Name);

                    if (interfaceType == null && typeof(ServiceType) == typeof(IRegisterScopped))
                        services.AddScoped(service);
                    else if (interfaceType == null && typeof(ServiceType) == typeof(IRegisterSingleton))
                        services.AddSingleton(service);
                    else if (interfaceType != null && typeof(ServiceType) == typeof(IRegisterScopped))
                        services.AddScoped(interfaceType, service);
                    else if (interfaceType != null && typeof(ServiceType) == typeof(IRegisterSingleton))
                        services.AddSingleton(interfaceType, service);
                });
        }
    }
}