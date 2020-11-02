using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DataBase;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend
{
    public partial class Startup
    {
        // Жизненный цикл зависимостей.
        // https://metanit.com/sharp/aspnet5/6.2.php

        // • Transient: при каждом обращении к сервису создается
        //     новый объект сервиса. В течение одного запроса
        //     может быть несколько обращений к сервису,
        //     соответственно при каждом обращении будет создаваться
        //     новый объект. Подобная модель жизненного
        //     цикла наиболее подходит для легковесных
        //     сервисов, которые не хранят данных о состоянии

        // • Scoped: для каждого запроса создается
        //     свой объект сервиса.То есть если в течение
        //     одного запроса есть несколько обращений
        //     к одному сервису, то при всех этих обращениях
        //     будет использоваться один и тот же объект сервиса.

        // • Singleton: объект сервиса создается при первом
        //     обращении к нему, все последующие запросы
        //     используют один и тот же ранее созданный объект сервиса

        // Собственно, метод, который позволяет очистить основной
        // класс Startup от возможной кучи подключений своих сервисов.
        private void SetupServices(IServiceCollection services)
        {
            // Добавляем сервис для работы с БД.
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration
                        .GetConnectionString("DefaultConnection")));

            // Добавляем сервис для того, чтобы делать запросы по заданию.
            services.AddScoped<QueriesService>();
        } // SetupServices.
    } // Startup.
}
