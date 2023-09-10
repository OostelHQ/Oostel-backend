using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Oostel.Infrastructure.Data;
using Oostel.Application;

namespace Oostel.API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
        {

            services.AddServicesConfiguration(_config);

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
