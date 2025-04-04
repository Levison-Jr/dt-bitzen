
using DTBitzen.Context;
using DTBitzen.Identity;
using DTBitzen.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DTBitzen
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDefaultIdentity<Usuario>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var securityKeySettings = builder.Configuration.GetSection("JwtOptions:SecurityKey").Value;

            if (string.IsNullOrEmpty(securityKeySettings))
            {
                throw new SecurityTokenException("JWT Security Key desconfigurada.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeySettings));

            builder.Services.Configure<JwtOptions>(options =>
            {
                builder.Configuration.GetSection(nameof(JwtOptions)).Bind(options);
                options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration.GetSection("JwtOptions:Issuer").Value,

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration.GetSection("JwtOptions:Audience").Value,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DbConnection") ??
                    throw new InvalidOperationException("Connection string indefinida.");

                options.UseNpgsql(connectionString);
            });

            builder.Services.AddScoped<IIdentityHelper, IdentityHelper>();
            builder.Services.AddScoped<AspNetUserManager<Usuario>>();
            builder.Services.AddScoped<SignInManager<Usuario>>();

            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            // Aplicar migrations pendentes ou criar o db inexistente
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();

                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await context.Database.MigrateAsync();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao aplicar migrations. Tentativa {i + 1}: {ex.Message}");
                        await Task.Delay(5000);
                    }
                }
            }

            app.Run();
        }
    }
}
