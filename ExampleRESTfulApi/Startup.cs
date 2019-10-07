 using DAL.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BLL;
using ExampleRESTfulApi.Providers;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using BLL.Interfaces;
using ExampleRESTfulApi.Middlewares;

namespace TitulWebCards
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                // Подключение дополнительной конфигурации с параметрами характерными для текущего значения окружающей среды
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        internal static IConfiguration Configuration { get; private set; }
        internal IHostingEnvironment HostingEnvironment { get; set; }

  
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("DAL")));

            services.AddAuthorizationServices(Configuration); // добавление сервисов авторизации и аутентификации

            services.AddScoped<IDataManager, DataManager>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddSwaggerService(); // добавление и настройка swagger

        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory  loggerFactory)
        {
            app.UseRequestLogging();
            loggerFactory.AddFile("Logs/App/{Date}", minimumLevel: LogLevel.Warning);
            var logger = loggerFactory.CreateLogger("FileLogger");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {              
                app.UseHsts();
            }
          
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExampleRESTApi V1");
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
