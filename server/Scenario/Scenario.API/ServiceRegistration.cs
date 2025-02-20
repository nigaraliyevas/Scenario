using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Scenario.Application.Profiles;
using Scenario.Application.Service.Implementations;
using Scenario.Application.Service.Interfaces;
using Scenario.Application.Settings;
using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;
using Scenario.DataAccess.Implementations;
using System.Text;

namespace Scenario.API
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                })
            .ConfigureApiBehaviorOptions(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(e => e.Value?.Errors.Count > 0)
                    .Select(x => new Dictionary<string, string>() { { x.Key, x.Value.Errors.First().ErrorMessage } });
                    return new BadRequestObjectResult(new { message = (string)null, errors });
                };
            });

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            //services.AddValidatorsFromAssemblyContaining<MovieCreateDto>();
            services.AddFluentValidationRulesToSwagger();


            
            services.AddScoped<IAboutTestimonialService, AboutTestimonialService>();
            services.AddScoped<ILikeDislikeService, LikeDislikeService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IUserScenarioFavoriteService, UserScenarioFavoriteService>();
            services.AddScoped<ICommentService, CommentService>();
            //services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IAboutTestimonialRepository, AboutTestimonialRepository>();
            services.AddScoped<ILikeDislikeRepository, LikeDislikeRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<IUserScenarioFavoriteRepository, UserScenarioFavoriteRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();


            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });

            services.AddHttpContextAccessor();

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<ScenarioAppDbContext>().AddDefaultTokenProviders();
            services.Configure<JwtSettings>(config.GetSection("Jwt"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"])),
                };
            });

            //Jwt Bearer send in UI
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                 });
            });

            //CORS Policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:5175")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
        }
    }
}
