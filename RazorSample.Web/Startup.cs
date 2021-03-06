﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorSample.Data;
using RazorSample.Hr;
using RazorSample.Random;
using RazorSample.Web.Configurations;
using RazorSample.Web.Services;

namespace RazorSample.Web
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options => options.LoginPath = "/account/login/");
      services.AddMvc(options => options.Filters.Add(new ViewNameActionFilter()));

      services.AddScoped<DbConfiguration>();
      services.AddDbContext<DbContext, ConfigurableDbContext>((factory, options) =>
        options.UseSqlServer(factory.GetRequiredService<DbConfiguration>().ConnectionString));

      services.AddScoped<IRepository, Repository>();

      services.AddScoped<IResourceBuilder, ResourceBuilder>();

      services.AddScoped<IRandomGenerator, RandomGenerator>();
      services.AddScoped<INotificationService, NotificationService>();
      services.AddScoped<IPasswordHasher, PasswordHasher>();

      services.AddScoped<IEmployeeService, EmployeeService>();
      services.AddScoped<IClientService, ClientService>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseDeveloperExceptionPage();

      using (var scope = app.ApplicationServices.CreateScope())
      {
        scope.ServiceProvider.GetRequiredService<DbContext>().Database.EnsureCreated();
      }

      app.UseAuthentication();
      app.UseStaticFiles();
      app.UseMvc(options => options.MapRoute("default", "{controller=employee}/{action=index}"));
    }
  }
}
