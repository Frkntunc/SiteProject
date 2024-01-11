using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Site.Application.Mapping;
using Site.Application.Settings;
using Site.Domain.Authentication;
using Site.Infrastructure;
using Site.Infrastructure.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using System;
using Site.Api.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Site.Application;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddAutoMapper(typeof(MappingProfile));

#region Jwt
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
var jwt = builder.Configuration.GetSection("JWT").Get<JwtSettings>();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
    options.Lockout.MaxFailedAccessAttempts = 5;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuth(jwt);
#endregion


#region Redis
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "SampleInstance";
});
#endregion

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Site.Api", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Site.Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionMiddlaware();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
