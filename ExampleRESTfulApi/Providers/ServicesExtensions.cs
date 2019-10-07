using DAL.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using ExampleRESTfulApi.Swagger.RequestExamples;

namespace ExampleRESTfulApi.Providers
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
        {        
            services.AddIdentity<IdentityUser, IdentityRole>(
                // Настройка характеристик пароля 
                option =>
                {
                    option.Password.RequireDigit = false;
                    option.Password.RequiredLength = 6;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireLowercase = false;
                }).AddEntityFrameworkStores<ApplicationDbContext>() // хранилище под Identity
                .AddDefaultTokenProviders(); // добавляется поддержка токена

            // Настройка параметров аутентификации и bearer токена
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Site"],
                    ValidIssuer = configuration["Jwt:Site"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]))
                };
            });

            return services;
        }

        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {        
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ExampleRESTApi", Version = "v1" });

                options.ExampleFilters(); // Разрешает добавление фильтров   

                options.AddSecurityDefinition("Bearer", // название схемы безопасности
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http, // Мы устанавливаем тип схемы на http, так как мы используем аутентификацию - bearer authentication
                    Scheme = "bearer" //Имя схемы авторизации HTTP, которая будет использоваться в заголовке авторизации.
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", // Имя схемы безопасности
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });      

                // xml-документация в swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);               
            });

            // зарегистрирует в ServiceProvider, для отображения пользовательских примеров в запросах
            services.AddSwaggerExamplesFromAssemblyOf<LoginViewModelExample>(); 
            services.AddSwaggerExamplesFromAssemblyOf<ContractDocumentModelExample>();
            //----------------------------------------------------------------------------------------

            return services;
        }
    }
}
