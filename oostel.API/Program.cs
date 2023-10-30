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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers(
 opt =>
 {
     var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
     opt.Filters.Add(new AuthorizeFilter(policy));
 });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");  //We're giving this a root where it's gonna be access from
app.MapHub<MessageHub>("hubs/message");
app.MapHub<CommentHub>("hubs/comment");

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
