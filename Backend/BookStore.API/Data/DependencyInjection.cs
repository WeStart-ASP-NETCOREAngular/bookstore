using BookStore.API.Interfaces;
using BookStore.API.Models;
using BookStore.API.Repositories;
using BookStore.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStore.API.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContext<BookStoreDbContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();


            services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<BookStoreDbContext>()
               .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddAuthLayer(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
               .AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(options => options.TokenValidationParameters =
               new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidAudience = configuration["JWT:ValidAudience"],
                   ValidIssuer = configuration["JWT:ValidIssuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secert"])),
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero
               });
            return services;
        }
    }
}
