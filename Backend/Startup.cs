using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DataBase;
using Backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Backend
{
    public class Startup
    {
        // Свойства класса.
        public IConfiguration Configuration { get; }

        // Конструктор.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } // ctorf.

        // Сервисы приложения.
        public void ConfigureServices(IServiceCollection services)
        {
            // Добавляем БД в сервисы.
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration
                        .GetConnectionString("DefaultConnection")));

            // Добавляем сервис для контроллеров.
            services.AddControllers();
            
            // Добавляем сервис для CORS.
            services.AddCors();

            services.AddScoped<QueriesService>();

        } // ConfigureServices.

        // Конвейер обработки запросов.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } // if.

            app.UseRouting();
            
            app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); // UseEndpoints
        } // Configure.
    } // Startup. 
}
