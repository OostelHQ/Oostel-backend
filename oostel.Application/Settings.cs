﻿using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Application.Modules.UserAuthentication.Services;
using Oostel.Application.Modules.UserMessage.Services;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Application.Modules.UserWallet.Services;
using Oostel.Application.UserAccessors;
using Oostel.Application.Validators.UserAuthentication;
using Oostel.Infrastructure.EmailService;
using Oostel.Infrastructure.FlutterwaveIntegration;
using Oostel.Infrastructure.Media;
using Oostel.Infrastructure.Repositories;
using System.Reflection;


namespace Oostel.Application
{
    public static class Settings
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration _configuration)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
            });

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<CloudinarySettings>(_configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IMediaUpload, MediaUpload>();
            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            services.AddScoped<IUserRolesProfilesService, UserRolesProfilesService>();
            services.AddScoped<IHostelService, HostelService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserWalletService, UserWalletService>();
            services.AddHttpClient<IFlutterwaveClient, FlutterwaveClient>();

            return services;
        }
    }
}
