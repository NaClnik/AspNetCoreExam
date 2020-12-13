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
    public partial class Startup
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
            // Добавляем сервис для контроллеров.
            services.AddControllers();
            
            // Добавляем сервис для CORS.
            services.AddCors();

            // Сделал класс Startup - partial классом.
            // В другом файле(Startup.MyServices.cs) я создал
            // ещё один Startup partial класс и в нём определил метод
            // SetupServices, чтобы не захламлять основной класс Startup
            // своими зависимостями. Так посоветовал делать Senior Developer
            // на вэбинаре от ITVDN.
            SetupServices(services);
        } // ConfigureServices.


        // Конвейер обработки запросов.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } // if.

            app.UseRouting();
            
            app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); // UseEndpoints
        } // Configure.
    } // Startup. 
}
