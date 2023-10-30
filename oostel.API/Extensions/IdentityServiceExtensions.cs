using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Infrastructure.Data;
using System.Text;

namespace Oostel.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
          IConfiguration config)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = null;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                options.Password.RequireLowercase = false;
            }).AddRoles<ApplicationRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opt =>
               {
                   opt.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = key,
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero
                   };

                   opt.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context =>
                       {
                           var accessToken = context.Request.Query["access_token"];

                           var path = context.HttpContext.Request.Path;
                           if (!string.IsNullOrEmpty(accessToken) &&
                               path.StartsWithSegments("/hubs"))
                           {
                               context.Token = accessToken;
                           }

                           return Task.CompletedTask;
                       }
                   };
               });

            return services;
        }
    }
}
