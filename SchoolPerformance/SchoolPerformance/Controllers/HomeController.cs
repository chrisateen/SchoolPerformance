using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using StackExchange.Redis;

namespace SchoolPerformance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Request made to view Homepage");
            return View();
        }

        public IActionResult FAQ()
        {
            _logger.LogInformation("Request made to view FAQ page");
            return View();
        }

        public IActionResult About()
        {
            _logger.LogInformation("Request made to view About page");
            return View();
        }

    }
}