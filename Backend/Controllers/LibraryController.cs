using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DataBase;
using Backend.Models.ViewModels;
using Backend.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        // Свойства класса.
        public QueriesService QueriesService { get; set; }

        // Ансамбль конструкторов.
        // Конструктор с параметрами.
        public LibraryController(QueriesService queriesService)
        {
            QueriesService = queriesService;
        } 

        // TODO: Подумать над применением switch вместо кучи методов.
        // Работает.
        // GET: api/<LibraryController>
        [HttpGet("{id}")]
        public async Task<JsonResult> GetQuery1Async(int id)
        {
            object collection = string.Empty;
            switch (id)
            {
                case 1:
                    collection = await QueriesService.Query1Async();
                    break;
                case 2:
                    collection = await QueriesService.Query2Async();
                    break;
                case 3:
                    collection = await QueriesService.Query3Async();
                    break;
                case 4:
                    collection = await QueriesService.Query4Async();
                    break;
                case 5:
                    collection = await QueriesService.Query5Async();
                    break;
                case 6:
                    collection = await QueriesService.Query6Async();
                    break;
            } // switch

            return new JsonResult(collection);
        }
    }
}
