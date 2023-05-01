using ApplicationCore.Const;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

namespace SurveyApp.Configuration
{
    public static class Configure
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services
                .AddDbContext<SurveyDbContext>()
                .AddIdentity<UserEntity, UserRoleEntity>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Lockout.MaxFailedAccessAttempts = 3;
                })
                .AddEntityFrameworkStores<SurveyDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
                opt.AddPolicy("Email", policy =>
                {
                    policy.RequireClaim("email");
                });
            });
            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    if (jwtSettings.Secret != null)
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                            ClockSkew = TimeSpan.FromSeconds(60)
                        };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenException))
                            {
                                context.Response.Headers.Add("Token-expired", "true");
                            }
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject("401 Not authorized");
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject("403 Not authorized");
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
              Enter 'Bearer' and then your token in the text input below.
              Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                    }
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Quiz API",
                });
            });
        }

        public static async void AddUsers(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {

                var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<UserRoleEntity>>();

                var userRoleExists = await roleManager.RoleExistsAsync(Roles.User);
                var adminRoleExists = await roleManager.RoleExistsAsync(Roles.User);

                if(!userRoleExists && !adminRoleExists)
                {
                    await roleManager.CreateAsync(new UserRoleEntity() { Name = Roles.User });
                    await roleManager.CreateAsync(new UserRoleEntity() { Name = Roles.Admin });
                }

                var foundNormalUser = await userManager.FindByEmailAsync("normaluser@mail.com");

                if (foundNormalUser == null)
                {
                    UserEntity user = new UserEntity() { Email = "normaluser@mail.com", UserName = "normalUser" };

                    var saved = await userManager?.CreateAsync(user, "Test123!");
                    await userManager.AddToRoleAsync(user, Roles.User);
                }

                var foundAdmin = await userManager.FindByEmailAsync("administrator@mail.com");

                if(foundAdmin == null)
                {
                    UserEntity admin = new UserEntity() { Email = "administrator@mail.com", UserName = "administrator" };

                    var saved = await userManager?.CreateAsync(admin, "Test123!");
                    userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
    }
}
