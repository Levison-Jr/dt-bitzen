
using DTBitzen.Context;
using DTBitzen.Identity;
using DTBitzen.Models;
using DTBitzen.Repositories;
using DTBitzen.Repositories.Interfaces;
using DTBitzen.Services;
using DTBitzen.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu_token_aqui}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

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

            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<ISalaService, SalaService>();
            builder.Services.AddScoped<IReservaService, ReservaService>();

            builder.Services.AddScoped<ISalaRepository, SalaRepository>();
            builder.Services.AddScoped<IReservaRepository, ReservaRepository>();


            builder.Services.AddScoped<AspNetUserManager<Usuario>>();
            builder.Services.AddScoped<SignInManager<Usuario>>();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();

            app.UseAuthentication();
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
