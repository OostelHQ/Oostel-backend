using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Oostel.Infrastructure.Data;
using Oostel.Application;
using Microsoft.AspNetCore.Identity.UI.Services;
using Oostel.Application.Modules.UserAuthentication.Services;
using Oostel.Infrastructure.EmailService;
using Oostel.Infrastructure.Repositories;

namespace Oostel.API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
        {

            services.AddServicesConfiguration(_config);

            services.AddScoped<UnitOfWork>();

            services.AddDbContext<ApplicationDbContext>(Options =>
            {
                Options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });

            services.AddIdentityServices(_config);

            return services;
        }

        public static void AddPaginationHeader(this HttpResponse response, int currentPage,
         int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages
            };
            response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationHeader));

        }
    }
}
