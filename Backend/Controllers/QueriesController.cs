using System;
using System.Collections;
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
    public class QueriesController : ControllerBase
    {
        // Свойства класса.
        public QueriesService QueriesService { get; set; }

        // Ансамбль конструкторов.
        // Конструктор с параметрами.
        public QueriesController(QueriesService queriesService)
        {
            QueriesService = queriesService;
        } // ctor.

        // GET: api/<QueriesController>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetQueriesAsync(int id)
        {
            string response;
            switch (id)
            {
                case 1:
                    response = await QueriesService.Query1Async();
                    break;
                case 2:
                    response = await QueriesService.Query2Async();
                    break;
                case 3:
                    response = await QueriesService.Query3Async();
                    break;
                case 4:
                    response = await QueriesService.Query4Async();
                    break;
                case 5:
                    response = await QueriesService.Query5Async();
                    break;
                case 6:
                    response = await QueriesService.Query6Async();
                    break;
                case 7:
                    response = await QueriesService.Query7Async();
                    break;
                case 8:
                    response = await QueriesService.Query8Async();
                    break;
                default:
                    return NotFound();
            } // switch

            return response;
        }
    }
}
