using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Oostel.Infrastructure.Data;
using Oostel.Application;
using Microsoft.AspNetCore.Identity.UI.Services;
using Oostel.Application.Modules.UserAuthentication.Services;
using Oostel.Infrastructure.EmailService;
using Oostel.Infrastructure.Repositories;
using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using System.Reflection;
using Marvin.Cache.Headers;
using Microsoft.Extensions.Configuration;
using System.Runtime;

namespace Oostel.API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
        {

           

            services.AddServicesConfiguration(_config);

            services.AddHttpContextAccessor();
            services.AddScoped<UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddDbContext<ApplicationDbContext>(Options =>
            {
                Options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });
          
            services.Configure<EmailConfiguration>(_config.GetSection("EmailConfiguration"));  

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RegisterUserCommand).GetTypeInfo().Assembly));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });

            services.AddResponseCaching();

            services.AddHttpCacheHeaders((expirationOpt) =>
            {
                expirationOpt.MaxAge = 65;
                expirationOpt.CacheLocation = CacheLocation.Private;
            },
            (validationOpt) =>
            {
                validationOpt.MustRevalidate = true;
            });

            services.AddIdentityServices(_config);

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("LandlordAndAgent", policy => policy.RequireRole("LandLord", "Agent"));
            });

            return services;
        }

    }
}
