using Serilog;
using Oostel.Application;
using Microsoft.AspNetCore.Identity;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Oostel.API.Extensions;
using Oostel.API.SIgnalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Oostel.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Connections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers(
 opt =>
 {
     var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
     opt.Filters.Add(new AuthorizeFilter(policy));
     opt.CacheProfiles.Add("120SecondsDuration", new CacheProfile
     {
         Duration = 120
     });
 });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.KeepAliveInterval = TimeSpan.FromSeconds(10);
    options.HandshakeTimeout = TimeSpan.FromSeconds(5);
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();

//builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");
app.UseRouting();

app.UseResponseCaching();

app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();


app.UseSwagger();
app.MapControllers();

app.MapHub<PresenceHub>("hubs/presence", options =>
{
    options.Transports =
        HttpTransportType.WebSockets |
        HttpTransportType.LongPolling;
    options.CloseOnAuthenticationExpiration = true;
});
app.MapHub<MessageHub>("hubs/message", options =>
{
    options.Transports =
        HttpTransportType.WebSockets |
        HttpTransportType.LongPolling;
    options.CloseOnAuthenticationExpiration = true;
});
app.MapHub<CommentHub>("hubs/comment", options =>
{
    options.Transports =
        HttpTransportType.WebSockets |
        HttpTransportType.LongPolling;
    options.CloseOnAuthenticationExpiration = true;
});


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<ApplicationDbContext>();

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedIdentityRoles(context, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

await app.RunAsync();
