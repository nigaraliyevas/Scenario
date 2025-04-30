using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Scenario.Application.Dtos.PlotDtos;
using Scenario.Application.Profiles;
using Scenario.Application.Service.Implementations;
using Scenario.Application.Service.Interfaces;
using Scenario.Application.Settings;
using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;
using Scenario.DataAccess.Implementations;
using Scenario.DataAccess.Implementations.UnitOfWork;
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
            services.AddDbContext<ScenarioAppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<PlotCreateDto>();
            services.AddFluentValidationRulesToSwagger();

            //Dependency injections

            services.AddScoped<IPlotRepository, PlotRepository>();
            services.AddScoped<IPlotService, PlotService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IScriptwriterRepository, ScriptwriterRepository>();
            services.AddScoped<IScriptwriterService, ScriptwriterService>();

            services.AddScoped<IPlotRatingRepository, PlotRatingRepository>();
            services.AddScoped<IPlotRatingService, PlotRatingService>();

            services.AddScoped<IChapterRepository, ChapterRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IChapterService, ChapterService>();

            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IContactUsService, ContactUsService>();


            services.AddScoped<IAdRepository, AdRepository>();
            services.AddScoped<IAdService, AdService>();


            services.AddScoped<IPlotAppUserRepository, PlotAppUserRepository>();

            services.AddScoped<IPlotCategoryRepository, PlotCategoryRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //email

            services.Configure<EmailConfigurationSettings>(config.GetSection("EmailConfiguration"));

            services.AddSingleton<IEmailSenderService, EmailSenderService>();

            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMinutes(5));
            //

            //end

            //gulbahar
            services.AddScoped<IAboutTestimonialService, AboutTestimonialService>();
            services.AddScoped<ILikeDislikeService, LikeDislikeService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IUserScenarioFavoriteService, UserScenarioFavoriteService>();

            services.AddScoped<IAboutTestimonialRepository, AboutTestimonialRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<ILikeDislikeRepository, LikeDislikeRepository>();
            services.AddScoped<IUserScenarioFavoriteRepository, UserScenarioFavoriteRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();



            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });

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


            services.AddAuthentication()
                 .AddCookie(options =>
                 {
                     options.LoginPath = "/login";  // Redirect to login page
                     options.LogoutPath = "/logout"; // Redirect after logout
                     options.AccessDeniedPath = "/access-denied"; // Redirect if unauthorized
                 })
                .AddGoogle(options =>
                {
                    options.ClientId = config["Authentication:Google:ClientId"];
                    options.ClientSecret = config["Authentication:Google:ClientSecret"];
                });

            //.AddApple(options =>
            //{
            //    options.ClientId = config["Authentication:Apple:ClientId"];  // Service ID
            //    options.TeamId = config["Authentication:Apple:TeamId"]; // Apple Developer Team ID
            //    options.KeyId = config["Authentication:Apple:KeyId"]; // Key ID from Apple
            //    options.UsePrivateKey((keyId) =>builder.Environment.ContentRootFileProvider.GetFileInfo("AuthKey.p8"));
            //});


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
                options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
        }
    }
}
